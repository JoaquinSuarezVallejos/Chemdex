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
        private void FixedUpdate()
        {
            if(particleCollision)
            {
                if (!particleCollision.GetComponent<ParticleLvl1>().selected && !particleAdded)
                {
                    if (transform.childCount > 1)
                    {
                        Debug.Log("No puede entrar más de una particula por espacio");
                    }
                    else
                    {
                        particleCollision.transform.parent = gameObject.transform;
                        particleCollision.transform.position = gameObject.transform.position;
                        if (particleCollision.GetComponent<ParticleLvl1>().GetType() == assignedParticle.GetType() && !fed)
                        {
                            parent.AddParticle(particleCollision);
                            enabled = false;
                            particleAdded = true;
                            fed = true;
                        }
                    }
                }
            }
        }
        private void OnCollisionEnter(Collision coll)
        {
            var part = coll.gameObject.GetComponent<ParticleLvl1>();
            particleCollision = coll.gameObject;
            
        }

        private void OnCollisionExit(Collision coll)
        {
            Debug.Log("salio");
            fed = false;
            particleCollision.gameObject.transform.parent = null;
        }

        private void Update()
        {

            if (!fed)
            {
                //se vuelve a activar y se ejecuta una funcion que no se que hace, pero seguro que se necesita para algo, tipo pickupparticle o algo asi.
                particleAdded = false;
                enabled = true;
                //para sacarle el child, moverlo de vuelta a su padre correspondiente. Si es un proton a proton marker, si es un neutron a neutron marker.
            }
        }
    }
    
}
