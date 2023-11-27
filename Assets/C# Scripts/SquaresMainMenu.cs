using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquaresMainMenu : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] float spawnRate;
    [SerializeField] GameObject[] normalSquares;
    [SerializeField] GameObject[] highlightedSquares;
    [SerializeField] List<GameObject> spawnedSquares = new List<GameObject>();
    GameObject randomSquare;
    GameObject spawnedSquare;
    GameObject lastRandSquare;
    [SerializeField] Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        randomSquare = normalSquares[Random.Range(0, normalSquares.Length)];
        lastRandSquare = randomSquare;
        spawnedSquare = Instantiate(randomSquare, canvas.transform);
        Destroy(spawnedSquare, lifeTime);
        spawnedSquares.Add(spawnedSquare);
        SpawnNewSquare();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedSquares.Count > 0)
        {
            spawnedSquares[0].transform.localScale *= new Vector2(100*Time.deltaTime, 100*Time.deltaTime);
        }
    }

    void SpawnNewSquare()
    {
        StartCoroutine(TimeBetweenSpawning());
        for (int i = 0; i < spawnedSquares.Count; i++)
        {
            if (spawnedSquares[i] == null)
            {
                spawnedSquares.Remove(spawnedSquares[i]);
            }
        }
    }

    IEnumerator TimeBetweenSpawning()
    {
        yield return new WaitForSeconds(spawnRate); //waits time.
        randomSquare = normalSquares[Random.Range(0, normalSquares.Length)];
        while (lastRandSquare == randomSquare)
        {
            randomSquare = normalSquares[Random.Range(0, normalSquares.Length)];
        }
        lastRandSquare = randomSquare;
        spawnedSquare = Instantiate(randomSquare, canvas.transform); //Instantiates that square.
        spawnedSquare.GetComponent<Image>().enabled = true;
        Destroy(spawnedSquare, lifeTime); //Destroyes that square when time passed and its bigger.
        spawnedSquares.Add(spawnedSquare); //Adds that spawned square to list.
        SpawnNewSquare();
    }
}
