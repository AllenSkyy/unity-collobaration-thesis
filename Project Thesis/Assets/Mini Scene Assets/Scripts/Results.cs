using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Results : MonoBehaviour
{
    float level1Score, level3Score;

    float level1HPS, level3HPS;
    float totalScore, totalHPS, ratingScore;
    [SerializeField] TextMeshProUGUI level1ScoreText, level2ScoreText, level3ScoreText;
    [SerializeField] TextMeshProUGUI TotalText, RatingText, ResultText;
    //[SerializeField] TextMeshProUGUI level1HPSText, level2HPSText, level3HPSText;

    // Start is called before the first frame update
    void Start()
    {
        level1Score = PlayerPrefs.GetFloat("level1Score");
        level3Score = PlayerPrefs.GetFloat("level3Score");

        level1HPS = PlayerPrefs.GetFloat("level1HPS");
        level3HPS = PlayerPrefs.GetFloat("level3HPS");
        totalScore = level1Score + level3Score;
        totalHPS = level1HPS + level3HPS;
        ratingScore = (totalScore/totalHPS) * 100f;

        level1ScoreText.text = string.Format("Level 1 Score: {0}/{1}", level1Score, level1HPS);
        level3ScoreText.text = string.Format("Level 1 Score: {0}/{1}", level3Score, level3HPS);
        TotalText.text = string.Format("Total: {0}/{1}", totalScore, totalHPS);
        RatingText.text = string.Format("Rating: {0}%", ratingScore);

        if(IsPassing())
        {
            ResultText.text = string.Format("You did it! You've made the barangay healthier for the youth!");
        }
        else
        {
            ResultText.text = string.Format("What a waste of tax pesos, do better next time! >:(");
        }
    }


    private bool IsPassing()
    {
        if (ratingScore >= 75) {return true;}
        else {return false;}
    }
}
