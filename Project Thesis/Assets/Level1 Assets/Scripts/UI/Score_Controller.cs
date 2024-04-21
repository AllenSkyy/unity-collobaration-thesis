using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Controller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, finalScoreText;

    [SerializeField] int currentScore;

    int totalScore;
    // Update is called once per frame
    void Update()
    {
        finalScoreText.text = string.Format("Score:{0}/{1}", currentScore, totalScore);
    }

    public void addToScore(int score)
    {
        currentScore += score;
        scoreText.text = string.Format("Score:{0000}", currentScore);
        //scoreText.text = string.Format("Score:{0}/{1}", currentScore, totalScore);
    }

    public void addToTotal(int score)
    {
        totalScore += score;
        //scoreText.text = string.Format("Score:{0}/{1}", currentScore, totalScore);
    }

    
}
