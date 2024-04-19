using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer_Controller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject dialogue1;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if(dialogue1.activeSelf)
        {
            Pause(remainingTime);
        }
        else
        {
            Play();
        }
    }

    private void Pause(float n)
    {
        remainingTime = n;
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60); 
        timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    private void Play()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if ( remainingTime < 0)
        {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60); 
        timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

}
