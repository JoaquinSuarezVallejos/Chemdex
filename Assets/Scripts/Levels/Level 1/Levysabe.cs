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
        ProtonLvl1 protonScript;
        public bool win = false;
        //[SerializeField] ProtonLvl1 protonlvl1;

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
        }
        // Update is called once per frame

        public void AddParticle(GameObject particleDropped)
        // Le pasaba ParticleLvl1 atom por parentesis
        {
            if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un nuetron.
            {
                foreach (ParticleLvl1 other in actual)
                {
                    //Debug.Log(other);
                    if (other.GetType() == particleDropped.GetComponent<NeutronLvl1>().GetType())
                    {
                        Debug.Log("Coincide alguna con la receta");
                        actual.Remove(other);
                        break;
                    }
                }
            }
            if (particleDropped.name == "Proton Level 1(Clone)" && particleDropped.GetComponent<ProtonLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un proton.
            {
                foreach (ParticleLvl1 other in actual)
                {
                    if (other.GetType() == particleDropped.GetComponent<ProtonLvl1>().GetType())
                    {
                        Debug.Log("Coincide alguna con la receta");
                        actual.Remove(other);
                        break;
                    }
                }
            }
            //if (actual is atom && actual.Count > 0)
            //{
            //    actual.Remove(atom);
            //}
            if (actual.Count == 0)
            {
                win = true;
                //return true;
            }
            //return false;
        }
    }

}
