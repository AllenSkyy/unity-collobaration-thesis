using UnityEngine;

public class BoxController : MonoBehaviour
{
    public FoodController.FoodType requiredFoodType;  
    public AudioClip correctFoodSound; // Assign the correct food sound effect in the Inspector
    private bool isFilled = false;
    private static int filledBoxCount = 0;
    private const int totalBoxCount = 4;

    private AudioSource audioSource;

    void Start()
    {
        SetRequiredFoodTypeFromBoxName();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
        Debug.Log($"Box created with required food type: {requiredFoodType}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("food"))
        {
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

    void SetRequiredFoodTypeFromBoxName()
    {
        string boxName = gameObject.name.ToLower();
        int boxIndex = int.Parse(boxName.Replace("box", ""));
        requiredFoodType = (FoodController.FoodType)boxIndex;
    }

    public void DeliverFood(FoodController.FoodType deliveredFoodType, GameObject foodObject)
    {
        if (CheckDeliveredFood(deliveredFoodType))
        {
            Debug.Log("CORRECT FOOD TYPE");
            isFilled = true;
            filledBoxCount++;

            

            Destroy(foodObject);

            if (filledBoxCount == totalBoxCount)
            {
                // All boxes are filled, trigger victory screen
                Debug.Log("All boxes are filled! Trigger victory screen.");
                // Implement logic to show the victory screen
            }

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
        }
    }

    public bool CheckDeliveredFood(FoodController.FoodType deliveredFoodType)
    {
        return deliveredFoodType == requiredFoodType;
    }
}
