using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartQuit : MonoBehaviour
{
    public void Restart()
    {

        SceneManager.LoadSceneAsync(1); 
    }

    public void QuitGame()
    {
         #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
         #else
             Application.Quit();
         #endif
            Debug.Log("Quit Game");
    }
}
