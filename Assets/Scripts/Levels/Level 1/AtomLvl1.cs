﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class AtomLvl1 : MonoBehaviour
    {
        /// Summary: controls the atom.

        [SerializeField] private GameObject shellTemplate; // electron shell template
        [SerializeField] private Workbench workbench;
        [SerializeField] private float seperateSpeed; //speed needed for particles to fly away
        [SerializeField] private bool interactable; 
        private Stack<ShellLvl1> shells; // stack of electron shells
        private DUIAnchor anchor; // reference to DUI anchor
        private List<ParticleLvl1> excessParticles; // particles that are not part of the atom 
        private float scale = 1;

        /* Electronic configuration orbit colors:
        [SerializeField] private Color sBlockColor;
        [SerializeField] private Color pBlockColor;
        [SerializeField] private Color dBlockColor;
        [SerializeField] private Color fBlockColor;*/

        public bool Interactable { get { return interactable; } set { interactable = value; } } // true when the atom can be interacted with
        public NucleusLvl1 Nucleus { get; private set; }
        public ShellLvl1 OuterShell { get { return shells.Peek(); } } // returns the outermost shell
        public int ElectronCount // counts the number of electrons in the atom
        {
            get
            {
                int e = 0;
                foreach (ShellLvl1 shell in shells)
                    e += shell.ElectronCount;
                return e;
            }
        }
        public Element Element { get; private set; } // the element of the atom

        private void Awake()
        {
            anchor = GetComponent<DUIAnchor>(); // gets the DUI anchor
            Nucleus = GetComponentInChildren<NucleusLvl1>(); // gets the nucleus
            shells = new Stack<ShellLvl1>(); // creates a new stack of shells

            excessParticles = new List<ParticleLvl1>(); // creates a new list of excess particles

            // ForceToCommon(1); // force the atom to be hydrogen
        }

        private void Update()
        {
            //Element = Elements.GetElement(Nucleus.ProtonCount); // gets the element of the atom
            
            if (Element != null)
            {
                // set the nucleus to be unstable if the isotope is unstable
                Isotope isotope = Element.GetIsotope(Nucleus.Mass);
                Nucleus.Shake = isotope != null ? !isotope.Stable : true;

                // set the minimum and maximum isotope mass
                Nucleus.MassMax = Element.MaxIsotope;
                Nucleus.MassMin = Element.MinIsotope;

                // auto add neutrons to make a valid isotope
                if (Nucleus.Mass < Element.MinIsotope)
                {
                    workbench.NewAutoNeutron();
                }

                // auto remove neutrons to make valid isotope
                if (Nucleus.Mass > Element.MaxIsotope)
                {
                    Nucleus.TrimNeutrons();
                }
            }

            else // if the element is null
            {
                Nucleus.MassMax = 0; 
                Nucleus.TrimNeutrons(); // remove all neutrons
            }

            //// add electron shells to match the element period
            //while (shells.Count < Elements.GetShells(Nucleus.ProtonCount))
            //{
            //    AddShell();
            //    AdjustScale();
            //}

            // remove electron shells to match the element period
            while (shells.Count > Elements.GetShells(Nucleus.ProtonCount))
            {
                RemoveShell();
                AdjustScale();
            }
        }

        private void FixedUpdate()
        { 
            if (excessParticles.Count > 0) // if there are excess particles
            {
                // convert to array to avoid concurrent modification
                ParticleLvl1[] excess = excessParticles.ToArray();
                foreach (ParticleLvl1 particle in excess)
                {
                    // calculate the force needed to separate the particle from the atom
                    Vector2 diffToAtom = particle.PhysicsObj.Position - transform.position;
                    Vector3 forceToSeperate = diffToAtom.normalized * seperateSpeed;

                    // apply the force
                    particle.PhysicsObj.AddForce(forceToSeperate);

                    // if the particle is far away enough from the atom
                    if (!DUI.DUI.Contains(particle.PhysicsObj.Position))
                    {
                        // destroy it
                        excessParticles.Remove(particle);
                        Destroy(particle.gameObject);
                    }
                }
            }
        }

        public bool Contains(Vector2 pos) // check if the position is within the bounds of the atom
        {
            return anchor.Bounds.Contains(pos);
        }

        public void AddExcessParticle(ParticleLvl1 particle) // add a particle to the excess list
        {
            excessParticles.Add(particle);
            particle.transform.SetParent(transform);
        }

        public void RemoveExcessParticle(ParticleLvl1 particle) // remove a particle from the excess list
        {
            if (excessParticles.Contains(particle))
            {
                excessParticles.Remove(particle);
                particle.transform.SetParent(transform);
            }
        }

        public bool RemoveElectron(ParticleLvl1 particle) // remove a specified electron from the atom
        {
            return OuterShell.RemoveParticle(particle);
        }

        private void RemoveShell() // remove and destroy the outer electron shell
        {
            // remove any particles in the outer shell
            foreach (ParticleLvl1 particle in OuterShell.Particles)
            {
                OuterShell.RemoveParticle(particle);
                AddExcessParticle(particle);
            }

            // destroy the shell object
            Destroy(shells.Pop().gameObject);

            if (shells.Count == 0) // if there are no shells left
                return; 
        }

        //private void AddShell() // add a new outer electron shell
        //{    
        //    // create a new shell object
        //    GameObject obj = Instantiate(shellTemplate, transform);
        //    obj.SetActive(true);
        //    obj.transform.localPosition = Vector3.zero;

        //    // add the new shell to the stack
        //    ShellLvl1 shell = obj.GetComponent<ShellLvl1>();

        //    // set properties based on shell layer
        //    //shell.MaxParticles = Elements.GetMaxElectrons(shells.Count, Nucleus.ProtonCount); // set the max number of particles for the shell
        //    //shell.NextShell = shells.Count == 0 ? null : OuterShell; // set the next shell to the current outer shell

        //    // push the shell to the stack
        //    shells.Push(shell);

        //    // fill the next shell 
        //    if (OuterShell.NextShell != null) // if there is a next shell
        //    {
        //        workbench.NewAutoElectron(OuterShell.NextShell.MaxParticles - OuterShell.NextShell.ElectronCount); // add electrons to fill the shell
        //    }
        //}

        public void ForceToCommon() 
        {
            ForceToCommon(Nucleus.ProtonCount);
        }

        // set the atom to the most common form of the current element (will be used for the Periodic Table)
        public void ForceToCommon(int protonCount)
        {
            // add or remove neutrons to match atomic number
            int protonDiff = protonCount - Nucleus.ProtonCount; // get the difference in protons
            workbench.NewAutoProton(protonDiff); // add protons to match atomic number
            Nucleus.TrimProtons(-protonDiff); // remove protons to match atomic number

            // get the most common stable form of the element
            Element element = Elements.GetElement(protonCount);

            if (element != null) // if the element is not null
            {
                // set the minimum and maximum isotope mass
                Nucleus.MassMax = element.MaxIsotope;
                Nucleus.MassMin = element.MinIsotope;

                Isotope common = element.GetCommon(); // get the most common isotope of the element

                if (common != null) // if the isotope is not null
                { 
                    // add or remove neutrons to match mass
                    int neutronDiff = (common.Mass - protonCount) - Nucleus.NeutronCount; // get the difference in neutrons
                    workbench.NewAutoNeutron(neutronDiff); // add neutrons to match mass
                    Nucleus.TrimNeutrons(-neutronDiff); // remove neutrons to match mass
                }            
            }

            // add shells to match element period
            //while (shells.Count < Elements.GetShells(protonCount))
            //{
            //    AddShell();
            //}

            // remove shells to match element period
            while (shells.Count > Elements.GetShells(protonCount))
            {
                RemoveShell();
            }

            // add or remove electrons to match charge
            int electronDiff = protonCount - ElectronCount;
            workbench.NewAutoElectron(electronDiff);
            OuterShell.TrimElectrons(-electronDiff);

            AdjustScale();
        }

        public void AdjustScale() // adjust the scale of the atom
        {
            // calculate scale = maxRadius / baseRadius 
            float minAxis = Mathf.Min(anchor.Bounds.extents.x, anchor.Bounds.extents.y); // get the minimum axis of the atom
            scale = Mathf.Min(1, (minAxis * 0.9f) / CalcRadius(shells.Count, 1)); // calculate the scale

            // set the scale of the nucleus
            Nucleus.Scale = scale;

            // update shells to match scale
            int num = shells.Count;
            foreach (ShellLvl1 shell in shells)
            {
                // set the scale
                shell.Scale = scale;
                // set the radius
                shell.Radius = CalcRadius(num, scale);
                num--;
            }
        }

        /// Helper method for adjusting scale
        private float CalcRadius(int num, float scale) // calculate the radius of a shell at a given scale
        {
            float nucleusRadius = Mathf.Log(Nucleus.Mass , 30 / scale) * scale + (scale / 2); // calculate the radius of the nucleus at the scale
            float shellRadiusDifference = scale * num; // calculate the difference in radius between the nucleus and the shell

            return nucleusRadius + shellRadiusDifference;
        }
    }
}