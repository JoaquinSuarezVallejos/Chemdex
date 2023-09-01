using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class ParticleTaker : MonoBehaviour
    {
        Levysabe parent;
        public ParticleLvl1 assignedParticle;
        public bool fed = false;
        GameObject particleCollision;
        bool particleAdded = false;

        private void Awake()
        {
            parent = GetComponentInParent<Levysabe>();
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

        private void OnCollisionEnter(Collision coll)
        {
            var part = coll.gameObject.GetComponent<ParticleLvl1>();
            particleCollision = coll.gameObject;
            if (!fed)
            {
                if(part.GetType() == assignedParticle.GetType())
                {
                    fed = true;
                }
            }
        }

        private void OnCollisionExit(Collision coll)
        {
            fed = false;
        }

        private void Update()
        {
            if (particleCollision != null)
            {
                if (!particleCollision.GetComponent<ParticleLvl1>().selected && fed && !particleAdded)
                {
                    particleCollision.transform.parent = transform;
                    parent.AddParticle(particleCollision);
                    Debug.Log("Se desactivo alguno");
                    enabled = false;
                    particleAdded = true;
                }
            }

            if (!fed)
            {
                //se vuelve a activar y se ejecuta una funcion que no se que hace, pero seguro que se necesita para algo, tipo pickupparticle o algo asi.
                particleAdded = false;
            }
        }
    }
    
}
