using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveScript : MonoBehaviour
{
    public int requiredPresses = 5; // Number of presses required to remove the object
    public float pressCooldown = 0.5f; // Cooldown period between key presses in seconds

    private int currentPresses = 0; // Counter for the number of presses
    private float lastPressTime; // Timestamp of the last key press
    private bool playerInsideCollider = false; // Flag to track if the player is inside the collider

    void Update()
    {
        // Check for key press in Update
        if (Input.GetKeyDown(KeyCode.E) && playerInsideCollider)
        {
            if (Time.time - lastPressTime > pressCooldown)
            {
                lastPressTime = Time.time;
                currentPresses++; // Increment the counter
                Debug.Log("Press count: " + currentPresses + " / " + requiredPresses); // Log the counter

                if (currentPresses >= requiredPresses)
                {
                    // Deactivate the object
                    gameObject.SetActive(false);

                    // Or, destroy the object
                    // Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
            Debug.Log("Press 'E' to interact.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
        }
    }
}
