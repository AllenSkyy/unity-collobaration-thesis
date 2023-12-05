using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] GameObject menu;  
    public Button HealthyButton, ObeseButton; 

    public event Action<int> onMenuSelected;

    public Child_Controller child;

    /* private void Awake()
    {
        HealthColors = HealthyButton.colors;
        ObeseColors = ObeseButton.colors;
    } */

    void Start()
    {
        HealthyButton.onClick.AddListener(() => ButtonClicked(0, HealthyButton));
        ObeseButton.onClick.AddListener(() => ButtonClicked(1, ObeseButton));
    }

    public void OpenMenu()
    {
        menu.SetActive(true);

    }

     public void CloseMenu()
    {
        menu.SetActive(false);

    }

    public void ButtonOn()
    {
        HealthyButton.enabled = true;
        ObeseButton.enabled = true;
    }

    public void ButtonOff()
    {
        HealthyButton.enabled = false;
        ObeseButton.enabled = false;
    }

   
    void  ButtonClicked(int buttonNo, Button button)
    {
        onMenuSelected?.Invoke(buttonNo);
    }

}
