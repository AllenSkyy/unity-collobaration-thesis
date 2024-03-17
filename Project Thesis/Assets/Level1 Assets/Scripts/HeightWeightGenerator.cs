using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightWeightGenerator : MonoBehaviour
{
    [SerializeField] TextAsset Weight_to_Height;
    [SerializeField] TextMeshProUGUI  HeightDisplay;
    [SerializeField] TextMeshProUGUI  WeightDisplay;
    private List<string> datarows = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        string[] rows = Weight_to_Height.text.Split('\n');
        foreach (string row in rows)
        {
            datarows.Add(row.Trim());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float SelectRandomHeight()
    {
        List<float> heights = new List<float>();

        foreach (string row in datarows)
        {
            string[] values = row.Split(' ');
            float height = float.Parse(values[0]);
            heights.Add(height);
        }

        // Select a random height from the list
        float randomHeight = heights[Random.Range(0, heights.Count)];
        return randomHeight;
    }

    public void GenerateRandomNumberForHeight()
    {
        float randomHeight = SelectRandomHeight();
        List<string> validRows = new List<string>();

        foreach (string row in datarows)
        {
            string[] values = row.Split(' ');
            float rowHeight = float.Parse(values[0]);

            if (rowHeight == randomHeight)
            {
                validRows.Add(row);
            }
        }

        if (validRows.Count > 0)
        {
            // Select a random row
            string selectedRow = validRows[Random.Range(0, validRows.Count)];

            // Get the range from the 1st and 8th number
            string[] values = selectedRow.Split(' ');
            float minValue = float.Parse(values[1]);
            float maxValue = float.Parse(values[7]);

            // Output a random number from the range
            float randomNumber = Random.Range(minValue, maxValue);
            Debug.Log("Heigh: " + randomHeight);
            DisplayHeight(randomHeight);
            DisplayWeight(randomNumber);

            Debug.Log("Random number within range: " + randomNumber);

            if (randomNumber <= float.Parse(values[3]))
            {
                Debug.Log("This Child is Wasted");
            }
            else if (randomNumber >= float.Parse(values[5]))
            {
                Debug.Log("This Child is Obese");
            }
            else
            {
                Debug.Log("This Child is Healthy");
            }
        }
        else
        {
            Debug.LogWarning("No data found for the specified height: " + randomHeight);
        }
    }

    void DisplayHeight(float Height)
    {
        HeightDisplay.text = Height.ToString() +"cm";
    }

    void DisplayWeight(float Weight)
    {
        string formattedNumber = Weight.ToString("F2");
        WeightDisplay.text = formattedNumber + "kg";
    }

    public void ResetDisplay()
    {
        HeightDisplay.text = "";
        WeightDisplay.text = "";
    }
}
