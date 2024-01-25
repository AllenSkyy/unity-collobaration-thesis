using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {noState, Weighing}
public class Game_Controller : MonoBehaviour
{
    Menu_Controller menuController;
    [SerializeField] GameObject child;
    [SerializeField] GameObject dialogue;
    float timer;

    public Child_Controller childcontrol;
    GameObject newChild;
    GameState state;
    private void Awake()
    {
        menuController = GetComponent<Menu_Controller>();
        childcontrol = child.GetComponent<Child_Controller>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        menuController.onMenuSelected += onMenuSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogue.activeSelf)
        {
            childcontrol.ActiveChild();
        }
        
        if (Physics2D.OverlapCircle(child.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null)
        {
            menuController.OpenMenu();
            //menuController.ButtonOn();
            state = GameState.Weighing;
        }
        else if (Physics2D.OverlapCircle(newChild.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null)
        {
            //menuController.ButtonOn();
            state = GameState.Weighing;
        }
        else
        {
            state = GameState.noState;
            menuController.ButtonOff();
        }

        if (state == GameState.Weighing)
        {
            timer += Time.deltaTime;
            int seconds = Mathf.FloorToInt(timer % 60); 
            if(seconds == 5)
            {
                menuController.ButtonOn();
                state = GameState.noState;
                timer = 0;
            }
            /* else
            {
                if(Random.Range(1, 4) == 3)
                {
                    childcontrol.randomWalk(5);
                }
                
            } */
            //menuController.HandleUpdate();
        }

    }

    void onMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {
            //Healthy
            Debug.Log("You have marked this child healthy!");
        }   
        else if (selectedItem == 1)
        {
            //Obese
            Debug.Log("You have marked this child Obese!");
           
        }
        childcontrol.Walk(-13);
        NewChild();
    }

    void NewChild()
    {
        if (newChild != null)
        {
            // Destroy the previous child before instantiating a new one
            Destroy(newChild, 5);
        }

        newChild = Instantiate(child, new Vector3(11.5f, -2.5f, 0f), Quaternion.identity);
        childcontrol = newChild.GetComponent<Child_Controller>();
    }
}
