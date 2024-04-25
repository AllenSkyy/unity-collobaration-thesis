using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_Obstacles : MonoBehaviour
{
    public GameObject obstacle;
    public float maxX, minx, maxY, minY;
    public float timeBetweenSpwan;
    public float spawnTime;

    public bool multipleSpawns;
    private bool SpawnAlready = false;
    private float StartTime;

    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - StartTime;

        if(elapsedTime > spawnTime && multipleSpawns)
        {
            Spawn();
            spawnTime = elapsedTime + timeBetweenSpwan;
        }
        else if(elapsedTime> spawnTime)
        {
            SpawnOnce();
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minx, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(obstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }

    void SpawnOnce()
    {
        if(!SpawnAlready)
        {
            Spawn();
            SpawnAlready = true;
        }
    }

    public void Adjustimebetweenspawn(float time)
    {
        timeBetweenSpwan = time;
    }
}
