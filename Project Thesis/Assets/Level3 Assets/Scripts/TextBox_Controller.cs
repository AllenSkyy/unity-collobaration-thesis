using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox_Controller : MonoBehaviour
{
    [SerializeField] GameObject DialoguePanel0, DialoguePanel1, DialoguePanel2, DialoguePanel3;

    float timer;
    int seconds, dialogueNum = 0;
    // Start is called before the first frame update
     private void Awake()
     {
        pauseGame();
     }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = Mathf.FloorToInt(timer); 
        if(dialogueNum < 3)
        {
            if(seconds > 120){pauseGame();}
            else if(seconds > 60){pauseGame();}
            else if(seconds > 30){pauseGame();}
        }
        
    }

    public void pauseGame()
    {
        if(dialogueNum == 0) {DialoguePanel0.SetActive(true);}
        else if (dialogueNum == 1) {DialoguePanel1.SetActive(true);}
        else if (dialogueNum == 2) {DialoguePanel2.SetActive(true);}
        else if (dialogueNum == 3) {DialoguePanel3.SetActive(true);}
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        if(dialogueNum == 0){DialoguePanel0.SetActive(false); dialogueNum++;}
        else if(dialogueNum == 1){DialoguePanel1.SetActive(false); dialogueNum++;}
        else if(dialogueNum == 2){DialoguePanel2.SetActive(false); dialogueNum++;}
        else if(dialogueNum == 3){DialoguePanel2.SetActive(false); dialogueNum++;}
        Time.timeScale = 1f;
    }
}
