using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] GameObject menu;   

    public event Action<int> onMenuSelected;

    List<Text> menuItems; 

    int selectedItem = 0;

    public Child_Controller child;

    private void Awake()
    {
        menuItems = menu.GetComponentsInChildren<Text>().ToList();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        UpdateItemSelection();

    }

    public void HandleUpdate()
    {
        int prevSelection = selectedItem;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            onMenuSelected?.Invoke(selectedItem);
            child.Walk();
        }

    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if(i == selectedItem)
                menuItems[i].color = Global_Settings.i.HighlightedColor;
            else
                menuItems[i].color = Color.black;
        }
    }
}
