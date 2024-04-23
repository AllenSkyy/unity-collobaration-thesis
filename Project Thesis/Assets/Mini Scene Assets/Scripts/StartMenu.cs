using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject CreditMenu;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); 
    }

    public void OpenCredits()
    {
        CreditMenu.SetActive(true);
    }

    public void CloseCredits()
    {
        CreditMenu.SetActive(false);
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
