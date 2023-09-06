using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Movement : MonoBehaviour
{
    public GameObject pointA;
    private Rigidbody2D rb;
    private Transform currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position
    }

    
}
