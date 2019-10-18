using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float spawnFactor = 0f;

    void Update()
    {
        spawnFactor += Time.deltaTime;
        if (spawnFactor >= spawnRate)
        {
            Spawn(prefab);
            spawnFactor = 0;
        }
    }

    void Spawn(GameObject gO)
    {
        GameObject spawnedObject = Instantiate(gO);
        spawnedObject.transform.position = transform.position;
    }
}
