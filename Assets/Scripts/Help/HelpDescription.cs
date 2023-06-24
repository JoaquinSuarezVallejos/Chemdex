using UnityEngine;
using UnityEngine.UI;

namespace Help
{
    [RequireComponent(typeof(Button))]
    public class HelpDescription : MonoBehaviour
    {
        [SerializeField] private string displayName;
        [SerializeField] [TextArea] private string description;

        public string[] Data { get { return new string[] { displayName, description }; } }
        public Button Button { get { return GetComponent<Button>(); } }

        private void Awake()
        {
            GetComponentInChildren<Text>().text = displayName;
        }
    }
}

