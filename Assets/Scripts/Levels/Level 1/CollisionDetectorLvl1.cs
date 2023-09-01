using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Physics;

public class CollisionDetectorLvl1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionExit(Collision coll)
    {
        Debug.Log("salio");
    }

    private void Update()
    {
        //if (Physics.OverlapSphere() > 0)
        //{

        //}
    }
}
