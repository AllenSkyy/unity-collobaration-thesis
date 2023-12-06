// BoxController.cs
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public FoodController.FoodType requiredFoodType;  // Use values FoodType.Food_0 to FoodType.Food_3
    private bool isOpen = false;
	 public bool isFilled = false;

    void Start()
    {
        // Set the required food type based on the box's name
        SetRequiredFoodTypeFromBoxName();

        Debug.Log($"Box created with required food type: {requiredFoodType}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("food"))
        {
            // Assuming the food has a script that determines its type, adjust accordingly
            FoodController foodController = other.GetComponent<FoodController>();

            if (foodController != null)
            {
                DeliverFood(foodController.GetFoodType(), other.gameObject);
            }
            else
            {
                Debug.LogError("Food object is missing the FoodController script.");
            }
        }
    }

  

    void DisplayFoodPrompt()
    {
        // Implement your logic to display a prompt indicating the required food type
        Debug.Log($"This box needs food_{requiredFoodType}");
    }
	
	 void SetRequiredFoodTypeFromBoxName()
    {
        // Assuming your boxes are named like "box0", "box1", ...
        string boxName = gameObject.name.ToLower();

        // Extract the index from the box name
        int boxIndex = int.Parse(boxName.Replace("box", ""));

        // Convert the index to FoodType
        requiredFoodType = (FoodController.FoodType)boxIndex;
    }

    void OpenBox()
    {
        // Implement the opening logic here
    }

    // Function to check if the delivered food matches the required food type
    public bool CheckDeliveredFood(FoodController.FoodType deliveredFoodType)
    {
        return deliveredFoodType == requiredFoodType;
    }

    // Function to handle the delivery of food to the box
    public void DeliverFood(FoodController.FoodType deliveredFoodType, GameObject foodObject)
    {
        if (CheckDeliveredFood(deliveredFoodType))
        {
            Debug.Log("CORRECT FOOD TYPE");
            isFilled = true;
			
            Destroy(foodObject);
			GameObject player = GameObject.FindGameObjectWithTag("Player");
      		if (player != null)
       		 {
            	GrabController grabController = player.GetComponent<GrabController>();
            	if (grabController != null)
            	{
                	grabController.DropItem();
            	}
        }
			
			
			
        }
        else
        {
            Debug.Log("WRONG FOOD TYPE");
            // Implement any logic for incorrect delivery
            // For example, play a different sound, show a message, etc.
        }
    }
}
