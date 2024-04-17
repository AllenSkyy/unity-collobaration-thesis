using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] GameObject menu, cheatsheet1, NextButtonPanel, PreviousButtonPanel;  
    private float page = 1;
    public Button HealthyButton, ObeseButton, WastedButton; 
    public Button NextPageButton, PreviousPageButton;

    public event Action<int> onMenuSelected;

    public Child_Controller child;


    void Start()
    {
        HealthyButton.onClick.AddListener(() => ButtonClicked(0, HealthyButton));
        ObeseButton.onClick.AddListener(() => ButtonClicked(1, ObeseButton));
        WastedButton.onClick.AddListener(() => ButtonClicked(2, WastedButton));
        NextPageButton.onClick.AddListener(() => ButtonClicked(4, NextPageButton));
        PreviousPageButton.onClick.AddListener(() => ButtonClicked(5, PreviousPageButton));
    }

    public void OpenMenu()
    {
        menu.SetActive(true);

    }

    public void NextPage()
    {
       
        cheatsheet1.SetActive(true);
            //PreviousButtonPanel.SetActive(true);
            //NextButtonPanel.SetActive(false);
           
        

    }

    public void PreviousPage()
    {
        
        cheatsheet1.SetActive(false);
            //PreviousButtonPanel.SetActive(false);
           // NextButtonPanel.SetActive(true);
       

    }

     public void CloseMenu()
    {
        menu.SetActive(false);

    }

    public void ButtonOn()
    {
        HealthyButton.enabled = true;
        ObeseButton.enabled = true;
        WastedButton.enabled = true;
        NextPageButton.enabled = true;
        PreviousPageButton.enabled = true;
    }

    public void ButtonOff()
    {
        HealthyButton.enabled = false;
        ObeseButton.enabled = false;
        WastedButton.enabled = false;
        NextPageButton.enabled = false;
        PreviousPageButton.enabled = false;
    }

   
    void  ButtonClicked(int buttonNo, Button button)
    {
        onMenuSelected?.Invoke(buttonNo);
        EventSystem.current.SetSelectedGameObject(null);
    }

}
