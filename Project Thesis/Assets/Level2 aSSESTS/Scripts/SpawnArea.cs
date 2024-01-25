using UnityEngine;


public class SpawnArea : MonoBehaviour
{
    // Reference to your food prefabs
    public GameObject[] foodPrefabs;
	private Vector2 foodPrefabSize;
    public int numberOfFoodObjects = 4;

    // Start is called before the first frame update
    void Start()
    {
        // Shuffle the array of food prefabs
        Shuffle(foodPrefabs);

        // Spawn each food prefab
        for (int i = 0; i < Mathf.Min(numberOfFoodObjects, foodPrefabs.Length); i++)
        {
            // Get a random spawn point within the spawn area
            Vector3 randomSpawnPoint = GetRandomSpawnPoint();

            // Instantiate the food object at the spawn point
            Instantiate(foodPrefabs[i], randomSpawnPoint, Quaternion.identity);
        }
    }

    // Shuffle the array
    void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Get a random point within the spawn area
    Vector3 GetRandomSpawnPoint()
    {
        // Get the bounds of the spawn area's collider
        Collider2D spawnAreaCollider = GetComponent<Collider2D>();

        if (spawnAreaCollider == null)
        {
            Debug.LogError("SpawnArea script requires a Collider2D component on the GameObject.");
            return Vector3.zero;
        }

        // Calculate an offset based on the size of the food object
        float offsetX = Random.Range(-spawnAreaCollider.bounds.extents.x, spawnAreaCollider.bounds.extents.x);
        float offsetY = Random.Range(-spawnAreaCollider.bounds.extents.y, spawnAreaCollider.bounds.extents.y);

        float randomX = spawnAreaCollider.bounds.center.x + offsetX;
        float randomY = spawnAreaCollider.bounds.center.y + offsetY;

        return new Vector3(randomX, randomY, 0f);
    }

    // Optionally, you can visualize the spawn area in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw the bounds of the spawn area's collider
        Collider2D spawnAreaCollider = GetComponent<Collider2D>();
        if (spawnAreaCollider != null)
        {
            Gizmos.DrawWireCube(spawnAreaCollider.bounds.center, spawnAreaCollider.bounds.size);
        }
    }
}
