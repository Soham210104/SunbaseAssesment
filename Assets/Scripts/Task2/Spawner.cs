using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircles = 5;
    public int maxCircles = 10;
    public float spawnArea = 5f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCircles();
    }

    // Update is called once per frame
    void SpawnCircles()
    {
        int circlesToSpawn = Random.Range(minCircles, maxCircles + 1);
        for (int i = 0; i < circlesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea, spawnArea), Random.Range(-spawnArea, spawnArea), 0);
            Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
