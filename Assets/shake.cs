using Atom;
using Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;

    [SerializeField] WinManagerLvl1 winManager;
    bool alreadyShaking = false;

    private void Awake()
    {
        _startPos = transform.position;
        alreadyShaking = false;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin()
    {
        alreadyShaking = true;
        Debug.Log("shikaing");
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        transform.position = _startPos;
    }

    private void Update()
    {
        if (winManager.shaking && !alreadyShaking)
        {
            Begin();
        }

        else if (!winManager.shaking)
        {
            StopCoroutine(Shake());
        }
    }

}
//{
//    [SerializeField] WinManagerLvl1 winManager;

//    Vector3 origin;

//    private PhysicsObject physicsObject;

//    private void Start()
//    {
//        origin = transform.localPosition;
//    }

//    private void Update()
//    {
//        if (winManager.shaking)
//        {
//            Vector3 forceToOrigin = origin - transform.localPosition; // calculate the force to the origin

//            Vector3 forceToShake = UnityEngine.Random.insideUnitSphere; // calculate the force to shake
//            physicsObject.AddForce(forceToShake + forceToOrigin); // add the force to the nucleus
//            Debug.Log("shaking");
//        }

//        else
//        {
//            Vector3 forceToOrigin = origin - transform.localPosition;
//            if (forceToOrigin != Vector3.zero)
//            {
//                physicsObject.AddForce(forceToOrigin);
//            }
//        }
//    }
//}