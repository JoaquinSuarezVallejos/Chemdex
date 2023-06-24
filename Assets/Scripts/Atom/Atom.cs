using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class Atom : MonoBehaviour
    {
        /// <summary>
        /// Controls the atom
        /// </summary>

        [SerializeField] private GameObject shellTemplate;
        [SerializeField] private Workbench workbench;
        [SerializeField] private float seperateSpeed; //speed particles fly away at
        [SerializeField] private bool interactable; 
        private Stack<Shell> shells; //stack of electron shells
        private DUIAnchor anchor; //ref to own DUI anchor
        private List<Particle> excessParticles; //particles that are not part of the atom 
        private float scale = 1;

        //TODO: replace with constants
        [SerializeField] private Color sBlockColor;
        [SerializeField] private Color pBlockColor;
        [SerializeField] private Color dBlockColor;
        [SerializeField] private Color fBlockColor;

        public bool Interactable { get { return interactable; } set { interactable = value; } }
        public Nucleus Nucleus { get; private set; }
        public Shell OuterShell { get { return shells.Peek(); } }
        public int ElectronCount
        {
            get
            {
                int e = 0;
                foreach (Shell shell in shells)
                    e += shell.ElectronCount;
                return e;
            }
        }
        public Element Element { get; private set; }

        private void Awake()
        {
            anchor = GetComponent<DUIAnchor>();
            Nucleus = GetComponentInChildren<Nucleus>();
            shells = new Stack<Shell>();

            excessParticles = new List<Particle>();

            ForceToCommon(1); //default to Hydrogen
        }

        private void Update()
        {
            //get the element
            Element = Elements.GetElement(Nucleus.ProtonCount);
            
            if(Element != null)
            {
                //set nucleus shake based on Isotope stability
                Isotope isotope = Element.GetIsotope(Nucleus.Mass);
                Nucleus.Shake = isotope != null ? !isotope.Stable : true;

                //set the min and max isotope mass
                Nucleus.MassMax = Element.MaxIsotope;
                Nucleus.MassMin = Element.MinIsotope;

                //Auto add Neutrons to make valid Isotope
                if(Nucleus.Mass < Element.MinIsotope)
                {
                    workbench.NewAutoNeutron();
                }

                //Auto remove Neutrons to make valid Isotope
                if(Nucleus.Mass > Element.MaxIsotope)
                {
                    Nucleus.TrimNeutrons();
                }
            }
            else
            {
                Nucleus.MassMax = 0;
                Nucleus.TrimNeutrons(); 
            }

            //add or remove shells to match element period
            while (shells.Count < Elements.GetShells(Nucleus.ProtonCount))
            {
                AddShell();
                AdjustScale();
            }

            while (shells.Count > Elements.GetShells(Nucleus.ProtonCount))
            {
                RemoveShell();
                AdjustScale();
            }
        }

        private void FixedUpdate()
        {
            if(excessParticles.Count > 0)
            {
                //copy to array so we can mutate list in loop
                Particle[] excess = excessParticles.ToArray();
                foreach (Particle particle in excess)
                {
                    //Seperate from atom
                    Vector2 diffToAtom = particle.PhysicsObj.Position - transform.position;
                    Vector3 forceToSeperate = diffToAtom.normalized * seperateSpeed;

                    //Apply the force
                    particle.PhysicsObj.AddForce(forceToSeperate);

                    //particle is no longer in DUI
                    if (!DUI.DUI.Contains(particle.PhysicsObj.Position))
                    {
                        //Destroy it
                        excessParticles.Remove(particle);
                        Destroy(particle.gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// check if position is within the bounds of the Atom
        /// </summary>
        /// <param name="pos">position to check</param>
        /// <returns>true when pos in anchor bounds</returns>
        public bool Contains(Vector2 pos)
        {
            return anchor.Bounds.Contains(pos);
        }

        public void AddExcessParticle(Particle particle)
        {
            excessParticles.Add(particle);
            particle.transform.SetParent(transform);
        }

        public void RemoveExcessParticle(Particle particle)
        {
            if (excessParticles.Contains(particle))
            {
                excessParticles.Remove(particle);
                particle.transform.SetParent(transform);
            }
        }

        /// <summary>
        /// try to Remove a specified electron from the atom
        /// </summary>
        /// <param name="particle">particle to remove</param>
        /// <returns>removal scucess</returns>
        public bool RemoveElectron(Particle particle)
        {
            //start the recursive call to remove from outer shell
            return OuterShell.RemoveParticle(particle);
        }

        /// <summary>
        /// Remove and Destroy the Outer shell
        /// </summary>
        private void RemoveShell()
        {
            //remove any particles in the outer shell
            foreach(Particle particle in OuterShell.Particles)
            {
                OuterShell.RemoveParticle(particle);
                AddExcessParticle(particle);
            }

            //destroy the shell object
            Destroy(shells.Pop().gameObject);

            if (shells.Count == 0)
                return; //don't need to calculate radius of nothing
        }

        /// <summary>
        /// Add a new outer shell
        /// </summary>
        private void AddShell()
        {    
            //create a new shell object
            GameObject obj = Instantiate(shellTemplate, transform);
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;

            //add the new shell to the stack
            Shell shell = obj.GetComponent<Shell>();

            //set attributes based on shell layer
            shell.MaxParticles = Elements.GetMaxElectrons(shells.Count, Nucleus.ProtonCount);
            shell.NextShell = shells.Count == 0 ? null : OuterShell;

            //push shell onto stack
            shells.Push(shell);

            //fill the next shell 
            if (OuterShell.NextShell != null)
            {
                workbench.NewAutoElectron(OuterShell.NextShell.MaxParticles - OuterShell.NextShell.ElectronCount);
            }
        }

        /// <summary>
        /// et the atom to the most common form of the current element
        /// </summary>
        public void ForceToCommon()
        {
            ForceToCommon(Nucleus.ProtonCount);
        }

        /// <summary>
        /// Set the atom to the most common form of an element
        /// </summary>
        /// <param name="protonCount">atomic number of the element to set</param>
        public void ForceToCommon(int protonCount)
        {
            //add or remove neutrons to match atomic number
            int protonDiff = protonCount - Nucleus.ProtonCount;
            workbench.NewAutoProton(protonDiff);
            Nucleus.TrimProtons(-protonDiff);

            //get the most common stable form of the element
            Element element = Elements.GetElement(protonCount);
            if (element != null)
            {
                //set the min and max isotope mass
                Nucleus.MassMax = element.MaxIsotope;
                Nucleus.MassMin = element.MinIsotope;

                Isotope common = element.GetCommon();
                if(common != null)
                {
                    //add or remove neutrons to match mass
                    int neutronDiff = (common.Mass - protonCount) - Nucleus.NeutronCount;
                    workbench.NewAutoNeutron(neutronDiff);
                    Nucleus.TrimNeutrons(-neutronDiff);
                }            
            }

            //add or remove shells to match element period
            while (shells.Count < Elements.GetShells(protonCount))
            {
                AddShell();
            }

            while (shells.Count > Elements.GetShells(protonCount))
            {
                RemoveShell();
            }

            //add or remove electrons to match charge
            int electronDiff = protonCount - ElectronCount;
            workbench.NewAutoElectron(electronDiff);
            OuterShell.TrimElectrons(-electronDiff);

            AdjustScale();
        }

        /// <summary>
        /// Adjust the scale of all 
        /// </summary>
        public void AdjustScale()
        {
            //calculate scale = maxRadius / baseRadius 
            float minAxis = Mathf.Min(anchor.Bounds.extents.x, anchor.Bounds.extents.y); //minor axis
            scale = Mathf.Min(1, (minAxis * 0.9f) / CalcRadius(shells.Count, 1));

            //set nucleus scale
            Nucleus.Scale = scale;

            //update shells to match scale
            int num = shells.Count;
            foreach (Shell shell in shells)
            {
                //set the scale
                shell.Scale = scale;
                //set the radius
                shell.Radius = CalcRadius(num, scale);
                num--;
            }
        }

        /// <summary>
        /// Helper method for adjusting scale
        /// Calculates shell radius at scale
        /// </summary>
        /// <param name="num">shell number</param>
        /// <param name="scale">current scale of the atom</param>
        /// <returns>Radius of shell (num) at scale</returns>
        private float CalcRadius(int num, float scale)
        {
            float nucleusRadius = Mathf.Log(Nucleus.Mass , 30 / scale) * scale + (scale / 2); //experimental max difference from center + particle width
            float shellRadiusDifference = scale * num;

            return nucleusRadius + shellRadiusDifference;
        }
    }
}

