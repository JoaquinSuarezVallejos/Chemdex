using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Levysabe : MonoBehaviour
    {
        // Start is called before the first frame update
        public ParticleLvl1[] receta;
        public Transform[] position;
        List<ParticleLvl1> actual;
        [SerializeField] ProtonLvl1 protonlvl1;

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
        }
        // Update is called once per frame

        public bool AddParticle(ParticleLvl1 atom)
        {
            if (actual.Contains(atom) && actual.Count > 0)
            {
                actual.Remove(atom);
            }
            if(actual.Count == 0)
            {
                Debug.Log("win");
                return true;
            }
            return false;
        }
    }

}
