using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace DUI
{

    public class DUISphereButton : MonoBehaviour
    {
        private float radius = 0;
        public float Radius { set { radius = value; } }

        public static DUISphereButton closest = null;

        private bool active = true; //true when the button is able to be clicked 
        private bool wasOver = false; //true when the mouse is over bounds

        public UnityEvent OnEnter; //called when the mouse enters bounds
        public UnityEvent OnClick; //called when the mouse clicks while in bounds
        public UnityEvent OnExit; //called when the mouse exits bounds

        protected virtual void Awake()
        {
            OnEnter.AddListener(Enter);
            OnExit.AddListener(Exit);
        }

        protected virtual void Update()
        {
            //only check for events when active

            if (active)
            {
                //over when diff to mouse position is smaller than radius
                bool over = ((Vector2)transform.position - (DUI.inputPos)).sqrMagnitude < radius * radius;
                //call enter when mouse is first over
                if (!wasOver && over)
                {
                    if (closest == null)
                    {
                        OnEnter?.Invoke();
                    }
                    else if (transform.position.z < closest.transform.position.z)
                    {
                        closest.OnExit?.Invoke();
                        OnEnter?.Invoke();
                    }
                   
                }
                //call exit when mouse first leaves
                else if (wasOver && !over)
                {
                    OnExit?.Invoke();
                }
                //call click when over and mouse down
                if (wasOver && closest.Equals(this) &&Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke();
                }
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
            closest = this;
        }

        private void Exit()
        {
            wasOver = false;
            closest = null;
        }


    }
}
