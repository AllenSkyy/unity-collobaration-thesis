using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class timer_game2 : MonoBehaviour
{
    float countdown = 90;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    public TMP_Text WinText;

    int score = 0; // Variable to store the score
    bool gameEnded = false;

    public GameObject gameOverUI;
     public GameObject WinUI;

    public GameObject introUI;
    public Text npctextbox;
    public string[] dialogue;
    private int index;
    public float wordSpeed;


    public GameObject Continue;
    private bool introDialogueActive = true; // Track if intro dialogue is active
    
    void Start()
    {
        // Set the initial score text
        UpdateScoreText();
        gameOverUI.SetActive(false);
        WinUI.SetActive(false);
        introUI.SetActive(true);
		StartCoroutine(StartDialogue());
			
                
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
         // Call the NextLine function when spacebar is pressed
                NextLine();
        }
        if (!introDialogueActive && !gameEnded)
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
                WinText.text = scoreText.text;
                WinUI.SetActive(true);

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

    public void zeroText(){
		npctextbox.text ="";
		index = 0;
		
	}

    public void NextLine()
{
    Continue.SetActive(false);

    if (index < dialogue.Length - 1)
    {
        index++;
        npctextbox.text = "";
        StartCoroutine(StartDialogue());
        Continue.SetActive(true);
    }
    else
    {
        zeroText();
        introUI.SetActive(false);
        introDialogueActive = false;
    }
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

    IEnumerator StartDialogue(){
		foreach(char letter in dialogue[index]. ToCharArray())
		{
			npctextbox.text += letter;
            yield return new WaitForSeconds(wordSpeed);
		}
        
	}

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
