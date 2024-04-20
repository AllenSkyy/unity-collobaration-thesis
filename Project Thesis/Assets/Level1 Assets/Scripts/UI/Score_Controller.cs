using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Controller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int currentScore;

    int totalScore;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToScore(int score)
    {
        currentScore += score;
        scoreText.text = string.Format("Score:{0000}", currentScore);
    }

    public void addToTotal(int score)
    {
        totalScore += score;
    }
}
