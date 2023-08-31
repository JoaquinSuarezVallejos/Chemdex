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
        [SerializeField] List<ParticleLvl1> actual;
        //[SerializeField] ProtonLvl1 protonlvl1;

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
        }
        // Update is called once per frame

        public void AddParticle(GameObject particleDropped)
        // Le pasaba ParticleLvl1 atom por parentesis
        {
            Debug.Log("Se agrego particula random");
            foreach (ParticleLvl1 script in actual)
            {
                if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0)
                {
                    Debug.Log(particleDropped);
                    Debug.Log("Coincide alguna con la receta");
                    actual.Remove(script);
                    break;
                }
                if (particleDropped.name == "Proton Level 1(Clone)" && particleDropped.GetComponent<ProtonLvl1>() != null && actual.Count > 0)
                {
                    Debug.Log(particleDropped);
                    Debug.Log("Coincide alguna con la receta");
                    actual.Remove(script);
                    break;
                }
            }
            //if (actual is atom && actual.Count > 0)
            //{
            //    actual.Remove(atom);
            //}
            if(actual.Count == 0)
            {
                Debug.Log("win");
                //return true;
            }
            //return false;
        }
    }

}
