using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private Animator animator; // Animator reference

    public Transform grabDetectRight;
    public Transform grabDetectLeft;
    public Transform boxHolder;
    public float rayDist;

    private bool isHolding = false;
    private Transform heldItem;
    private float boxHolderOffsetX = -1.5f; // Adjusted box holder offset
    public bool isFacingRight = true;
    private bool isIdle = true;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get Animator component at start
    }

    private int GetFoodType(GameObject foodObject)
    {
        // Assuming you have attached the FoodController script to your food objects
        FoodController foodController = foodObject.GetComponent<FoodController>();

        if (foodController != null)
        {
            // Access the food type from the FoodController
            int foodType = (int)foodController.foodType;

            return foodType;
        }

        // Return -1 or some invalid value if the type cannot be determined
        return -1;
    }

    private void Update()
    {
        // Set GrabR and GrabL parameters based on facing direction and holding state
        animator.SetBool("GrabR", isFacingRight && isHolding);
        animator.SetBool("GrabL", !isFacingRight && isHolding);   
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isHolding)
            {
                if (isFacingRight && TryGrab(grabDetectRight, Vector2.right))
                {
                    isHolding = true;
                    SetBoxPosition();
                    animator.SetBool("grabber", true); // Set grabber to true when holding something
                }
                else if (!isFacingRight && TryGrab(grabDetectLeft, Vector2.left))
                {
                    isHolding = true;
                    SetBoxPosition();
                    animator.SetBool("grabber", true); // Set grabber to true when holding something
                }
            }
            else
            {
                DropItem();
            }
        }

        if (isHolding && heldItem != null)
        {
            int foodType = GetFoodType(heldItem.gameObject);

            float xOffset = isFacingRight ? -boxHolderOffsetX : boxHolderOffsetX;
            Vector3 newPosition = transform.position + new Vector3(xOffset, -0.2f, 0.0f);
            heldItem.position = newPosition;
        }
    }

    private void SetBoxPosition()
    {
        float boxHolderX = isFacingRight ? -1.5f : 1.5f; // Adjusted box holder position
        Vector3 boxHolderPosition = new Vector3(transform.position.x + boxHolderX, transform.position.y - 0.2f, 0.0f);
        boxHolder.position = boxHolderPosition;
    }

    private bool TryGrab(Transform grabDetector, Vector2 direction)
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetector.position, direction * rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "food")
        {
            if (!isHolding)
            {
                Collider2D itemCollider = grabCheck.collider.gameObject.GetComponent<Collider2D>();
                if (itemCollider != null && itemCollider.OverlapPoint(grabDetector.position))
                {
                    heldItem = grabCheck.collider.gameObject.transform;
                    heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
                    return true;
                }
            }
        }
        return false;
    }

    public void DropItem()
    {
        if (isHolding && heldItem != null)
        {
            heldItem.GetComponent<Rigidbody2D>().isKinematic = false;
            heldItem = null;
            isHolding = false;
            animator.SetBool("grabber", false); // Set grabber to false when dropping something
            animator.SetBool("GrabR", false);
            animator.SetBool("GrabL", false);
        }
    }
}
