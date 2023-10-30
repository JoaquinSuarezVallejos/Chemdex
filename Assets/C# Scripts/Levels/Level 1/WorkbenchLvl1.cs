using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;
namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class WorkbenchLvl1 : MonoBehaviour
    {

        [SerializeField] private GameObject ProtonPrefab;
        [SerializeField] private GameObject NeutronPrefab;
        [SerializeField] private GameObject ElectronPrefab;
        [SerializeField] private GameObject ThirdParticlePrefab;
        [Space(5)]
        [SerializeField] private GameObject ProtonMarker;
        [SerializeField] private GameObject NeutronMarker;
        [SerializeField] private GameObject ElectronMarker;

        private ParticleCounterManager particleCounter;

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

        private void Start()
        {
            particleCounter = GameObject.Find("ParticleCounterManager").GetComponent<ParticleCounterManager>();
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

        public void NewProton()
        {
            GameObject obj = Instantiate(ProtonPrefab, transform.GetChild(0));

            ProtonLvl1 proton = obj.GetComponent<ProtonLvl1>();
            if (proton != null)
            {
                proton.OnSelect?.Invoke();
            }

            particleCounter.newProtonOnScreen(obj);
        }

        public void NewNeutron()
        {
            GameObject obj = Instantiate(NeutronPrefab, transform.GetChild(1));

            NeutronLvl1 neutron = obj.GetComponent<NeutronLvl1>();
            if (neutron != null)
            {
                neutron.OnSelect?.Invoke();
            }

            particleCounter.newNeutronOnScreen(obj);
        }

        public void NewElectron()
        {
            GameObject obj = Instantiate(ElectronPrefab, transform.GetChild(2));

            ElectronLvl1 electron = obj.GetComponent<ElectronLvl1>();
            if (electron != null)
            {
                electron.OnSelect?.Invoke();
            }
        }

        public void NewThirdParticle()
        {
            GameObject obj = Instantiate(ThirdParticlePrefab, GameObject.Find("Third Marker").transform);

            ThirdParticleLvl1 third = obj.GetComponent<ThirdParticleLvl1>();
            if (third != null)
            {
                third.OnSelect?.Invoke();
            }

            //llamo a que se agregó un nuevo thirdParticle.
        }
    }
}