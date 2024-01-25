using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetectRight;
    public Transform grabDetectLeft;
    public Transform boxHolder;
    public float rayDist;

    private bool isHolding = false;
    private Transform heldItem;
    private float boxHolderOffsetX = -1.1f;
    public bool isFacingRight = true;
    private bool isIdle = true;
	
	private int GetFoodType(GameObject foodObject)
    {
        // Assuming you have attached the FoodController script to your food objects
        FoodController foodController = foodObject.GetComponent<FoodController>();

        if (foodController != null)
        {
            // Access the food type from the FoodController
            int foodType = (int)foodController.foodType;

            // Debug log for food type
            Debug.Log($"Food type: {foodType}");

            return foodType;
        }

        // Return -1 or some invalid value if the type cannot be determined
        return -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isHolding)
            {
                if (isFacingRight && TryGrab(grabDetectRight, Vector2.right))
                {
                    isHolding = true;
                    SetBoxHolderOffsetX(-1.1f); // Set the X offset for boxHolder to the left
                    SetBoxPosition();
                }
                else if (!isFacingRight && TryGrab(grabDetectLeft, Vector2.left))
                {
                    isHolding = true;
                    SetBoxHolderOffsetX(-1.1f); // Set the X offset for boxHolder to the right
                    SetBoxPosition();
                }
            }
            else
            {
                DropItem();
            }
        }

        if (isHolding && heldItem != null)
        {
			// Call the GetFoodType function and pass the held food object
     	   int foodType = GetFoodType(heldItem.gameObject);

		
            float xOffset = isFacingRight ? -boxHolderOffsetX : boxHolderOffsetX;
            Vector3 newPosition = transform.position + new Vector3(xOffset, -0.2f, 0.0f);
            heldItem.position = newPosition;
        }

        if (boxHolder != null)
        {
            // Adjust boxHolder position based on facing direction, but only if not idle
            if (!isIdle)
            {
                SetBoxPosition();
            }
        }
    }

    void SetBoxHolderOffsetX(float offsetX)
    {
        boxHolderOffsetX = offsetX;
    }

    void SetBoxPosition()
    {
        float boxHolderX = isFacingRight ? -1.1f : 1.1f;
        Vector3 boxHolderPosition = new Vector3(transform.position.x + boxHolderX, transform.position.y - 0.2f, 0.0f);
        boxHolder.position = boxHolderPosition;
    }

    bool TryGrab(Transform grabDetector, Vector2 direction)
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
        }
    }
}
