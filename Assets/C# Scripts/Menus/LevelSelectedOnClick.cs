using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectedOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
     //ver si la linea del start va aca o si tengo que poner un addlistener.
     //Ademas ver como llamar una función una vez que el botón está clickeado. porque ponerlo desde inspector no sirve porque se borran cuando se cambia de escena.
    }

}
