using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefab;
    public float spawnRate = 1f;
    public float spawnFactor = 0f;
    public bool activateBoss = false;

    private void Start()
    {
        activateBoss = false;
    }

    void Update()
    {        
        if (activateBoss)
        {
            spawnFactor += Time.deltaTime;
            if (spawnFactor >= spawnRate)
            {
                SpawnBoss(prefab[1]);
                Destroy(gameObject);
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
