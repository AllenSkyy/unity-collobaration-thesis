using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Controller : MonoBehaviour
{
    public float truckSpeed;
    private Rigidbody2D rb;
    private Vector2 truckDirection;

    Score_Controller scoreController;
    [SerializeField] GameObject[] CheckpointDios;
    private int dialogueNum = 0;

    private void Awake()
    {
        scoreController = GetComponent<Score_Controller>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreController.addToTotal(1000);
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical");
        truckDirection = new Vector2(0, directionY).normalized;

        Debug.Log("dialogue num for truck is " + dialogueNum);
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
            CheckpointDios[dialogueNum].SetActive(true);
            Time.timeScale = 0f;
        }
        else if(collision.tag == "Obstacle")
        {
            scoreController.addToScore(-25);
        }
   }

   public void resumeGame()
   {
        CheckpointDios[dialogueNum].SetActive(false);
        dialogueNum++;
        Time.timeScale = 1f;
   }
}
