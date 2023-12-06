// FoodController.cs
using UnityEngine;

public class FoodController : MonoBehaviour
{
    // Define an enumeration for different food types
    public enum FoodType
    {
        Food_0,
        Food_1,
        Food_2,
        Food_3
    }

    // Set the food type for each food object in the Inspector
    public FoodType foodType;

    void Start()
    {
        // Assign the food type based on the object's name
        if (gameObject.name.Contains("Food_0"))
        {
            foodType = FoodType.Food_0;
        }
        else if (gameObject.name.Contains("Food_1"))
        {
            foodType = FoodType.Food_1;
        }
        else if (gameObject.name.Contains("Food_2"))
        {
            foodType = FoodType.Food_2;
        }
        else if (gameObject.name.Contains("Food_3"))
        {
            foodType = FoodType.Food_3;
        }
    }
	
	// Add a method to get the food type
    public FoodType GetFoodType()
    {
        return foodType;
    }
}
