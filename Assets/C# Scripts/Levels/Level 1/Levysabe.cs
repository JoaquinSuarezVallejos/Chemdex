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
        [SerializeField] ProtonLvl1 protonlvl1;
        [SerializeField] NeutronLvl1 neutronLvl1;

        private void Awake()
        {
            actual = new List<ParticleLvl1>(receta);
        }
        // Update is called once per frame

        public void AddParticle(GameObject particleDropped)
        {
            if (particleDropped.name == "Neutron Level 1(Clone)" && particleDropped.GetComponent<NeutronLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un nuetron.
            {
                foreach (ParticleLvl1 other in actual) //Recorro la lista
                {
                    if (other.GetType() == particleDropped.GetComponent<NeutronLvl1>().GetType()) //Si el objeto que se dropeó está en la lista:
                    {
                        Debug.Log("Coincide alguna con la receta");
                        actual.Remove(other); //se borra
                        break;
                    }
                }
            }
            if (particleDropped.name == "Proton Level 1(Clone)" && particleDropped.GetComponent<ProtonLvl1>() != null && actual.Count > 0) //El objeto dropeado tiene un proton.
            {
                foreach (ParticleLvl1 other in actual)//Recorro la lista
                {
                    if (other.GetType() == particleDropped.GetComponent<ProtonLvl1>().GetType())//Si el objeto que se dropeó está en la lista:
                    {
                        Debug.Log("Coincide alguna con la receta");
                        actual.Remove(other);//se borra
                        break;
                    }
                }
            }
            if (actual.Count == 0)
            {
                win = true;
            }
        }

        public void CheckList()
        {
            if (actual.Count == receta.Length)
            {
                for (int i = 0; i < receta.Length; i++)
                {
                    if (actual[i] == receta[i])
                    {
                        Debug.Log("son iguales");
                    }
                    else
                    {
                        Debug.Log("no iguales");
                    }
                }
            }
            else
            {
                for (int i = 0; i<actual.Count; i++)
                {
                    if (actual[i] != receta[i]) //VER COMO MIERDA CHEQUEAR LA RECETA Y ACTUAL.
                    {
                        break;
                    }
                    Debug.Log(receta[i]);
                }
                //actual.Add(script);
            }
        }
    }

}
