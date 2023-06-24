using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DUI
{
    [RequireComponent(typeof(DUIAnchor))]
    public class DUIButton : MonoBehaviour
    {
        /// <summary>
        /// sends out events when mouse enters, clicks, and exits DUIanchor bounds
        /// </summary>

        private DUIAnchor anchor; //ref to spriteRenderer component

        private bool active = true; //true when the button is able to be clicked 
        private bool wasOver = false; //true when the mouse is over bounds

        public UnityEvent OnEnter; //called when the mouse enters bounds
        public UnityEvent OnClick; //called when the mouse clicks while in bounds
        public UnityEvent OnExit; //called when the mouse exits bounds

        protected virtual void Awake()
        {
            anchor = GetComponent<DUIAnchor>();

            OnEnter.AddListener(Enter);
            OnExit.AddListener(Exit);
        }

        protected virtual void Update()
        {
            //only check for events when active

//#if UNITY_EDITOR || UNITY_STANDALONE
            if (active)
            {
                bool over = anchor.Bounds.Contains(DUI.inputPos);
                //call enter when mouse is first over
                if (!wasOver && over)
                {
                    OnEnter?.Invoke();
                }
                //call exit when mouse first leaves
                else if (wasOver && !over)
                {
                    OnExit?.Invoke();
                }
                //call click when over and mouse down
                if (wasOver && Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke();
                }
                /*
#elif UNITY_ANDROID || UNITY_IOS
            if (active && Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                bool over = anchor.Bounds.Contains(DUI.inputPos);
                //call enter when mouse is first over
                if (!wasOver && over)
                {
                    OnEnter?.Invoke();
                }
                //call exit when mouse first leaves
                else if (wasOver && (!over || touch.phase == TouchPhase.Ended))
                {
                    OnExit?.Invoke();
                }
                if (wasOver && touch.phase == TouchPhase.Began)
                {
                    OnClick?.Invoke();
                }
#endif*/
            }
            //call exit if disabled with mouse over
            else if (wasOver)
            {
                OnExit?.Invoke();
            }
        }

        private void Enter()
        {
            wasOver = true;
        }

        private void Exit()
        {
            wasOver = false;
        }
    }
}
