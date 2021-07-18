using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMaker : MonoBehaviour
{
    private float cloudSpeed;
    public GameObject startPoint;
    public GameObject endPoint;
    public float spawnIntervalMin;
    public float spawnIntervalMax;    
    public GameObject[] clouds;

    void Start()
    {
        StartCoroutine("GenerateClouds");
    }

    IEnumerator GenerateClouds()
    {
        for(;;)
        {
            float nextSpawnTime = generateSpawnTime();
            SpawnCloud();
            
            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    private void SpawnCloud()
    {
        
    }

    private float generateSpawnTime()
    {
        return UnityEngine.Random.Range(spawnIntervalMin, spawnIntervalMax);
    }
}
