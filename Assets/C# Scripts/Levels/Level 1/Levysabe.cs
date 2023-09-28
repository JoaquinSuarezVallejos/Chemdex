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
        public bool win = false;
        bool listIsNull = false;
        bool listModified = false;
        private bool protonScript;
        private bool neutronScript;
        [SerializeField] ProtonLvl1 protonLvl1;
        [SerializeField] NeutronLvl1 neutronLvl1;

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
        }
        // Update is called once per frame

        public void AddParticle(GameObject particleDropped, ParticleTaker script)
        {
            if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un nuetron.
            {
                //foreach (ParticleLvl1 other in actual) //Recorro la lista
                for (int i = 0; i < actual.Count; i++)
                {
                    if(actual[i] != null)
                    {
                        if (actual[i].GetType() == particleDropped.GetComponent<NeutronLvl1>().GetType()) //Si el objeto que se dropeó está en la lista:
                        {
                            Debug.Log("Coincide alguna con la receta");
                            script.wasCorrect = true;
                            //actual.Remove(other); //se borra
                            actual[i] = null;
                            break;
                        }
                    }
                }
            }
            if (particleDropped.name == "Proton Level 1(Clone)" && particleDropped.GetComponent<ProtonLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un proton.
            {
                //foreach (ParticleLvl1 other in actual)//Recorro la lista
                for(int i = 0; i < actual.Count; i++)
                {
                    if(actual[i] != null)
                    {
                        if (actual[i].GetType() == particleDropped.GetComponent<ProtonLvl1>().GetType())//Si el objeto que se dropeó está en la lista:
                        {
                            Debug.Log("Coincide alguna con la receta");
                            script.wasCorrect = true;
                            //actual.Remove(other);//se borra
                            actual[i] = null;
                            break;
                        }
                    }
                }
            }
            foreach (ParticleLvl1 scrit in actual)
            {
                if (scrit != null)
                {
                    listIsNull = false;
                    break;
                }
                else
                {
                    listIsNull = true;
                }
            }
            if (listIsNull)
            {
                win = true;
            }
        }

        public void CheckList(bool wasCorrect, GameObject particleOUT)
        {
            neutronScript = particleOUT.GetComponent<NeutronLvl1>();
            protonScript = particleOUT.GetComponent<ProtonLvl1>();

            if (actual.Count == receta.Length)
            {
                for (int i = 0; i < receta.Length; i++)
                {
                    if (actual[i] == receta[i])
                    {
                    }
                    else if (actual[i] != receta [i] && !listModified)
                    {
                        ResetearLista(wasCorrect);
                    }
                }
                listModified = false;
            }
            else
            {
                ResetearLista(wasCorrect);
            }
        }

        void ResetearLista(bool wasCorrect)
        {
            if (protonScript)
            {
                if (wasCorrect && !listModified)
                {
                    for (int i = 0; i < receta.Length; i++)
                    {
                        if (actual[i] != receta[i] && !listModified && receta[i].GetType() == protonLvl1.GetType())
                        {
                            actual[i] = receta[i];
                            listModified = true;
                        }
                    }
                }
            }

            else if (neutronScript)
            {
                if (wasCorrect && !listModified)
                {
                    for (int i = 0; i < receta.Length; i++)
                    {
                        if (actual[i] != receta[i] && !listModified && receta[i].GetType() == neutronLvl1.GetType())
                        {
                            actual[i] = receta[i];
                            listModified = true;
                        }
                    }
                }
            }
        }
    }

}
