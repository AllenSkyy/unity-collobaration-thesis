using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class HeightWeightGenerator : MonoBehaviour
{
    [SerializeField] TextAsset Weight_to_Height;
    [SerializeField] TextAsset Height_to_Age;

    [SerializeField] TextMeshProUGUI  HeightDisplay;
    [SerializeField] TextMeshProUGUI  WeightDisplay;
    [SerializeField] TextMeshProUGUI  YearDisplay;
    [SerializeField] TextMeshProUGUI  MonthDisplay;
    private List<string> datarows = new List<string>();
    private List<string> datarows2 = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        string[] rows = Weight_to_Height.text.Split('\n');
        foreach (string row in rows)
        {
            datarows.Add(row.Trim());
        }

        string[] rows2 = Height_to_Age.text.Split('\n');
        foreach (string row in rows2)
        {
            datarows2.Add(row.Trim());
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

    int SelectRandomAge()
    {
        List<int> agesYear = new List<int>();

        foreach (string row in datarows2)
        {
            string[] values = row.Split(' ');
            int ageYear = int.Parse(values[0]);
            agesYear.Add(ageYear);
        }

        // Select a random height from the list
        int randomAge = agesYear[Random.Range(0, agesYear.Count)];
        return randomAge;
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

            // Get the range from the 1st and 7th number
            string[] values = selectedRow.Split(' ');
            float minValue = float.Parse(values[1]);
            float maxValue = float.Parse(values[7]);

            // Output a random number from the range
            float randomNumber = Random.Range(minValue, maxValue);
            Debug.Log("Height: " + randomHeight);
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

    public void GenerateRandomNumberForHeightandAge()
    {
        float randomHeight = SelectRandomHeight();
        int randomAge = SelectRandomAge();

        List<string> validRows = new List<string>();
        List<string> validRows2 = new List<string>();

        foreach (string row in datarows)
        {
            string[] values = row.Split(' ');
            float rowHeight = float.Parse(values[0]);

            if (rowHeight == randomHeight)
            {
                validRows.Add(row);
            }
        }

        foreach (string row in datarows2)
        {
            string[] values = row.Split(' ');
            int rowAge = int.Parse(values[0]);

            if (rowAge == randomAge)
            {
                validRows2.Add(row);
            }
        }

        if (validRows.Count > 0)
        {
            // Select a random row
            string selectedRow = validRows[Random.Range(0, validRows.Count)];

            // Get the range from the 1st and 7th number
            string[] values = selectedRow.Split(' ');
            float minValue = float.Parse(values[1]);
            float maxValue = float.Parse(values[7]);

            // Output a random number from the range
            float randomNumber = Random.Range(minValue, maxValue);
            Debug.Log("Height: " + randomHeight);
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

        if(validRows2.Count > 0)
        {
            // Select a random row
            string selectedRow2 = validRows2[Random.Range(0, validRows2.Count)];

            // Get the range from the 1st and 7th number
            string[] values2 = selectedRow2.Split(' ');
            int month = int.Parse(values2[1]);
            float minheight = float.Parse(values2[5]);
            DisplayAgeMonth(month);
            DisplayAgeYear(randomAge);

            if(randomHeight < minheight)
            {
                Debug.Log("This child is stunted");
            }

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

    void DisplayAgeYear(int Year)
    {
        YearDisplay.text = Year.ToString() +" Years";
    }

    void DisplayAgeMonth(int Month)
    {
        MonthDisplay.text = Month.ToString() +" Months";
    }

    public void ResetDisplay()
    {
        HeightDisplay.text = "";
        WeightDisplay.text = "";
        YearDisplay.text = "";
        MonthDisplay.text = "";
    }
}
