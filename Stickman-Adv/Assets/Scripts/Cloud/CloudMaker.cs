using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMaker : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject[] clouds;
    public float spawnIntervalMin;
    public float spawnIntervalMax;    

    //cloud speed interval
    public float minCloudsSpeed;
    public float maxCloudSpeed;

    //cloud scale interval
    public float minCloudScale;
    public float maxCloudScale;
    public float heightVariationFactor;

    //pre-start clouds number interval
    public int minStartClouds;
    public int maxStartClouds;

    void Start()
    {
        preSpawnCloud();
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

    private void SpawnCloud(float? xPosition = null)
    {
        int cloudIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[cloudIndex]);
        
        float cloudSpeed = UnityEngine.Random.Range(minCloudsSpeed, maxCloudSpeed);
        float cloudScale = UnityEngine.Random.Range(minCloudScale, maxCloudScale);

        float cloudXPosition = xPosition ?? startPoint.transform.position.x;
        float cloudYposition = UnityEngine.Random.Range(startPoint.transform.position.y - heightVariationFactor, startPoint.transform.position.y + heightVariationFactor);

        cloud.transform.position = new Vector2(cloudXPosition,cloudYposition);
        cloud.transform.localScale = new Vector2(cloudScale, cloudScale);

        cloud.GetComponent<CloudBehavior>().Move(cloudSpeed, endPoint.transform.position.x);
        
    }

    private void preSpawnCloud() {
        int numberOfClouds = UnityEngine.Random.Range(minStartClouds, maxStartClouds);
        float xStart = startPoint.transform.position.x;
        float xFinish = endPoint.transform.position.x;

        Debug.Log($"{xStart} - {xFinish}");

        for (int i = 0; i < numberOfClouds; i++)
        {
            float xPosition = UnityEngine.Random.Range(xStart, xFinish);
            Debug.Log($"{xPosition}");
            SpawnCloud(xPosition);
        }
    }

    private float generateSpawnTime()
    {
        return UnityEngine.Random.Range(spawnIntervalMin, spawnIntervalMax);
    }
}
