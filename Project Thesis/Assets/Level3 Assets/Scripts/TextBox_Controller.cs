using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox_Controller : MonoBehaviour
{
    [SerializeField] GameObject DialoguePanel0, DialoguePanel1, DialoguePanel2, DialoguePanel3;
    [SerializeField] GameObject spawnpoint;

    Spawn_Obstacles spawnobstacles;
    camMovement cam;

    float timer;
    int seconds, dialogueNum = 0;
    // Start is called before the first frame update
     private void Awake()
     {
        pauseGame();
     }
    void Start()
    {
        spawnobstacles = spawnpoint.GetComponent<Spawn_Obstacles>();
        cam = GetComponent<camMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = Mathf.FloorToInt(timer); 
        if(dialogueNum <= 3)
        {
            if(seconds > 300 && dialogueNum == 3){pauseGame();} // game ends
            else if(seconds > 155 && dialogueNum == 2)
            {
                pauseGame();
                spawnobstacles.Adjustimebetweenspawn(0.5f);
                cam.AdjustCamSpeed();
            } //obstacles appear more frequent and game gets faster
            else if(seconds > 30 && dialogueNum == 1){pauseGame();} // obstacles appear
        }
        
        Debug.Log("Seconds is " + seconds+ " dialoguenum is "+ dialogueNum);
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
        else if(dialogueNum == 3){DialoguePanel3.SetActive(false); dialogueNum++;}
        Time.timeScale = 1f;
    }
}
