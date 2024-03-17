using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator animator;
    public GrabController grabController; // Reference to the GrabController script
	private float lastHorizontalInput; // Store the last horizontal input
    public bool isFacingRight = true;
	public float moveSpeed = 5.0f;
	

    void Start()
    {
        lastHorizontalInput = Input.GetAxis("Horizontal");
    }
	
	public bool GetIsFacingRight()
	{
    	return isFacingRight;
	}

    // Update is called once per frame
    void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    

    // Update the facing direction based on horizontal input
    if (horizontalInput > 0)
    {
        isFacingRight = true;
    }
    else if (horizontalInput < 0)
    {
        isFacingRight = false;
    }

    // Set the facing direction in the GrabController script
    grabController.isFacingRight = isFacingRight;

    Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);
    // Multiply the movement vector by the speed factor
    Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

    // Update position
    transform.position = newPosition;

    animator.SetFloat("Horizontal", movement.x);
    animator.SetFloat("Vertical", movement.y);
    animator.SetFloat("Magnitude", movement.magnitude);
    }
}
