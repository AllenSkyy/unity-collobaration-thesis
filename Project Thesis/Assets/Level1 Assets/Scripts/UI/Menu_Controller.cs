using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] GameObject menu, cheatsheet1, cheatsheet2, cheatsheet3;
    [SerializeField] GameObject NextButtonPanel, PreviousButtonPanel;  
    [SerializeField] GameObject Info2, StuntedToggle;
    private float page = 1;
    private float toggleValue = 0;
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
        if(page == 1)
        {
            cheatsheet2.SetActive(true);
            PreviousButtonPanel.SetActive(true);
            page++;
        }
        else if (page == 2)
        {
            cheatsheet3.SetActive(true);
            NextButtonPanel.SetActive(false);
            page++;
        }
           
        

    }

    public void PreviousPage()
    {
        if(page == 2)
        {
            cheatsheet2.SetActive(false);
            PreviousButtonPanel.SetActive(false);
            page--;
        }
        else if (page == 3)
        {
            cheatsheet3.SetActive(false);
            NextButtonPanel.SetActive(true);
            page--;
        }
       

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

    //function for the start of Phase2
    public void StartPhase2()
    {
        Info2.SetActive(true);
        StuntedToggle.SetActive(true);
    }

    public void SetToggleValueNo()
    {
        toggleValue = 0;
    }

    public void SetToggleValueYes()
    {
        toggleValue = 1;
    }


    public float GetToggleValue()
    {
        return toggleValue;
    }


}
