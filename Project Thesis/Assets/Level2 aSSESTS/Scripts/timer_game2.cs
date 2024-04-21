using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer_game2 : MonoBehaviour
{
    float countdown = 10;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    int score = 0; // Variable to store the score
    bool gameEnded = false;

    public GameObject gameOverUI;
    
    void Start()
    {
        // Set the initial score text
        UpdateScoreText();
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded)
        {
            countdown -= Time.deltaTime;
            double roundedCountdown = System.Math.Round(countdown, 2);
            timerText.text = roundedCountdown.ToString();

            if (AllBoxesFilled())
            {
                Debug.Log("You win!");
                gameEnded = true;
                AddRemainingTimeToScore();
                timerText.text = "0";
                // Stop the game or trigger victory screen
            }
            else if (countdown <= 0)
            {
                Debug.Log("Game over! Restarting...");
                timerText.text = "0";
                gameOverUI.SetActive(true);
          
                
                // Pass the current score to the game over canvas
                
            }
        }
    }

    bool AllBoxesFilled()
    {
        BoxController[] boxes = FindObjectsOfType<BoxController>();
        bool allFilled = true;
        int tempScoreIncrease = 0; // Temporary variable to track score increase

        foreach (BoxController box in boxes)
        {
            if (!box.IsFilled)
            {
                allFilled = false;
            }
            else
            {
                // Increment score for each filled box
                tempScoreIncrease += 100;
            }
        }

        // Update score text to show the current score increase
        scoreText.text = $"Score: {score + tempScoreIncrease}";

        if (allFilled)
        {
            // Update the actual score after checking all boxes
            score += tempScoreIncrease;
            UpdateScoreText();
        }

        return allFilled;
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void AddRemainingTimeToScore()
    {
        // Convert remaining time to a score value
        int remainingScore = (int)(countdown * 10); // Example conversion, adjust as needed
        score += remainingScore;
        Debug.Log($"Remaining time converted to score: {remainingScore}");
        UpdateScoreText();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
