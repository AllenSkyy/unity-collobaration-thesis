using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Controller : MonoBehaviour
{
    public float truckSpeed;
    private Rigidbody2D rb;
    private Vector2 truckDirection;

    Score_Controller scoreController;

    private void Awake()
    {
        scoreController = GetComponent<Score_Controller>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical");
        truckDirection = new Vector2(0, directionY).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, truckDirection.y * truckSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.tag == "Checkpoint")
        {
            scoreController.addToScore(125);
        }
        else if(collision.tag == "Obstacle")
        {
            scoreController.addToScore(-25);
        }
   }
}
