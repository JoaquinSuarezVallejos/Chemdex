using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public class Levysabe : MonoBehaviour
    {
        #region Win or Lose
        public bool win = false;
        public bool lose = false;
        bool listIsNull = false;
        bool holdersAreFull = false;
        [SerializeField] GameObject[] holders;
        #endregion
        #region Ideal molecule
        public ParticleLvl1[] receta;
        public Transform[] position;
        [SerializeField] List<ParticleLvl1> actual;
        bool listModified = false;
        private bool protonScript;
        private bool neutronScript;
        private bool thirdScript;
        [SerializeField] ProtonLvl1 protonLvl1;
        [SerializeField] NeutronLvl1 neutronLvl1;
        [SerializeField] ThirdParticleLvl1 thirdParticleLvl1;
        #endregion

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
            holders = GameObject.FindGameObjectsWithTag("Holder");
        }

        private void Update()
        {
            #region Winning Check
            foreach (ParticleLvl1 scrit in actual)
            {
                if (scrit != null)
                {
                    listIsNull = false;
                    break;
                }
                else //if all 3 are null you win.
                {
                    listIsNull = true;
                }
            }
            if (listIsNull)
            {
                win = true;
            }
            #endregion
            #region Losing Check
            foreach (GameObject holder in holders)
            {
                if (!holder.GetComponent<ParticleTaker>().isOccupied)
                {
                    holdersAreFull = false;
                    break;
                }
                else
                {
                    holdersAreFull = true;
                }
            }
            if (holdersAreFull)
            {
                lose = true;
            }
            #endregion
        }

        public void AddParticle(GameObject particleDropped, ParticleTaker script)
        {
            #region Is a Neutron?
            if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un nuetron.
            {
                //foreach (ParticleLvl1 other in actual) //Recorro la lista
                for (int i = 0; i < actual.Count; i++)
                {
                    if (actual[i] != null)
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
            #endregion
            #region Is a Proton?
            if (particleDropped.name == "Proton Level 1(Clone)" && particleDropped.GetComponent<ProtonLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un proton.
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    if (actual[i] != null)
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
            
            #endregion
            #region Is a Third Particle?
            if (particleDropped.name == "Third Particle Level(Clone)" && particleDropped.GetComponent<ThirdParticleLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un third particle.
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    if (actual[i] != null)
                    {
                        if (actual[i].GetType() == particleDropped.GetComponent<ThirdParticleLvl1>().GetType())//Si el objeto que se dropeó está en la lista:
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
            #endregion

        }

        public void CheckList(bool wasCorrect, GameObject particleOUT)
        {
            #region Wich particle type?
            neutronScript = particleOUT.GetComponent<NeutronLvl1>();
            protonScript = particleOUT.GetComponent<ProtonLvl1>();
            thirdScript = particleOUT.GetComponent<ThirdParticleLvl1>();
            #endregion
            #region Reset recipe if list was modified
            if (actual.Count == receta.Length)
            {
                for (int i = 0; i < receta.Length; i++)
                {
                    if (actual[i] != receta [i] && !listModified)
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
            #endregion
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

            else if (thirdScript)
            {
                if (wasCorrect && !listModified)
                {
                    for (int i = 0; i < receta.Length; i++)
                    {
                        if (actual[i] != receta[i] && !listModified && receta[i].GetType() == thirdParticleLvl1.GetType())
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
