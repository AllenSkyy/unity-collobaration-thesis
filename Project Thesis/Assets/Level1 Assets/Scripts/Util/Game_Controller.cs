using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {Menu}
public class Game_Controller : MonoBehaviour
{
    Menu_Controller menuController;
    [SerializeField] GameObject child;
    
    GameState state;
    private void Awake()
    {
        menuController = GetComponent<Menu_Controller>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        menuController.onMenuSelected += onMenuSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(child.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null)
        {
            menuController.OpenMenu();
            state = GameState.Menu;
        }
        if (state == GameState.Menu)
        {
            menuController.HandleUpdate();
        }

    }

    void onMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {
            //Healthy
            
        }
        else if (selectedItem == 1)
        {
            //Obese
           
        }
    }
}
