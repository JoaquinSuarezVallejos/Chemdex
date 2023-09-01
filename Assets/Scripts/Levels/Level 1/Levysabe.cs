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
            if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un nuetron.
            {
                foreach (ParticleLvl1 other in actual)
                {
                    //Debug.Log(other);
                    if (other == particleDropped.GetComponent("NeutronLvl1"))
                    {
                        Debug.Log(particleDropped);
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
                    Debug.Log(other + "este script esta debugeado");
                    Debug.Log(particleDropped.GetComponent(typeof(ProtonLvl1)) as ProtonLvl1);
                    protonScript = particleDropped.GetComponent<ProtonLvl1>();
                    if (other == protonScript)
                    {
                        Debug.Log(particleDropped);
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
                Debug.Log("win");
                //return true;
            }
            //return false;
        }
    }

}
