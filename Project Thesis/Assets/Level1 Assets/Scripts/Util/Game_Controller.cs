using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {noState, Weighing, Weighed}
public enum GamePhase {PhaseOne, PhaseTwo, PhaseThree}
public class Game_Controller : MonoBehaviour
{
    Menu_Controller menuController;
    HeightWeightGenerator heightWeightGenerator;
    [SerializeField] GameObject child;
    [SerializeField] GameObject dialogue;
    float timer;
    int seconds;

    public Child_Controller childcontrol;
    GameObject newChild;
    GameState state;
    GamePhase phase;

    private void Awake()
    {
        menuController = GetComponent<Menu_Controller>();
        childcontrol = child.GetComponent<Child_Controller>();
        heightWeightGenerator = GetComponent<HeightWeightGenerator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        menuController.onMenuSelected += onMenuSelected;
        menuController.ButtonOff();
        childcontrol.ActiveChild();
        state = GameState.noState;
        phase = GamePhase.PhaseOne;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = Mathf.FloorToInt(timer % 60); 
        // if(!dialogue.activeSelf)
        // {
        //     childcontrol.ActiveChild();
        // }

        if(phase == GamePhase.PhaseThree)
        {
            Debug.Log("Entering Difficulty 3");
            GamePhase2();
        }
        else if (phase == GamePhase.PhaseTwo)
        {
            menuController.StartPhase2();
            GamePhase2();
        }
        else
        {
            GamePhase1();
        }
        

        Debug.Log("State is: " + state);

    }

    void GamePhase1()
    {
        if (Physics2D.OverlapCircle(child.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null && state == GameState.noState)
        {
            menuController.OpenMenu();
            state = GameState.Weighing;
        }
        else if (newChild != null && Physics2D.OverlapCircle(newChild.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null && state == GameState.noState)
        {
            state = GameState.Weighing;
        }

        if (state == GameState.Weighing)
        {
            // timer += Time.deltaTime;
            // int seconds = Mathf.FloorToInt(timer % 60); 
            // if(seconds == 5)
            // {
            //     menuController.ButtonOn();
            //     heightWeightGenerator.GenerateRandomNumberForHeight();
            //     state = GameState.Weighed;
            //     timer = 0;
            // }
            menuController.ButtonOn();
            heightWeightGenerator.GenerateRandomNumberForHeight();
            state = GameState.Weighed;
        }

    }

    void GamePhase2()
    {
        if (newChild != null && Physics2D.OverlapCircle(newChild.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null && state == GameState.noState)
        {
            state = GameState.Weighing;
        }

        if (state == GameState.Weighing)
        {
            menuController.ButtonOn();
            heightWeightGenerator.GenerateRandomNumberForHeightandAge();
            state = GameState.Weighed;
        }

    }

    void onMenuSelected(int selectedItem)
    {
        if (selectedItem == 4)
        {
            menuController.NextPage();
        }
        else if (selectedItem == 5)
        {
            menuController.PreviousPage();
        }
        else
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
            else if (selectedItem == 2)
            {
                //Wasted
                Debug.Log("You have marked this child Wasted");
                
            }

            if(seconds > 200)
            {
                phase = GamePhase.PhaseThree;
            }
            else if(seconds > 10)
            {
                phase = GamePhase.PhaseTwo;
            }

            if (menuController.GetToggleValue() == 1)
            {
                Debug.Log("You have marked this child Stunted");
            }
            heightWeightGenerator.ResetDisplay();
            menuController.ButtonOff();
            state = GameState.noState;
            childcontrol.Walk(-13);
            NewChild();
        }

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
