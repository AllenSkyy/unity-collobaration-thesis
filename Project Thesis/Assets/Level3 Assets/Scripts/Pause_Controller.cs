using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Controller : MonoBehaviour
{
   public GameObject dialPanel;

    void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialPanel.activeSelf)
        {
            Continue();
        }
    }

    public void Pause()
    {
        dialPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        dialPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
