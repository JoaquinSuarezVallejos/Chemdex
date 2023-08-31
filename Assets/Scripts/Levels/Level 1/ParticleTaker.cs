using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class ParticleTaker : MonoBehaviour
    {
        Levysabe parent;
        public ParticleLvl1 assignedParticle;
        bool fed = false;

        private void Awake()
        {
            parent = GetComponentInParent<Levysabe>();
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

        private void OnCollisionEnter(Collision coll)
        {
            var part = coll.gameObject.GetComponent<ParticleLvl1>();
            if (part && !fed)
            {
                if(part.GetType() == assignedParticle.GetType())
                {
                    fed = true;
                    coll.transform.parent = transform;
                    parent.AddParticle(coll.gameObject);
                    Debug.Log("Se destruyo alguno");
                    Destroy(this);
                }
            }
        }
    }
    
}
