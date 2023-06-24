using UnityEngine;
using UnityEngine.Events;
using Physics;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))]
    [RequireComponent(typeof(DUISphereButton))]
    public abstract class Particle : MonoBehaviour
    {
        private DUISphereButton sphereButton; //ref to attached DUI sphere collider 
        private const int releaseSpeed = 20;

        protected bool inAtom = false; //internally true when part of the atom
        protected bool selected = false; //true when the particle is currently selected

        public UnityEvent OnSelect; //called when the particle is first selected 
        public UnityEvent OnDeselect; //called when the particle is released from selection

        protected static Atom atom; //static ref to the Atom

        //get and set the radius in Unity Units
        public float Radius {
            get { return transform.localScale.x / 2; }
            set { transform.localScale = Vector3.one * 2.0f * value; }
        }

        public PhysicsObject PhysicsObj { get; private set; }

        protected virtual void Awake()
        {
            //find components
            PhysicsObj = GetComponent<PhysicsObject>();
            sphereButton = GetComponent<DUISphereButton>();

            //set spherebutton to have same radius as particle
            sphereButton.Radius = Mathf.Max(0.5f, Radius);

            //get the satic reference to the atom
            if(atom == null)
            {
                atom = FindObjectOfType<Atom>();
                if (atom == null)
                {
                    throw new System.Exception("An Atom class is needed");
                }
            }

            //hook up events 
            sphereButton.OnClick.AddListener(Select);
            OnSelect.AddListener(Select);
            OnDeselect.AddListener(Deselect);
        }

        protected virtual void Update()
        {
            //behavior when particle is selected by the user
            if (selected)
            {
                //move to mouse position 
                transform.position = (Vector3)DUI.DUI.inputPos + Vector3.back;

                //call deselect when intut released
                if (Input.GetMouseButtonUp(0))
                {
                    OnDeselect?.Invoke();
                }
            }
        }

        protected void Select()
        {
            //run the pickup particle behavior
            PickUpParticle();

            selected = true;
        }

        protected void Deselect()
        {
            //run the drop particle behavior
            DropParticle();

            PhysicsObj.AddForce((DUI.DUI.inputPos - DUI.DUI.inputPosPrev) * releaseSpeed);
            selected = false;
        }

        /// <summary>
        /// Behavior for when the particle is dropped into the atom
        /// </summary>
        protected virtual void DropParticle()
        {
            inAtom = true;
        }

        /// <summary>
        /// Behavior for when the particle is picked up out of the atom
        /// </summary>
        protected virtual void PickUpParticle()
        {
            inAtom = false;
        }

    }
}
