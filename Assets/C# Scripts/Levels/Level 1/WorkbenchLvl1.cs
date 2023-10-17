using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;
namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class WorkbenchLvl1 : MonoBehaviour
    {
        /// <summary>
        /// handles the 
        /// </summary>

        [SerializeField] private GameObject ProtonPrefab;
        [SerializeField] private GameObject NeutronPrefab;
        [SerializeField] private GameObject ElectronPrefab;
        [SerializeField] private GameObject ThirdParticlePrefab;
        [Space(5)]
        [SerializeField] private GameObject ProtonMarker;
        [SerializeField] private GameObject NeutronMarker;
        [SerializeField] private GameObject ElectronMarker;

        //private AtomLvl1 atom;

        private void Awake()
        {
            //atom = FindObjectOfType<AtomLvl1>();

            //make sure there are at least 3 children
            if (transform.childCount >= 3)
            {
                GameObject proton = Instantiate(ProtonMarker, transform.GetChild(0));
                proton.transform.localPosition = Vector3.zero;

                GameObject neutron = Instantiate(NeutronMarker, transform.GetChild(1));
                neutron.transform.localPosition = Vector3.zero;

                GameObject electron = Instantiate(ElectronMarker, transform.GetChild(2));
                electron.transform.localPosition = Vector3.zero;
            }
        }

        public void NewAutoProton(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                Debug.Log("New Proton");
                Instantiate(ProtonPrefab, transform.GetChild(0)).GetComponent<ProtonLvl1>().OnDeselect?.Invoke();
            }
        }

        public void NewAutoNeutron(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                Debug.Log("New Neutron");
                Instantiate(NeutronPrefab, transform.GetChild(1)).GetComponent<NeutronLvl1>().OnDeselect?.Invoke();
            }
        }

        public void NewAutoElectron(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                Debug.Log("New Electron");
                Instantiate(ElectronPrefab, transform.GetChild(2)).GetComponent<ElectronLvl1>().OnDeselect?.Invoke();
            }
        }
        /// <summary>
        /// create a new proton
        /// </summary>
        public void NewProton()
        {
            //Debug.Log("New Proton");

            GameObject obj = Instantiate(ProtonPrefab, transform.GetChild(0));

            ProtonLvl1 proton = obj.GetComponent<ProtonLvl1>();
            if (proton != null)
            {
                proton.OnSelect?.Invoke();
            }
        }

        /// <summary>
        /// create a new neutron
        /// </summary>
        public void NewNeutron()
        {
            Debug.Log("New Neutron");

            GameObject obj = Instantiate(NeutronPrefab, transform.GetChild(1));

            NeutronLvl1 neutron = obj.GetComponent<NeutronLvl1>();
            if (neutron != null)
            {
                neutron.OnSelect?.Invoke();
            }
        }

        public void NewElectron()
        {
            Debug.Log("New Electron");

            GameObject obj = Instantiate(ElectronPrefab, transform.GetChild(2));

            ElectronLvl1 electron = obj.GetComponent<ElectronLvl1>();
            if (electron != null)
            {
                electron.OnSelect?.Invoke();
            }
        }

        public void NewThirdParticle()
        {
            Debug.Log("New Third Particle");

            GameObject obj = Instantiate(ThirdParticlePrefab, GameObject.Find("Third Marker").transform);

            ThirdParticleLvl1 third = obj.GetComponent<ThirdParticleLvl1>();
            if (third != null)
            {
                third.OnSelect?.Invoke();
            }
        }
    }
}