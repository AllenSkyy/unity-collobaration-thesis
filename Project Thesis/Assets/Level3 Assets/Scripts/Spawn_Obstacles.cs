using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obstacles : MonoBehaviour
{
    public GameObject obstacle;
    public float maxX, minx, maxY, minY;
    public float timeBetweenSpwan;
    private float spawnTime;


    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpwan;
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minx, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(obstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
