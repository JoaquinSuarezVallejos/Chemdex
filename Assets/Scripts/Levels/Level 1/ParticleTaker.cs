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
        }
        private void OnCollisionEnter(Collision coll)
        {
            var part = coll.gameObject.GetComponent<ParticleLvl1>();
            if (part && !fed)
            {
                Debug.Log(part.GetType());
                Debug.Log(assignedParticle.GetType());
                if(part.GetType() == assignedParticle.GetType())
                {
                    Debug.Log("eat");
                    fed = true;
                    coll.transform.parent = transform;
                    if(parent.AddParticle(part))
                    {
                        Debug.Log("win");
                    }
                    Destroy(this);
                }
            }
        }
    }
    
}
