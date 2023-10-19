using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class ParticleTaker : MonoBehaviour
    {
        Levysabe parent;
        public ParticleLvl1 assignedParticle;
        public GameObject particleGO;
        public bool wasCorrect = false;
        public bool isOccupied = false;

        private void Awake()
        {
            parent = GetComponentInParent<Levysabe>();
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        private void OnCollisionStay(Collision coll)
        {
            var part = coll.gameObject.GetComponent<ParticleLvl1>();
            if (!part.selected && particleGO == null)
            {
                if (transform.childCount > 1)
                {
                    Debug.Log("No puede entrar más de una particula por espacio");
                }
                else
                {
                    Debug.Log("puede entrar la particula");
                    particleGO = part.gameObject;
                    particleGO.transform.parent = gameObject.transform;
                    isOccupied = true;
                    //particleGO.transform.position = gameObject.transform.position;
                    if (part.GetType() == assignedParticle.GetType())
                    {
                        parent.AddParticle(particleGO, this);
                    }
                }
            }
        }

        //private void OnCollisionExit(Collision coll)
        //{
        //    Debug.Log("salio");
        //    fed = false;
        //    particleCollision.gameObject.transform.parent = null;
        //}

        private void Update()
        {
            if (particleGO != null)
            {
                particleGO.transform.position = Vector3.Lerp(particleGO.transform.position, transform.position, Time.deltaTime * 3f);
                if (Vector3.Distance(transform.position, particleGO.transform.position) > 1 && !particleGO.GetComponent<ParticleLvl1>().selected)
                {
                    particleGO.transform.parent = null;
                    if (particleGO.GetComponent<NeutronLvl1>() != null)
                    {
                        parent.CheckList(wasCorrect, particleGO);
                    }
                    else if (particleGO.GetComponent<ProtonLvl1>() != null)
                    {
                        parent.CheckList(wasCorrect, particleGO);
                    }
                    //Destroy(particleGO.gameObject); Si la destruyo se destruye el script.
                    wasCorrect = false;
                    particleGO = null;
                    isOccupied = false;
                }
            }

            if (isOccupied)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    
}
