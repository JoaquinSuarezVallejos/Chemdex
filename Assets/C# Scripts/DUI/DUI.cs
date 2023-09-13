using UnityEngine;

namespace DUI
{
    public class DUI : MonoBehaviour
    {
        /// <summary>
        /// handles the global DUI variables
        /// </summary>

        public static float cameraHeight; //height of the screen in Unity units
        public static float cameraWidth; //width of the screen in Unity Units 

        public static Vector2 inputPos; //position of the mouse or touch in Unity Unitys
        public static Vector2 inputPosPrev; //position of the mouse or touch in Unity Units last frame

        private void Awake()
        {
            Camera cam = Camera.main;

            //Orthographic setup
            if (cam.orthographic)
            {
                cameraHeight = cam.orthographicSize;
            }
            //Perspective setup
            else
            {
                //make sure vertical fov is 60
                cam.fieldOfView = 60;
                cameraHeight = (transform.position.z - cam.transform.position.z) / Mathf.Sqrt(3);
            }

            //calculate width based on height
            cameraWidth = cameraHeight * Screen.width / Screen.height;
        }

        public static bool Contains(Vector2 pos)
        {
            return Mathf.Abs(pos.x) < cameraWidth && Mathf.Abs(pos.y) < cameraHeight;
        }

        private void Update()
        {
            //set the previous input position
            inputPosPrev = inputPos;

            //only need to calculate input position once
#if UNITY_EDITOR || UNITY_STANDALONE
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
            inputPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif  
        }

    }
}
