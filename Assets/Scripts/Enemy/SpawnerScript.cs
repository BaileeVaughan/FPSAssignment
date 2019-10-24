using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefab;
    public float spawnRate = 1f;
    public float spawnFactor = 0f;
    public bool isBoss = false;

    private void Start()
    {
        isBoss = false;
    }

    void Update()
    {        
        if (isBoss)
        {
            spawnFactor += Time.deltaTime;
            if (spawnFactor >= spawnRate)
            {
                SpawnBoss(prefab[1]);
                spawnRate = 10000000000000000000;
            }
        }
        else
        {
            spawnFactor += Time.deltaTime;
            if (spawnFactor >= spawnRate)
            {
                Spawn(prefab[0]);
                spawnFactor = 0;
            }
        }
    }

    void Spawn(GameObject gO)
    {
        Instantiate(gO, transform.position, Quaternion.identity);
    }

    void SpawnBoss(GameObject gO)
    {
        Instantiate(gO, transform.position, Quaternion.identity);
    }
}
