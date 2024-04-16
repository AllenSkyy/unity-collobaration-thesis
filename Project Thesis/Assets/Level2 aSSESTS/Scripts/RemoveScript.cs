using UnityEngine;

public class RemoveScript : MonoBehaviour
{
    public Sprite[] obstacleSprites; // Array of sprites representing different stages of obstacle unfolding
    public int requiredPresses = 5;
    public float pressCooldown = 0.5f;

    private int currentPresses = 0;
    private bool playerInsideCollider = false;
    private bool fullyInteracted = false;
    private float lastPressTime;

    void Update()
    {
        if (!fullyInteracted && Input.GetKeyDown(KeyCode.E) && playerInsideCollider)
        {
            if (Time.time - lastPressTime > pressCooldown)
            {
                lastPressTime = Time.time;
                currentPresses++;
                Debug.Log("Press count: " + currentPresses + " / " + requiredPresses);

                // Advance to the next stage of the obstacle unfolding
                if (currentPresses <= requiredPresses)
                {
                    GetComponent<SpriteRenderer>().sprite = obstacleSprites[currentPresses];
                }

                if (currentPresses >= requiredPresses)
                {
                    // Handle obstacle removal or any other action
                    Debug.Log("Obstacle fully interacted.");
                    fullyInteracted = true;

                    // Remove the Rigidbody component
                    Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
                    if (rigidBody != null)
                    {
                        Destroy(rigidBody);
                        Debug.Log("Rigidbody removed.");
                    }

                    // Disable the Collider component
                    Collider2D collider = GetComponent<Collider2D>();
                    if (collider != null)
                    {
                        collider.enabled = false;
                        Debug.Log("Collider disabled.");
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
            if (!fullyInteracted)
            {
                Debug.Log("Press 'E' to interact.");
            }
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
