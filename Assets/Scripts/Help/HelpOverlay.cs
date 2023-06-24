using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Help
{
    public class HelpOverlay : MonoBehaviour
    {
        private List<HelpDescription> helpDescriptions;

        [SerializeField] private GameObject infoUI;
        [SerializeField] private Text infoName;
        [SerializeField] private Text infoDesc;


        private void Awake()
        {
            helpDescriptions = new List<HelpDescription>();
            helpDescriptions.AddRange(GetComponentsInChildren<HelpDescription>());

            foreach(HelpDescription helpDescription in helpDescriptions)
            {
                helpDescription.Button.onClick.AddListener(() => ShowInfoPanel(helpDescription.Data));
            }

            gameObject.SetActive(false);
        }

        public void ShowInfoPanel(string[] descData)
        {
            infoUI.SetActive(true);
            infoName.text = descData[0];
            infoDesc.text = descData[1];
        }

        public void HideInfoPanel()
        {
            infoUI.SetActive(false);
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            HideInfoPanel();
        }
    }

}
