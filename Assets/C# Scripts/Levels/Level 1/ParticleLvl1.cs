﻿using UnityEngine;
using UnityEngine.Events;
using Physics;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(PhysicsObject))] // requires a physics object component
    [RequireComponent(typeof(DUISphereButton))] // requires a sphere button component
    public class ParticleLvl1 : MonoBehaviour // subatomic particles: proton, neutron and electron
    {
        private DUISphereButton sphereButton; // the sphere button component
        private const int releaseSpeed = 20; // speed of release of particles
        protected bool inAtom = false; // true when the particle is in the atom
        public bool selected = false; // true when the particle is currently selected

        public UnityEvent OnSelect; // called when the particle is first selected 
        public UnityEvent OnDeselect; // called when the particle is released from selection

        protected static AtomLvl1 atom; // the atom the particle is in

        public float Radius // get and set the radius in Unity units
        {
            get { return transform.localScale.x / 2; }
            set { transform.localScale = Vector3.one * 2.0f * value; }
        }

        public PhysicsObject PhysicsObj { get; private set; } // the physics object component

        protected virtual void Awake()
        {
            PhysicsObj = GetComponent<PhysicsObject>(); // get the physics object component
            sphereButton = GetComponent<DUISphereButton>(); // get the sphere button component

            sphereButton.Radius = Mathf.Max(0.5f, Radius); //set sphere button to have the same radius as the particle

            // get the satic reference to the atom
            if (atom == null)
            {
                atom = FindObjectOfType<AtomLvl1>();

                if (atom == null)
                {
                    throw new System.Exception("An Atom class is needed");
                }
            }

            // hook up events 
            sphereButton.OnClick.AddListener(Select); // select the particle when the sphere button is clicked
            OnSelect.AddListener(Select); // select the particle when the OnSelect event is called
            OnDeselect.AddListener(Deselect); // deselect the particle when the OnDeselect event is called
        }

        protected virtual void Update()
        {
            // behavior when the particle is selected by the user
            if (selected)
            {
                transform.position = (Vector3)DUI.DUI.inputPos + Vector3.back; // move the particle to the mouse position

                if (Input.GetMouseButtonUp(0)) // check if the user released the mouse button
                {
                    OnDeselect?.Invoke(); // call the OnDeselect event
                }
            }
        }

        protected void Select()
        {
            PickUpParticle(); // run the pickup particle behavior

            selected = true;
        }

        protected void Deselect()
        {
            DropParticle();  // run the drop particle behavior

            PhysicsObj.AddForce((DUI.DUI.inputPos - DUI.DUI.inputPosPrev) * releaseSpeed); // add a force to the particle
            selected = false;
        }

        protected virtual void DropParticle() // Behavior for when the particle is dropped into the atom
        {
            inAtom = true;
        }

        protected virtual void PickUpParticle() // Behavior for when the particle is picked up out of the atom
        {
            inAtom = false;
        }
    }
}