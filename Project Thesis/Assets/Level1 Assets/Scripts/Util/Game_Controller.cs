using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;


public enum GameState {noState, Weighing, Weighed}
public enum GamePhase {PhaseOne, PhaseTwo, PhaseThree, End}
public class Game_Controller : MonoBehaviour
{
    Menu_Controller menuController;
    HeightWeightGenerator heightWeightGenerator;
    Score_Controller scoreController;
    [SerializeField] GameObject child;

    float timer, timerForPhase3;
    int seconds, secondsPhase3;

    public Child_Controller childcontrol;
    GameObject newChild;
    GameState state;
    GamePhase phase;

    private void Awake()
    {
        menuController = GetComponent<Menu_Controller>();
        childcontrol = child.GetComponent<Child_Controller>();
        heightWeightGenerator = GetComponent<HeightWeightGenerator>();
        scoreController = GetComponent<Score_Controller>();
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
        seconds = Mathf.FloorToInt(timer); 

        if(phase == GamePhase.End)
        {
            SceneManager.LoadSceneAsync(2);
        }
        else if(phase == GamePhase.PhaseThree)
        {
            Debug.Log("Entering Difficulty 3");
            timerForPhase3 += Time.deltaTime;
            secondsPhase3 = Mathf.FloorToInt(timer % 60);
            GamePhase3();

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
        

        Debug.Log("Phase is: " + phase);
        Debug.Log("Seconds are: " + seconds);

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

    void GamePhase3()
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

        // Check if 10 seconds have passed
        if (timerForPhase3 >= 23)
        {
            // Move the child away from the scale
            if (childcontrol != null)
            {
                childcontrol.Walk(-13); // Adjust this value as per your game's requirements
            }

            // Reset timer and other necessary variables
            timerForPhase3 = 0;
            heightWeightGenerator.ResetDisplay();
            menuController.ButtonOff();
            state = GameState.noState;
            NewChild();
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
                Debug.Log("You have marked this child normal!");
                checkAnswer("Normal");
            }   
            else if (selectedItem == 1)
            {
                //Obese
                Debug.Log("You have marked this child Obese!");
                checkAnswer("Obese");
            
            }
            else if (selectedItem == 2)
            {
                //Wasted
                Debug.Log("You have marked this child Wasted");
                checkAnswer("Wasted");
                
            }
            if(seconds >= 300)
            {
                phase = GamePhase.End;
            }
            else if(seconds >= 200)
            {
                phase = GamePhase.PhaseThree;
            }
            else if(seconds >= 100)
            {
                phase = GamePhase.PhaseTwo;
            }

            if (menuController.GetToggleValue() == 1)
            {
                Debug.Log("You have marked this child Stunted");
                checkStunted("marked");
            }else{checkStunted("unmarked");}

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

    void checkAnswer(string answer)
    {
        if(phase == GamePhase.PhaseOne || phase == GamePhase.PhaseTwo)
        {
            if(answer == heightWeightGenerator.getAnswer())
            {
                scoreController.addToScore(100);
            }
            scoreController.addToTotal(100);
        }
        else if (phase == GamePhase.PhaseThree)
        {
            if(answer == heightWeightGenerator.getAnswer())
            {
                scoreController.addToScore(200);
            }
            scoreController.addToTotal(200);
        }
    }

    void checkStunted(string answer)
    {
        if(heightWeightGenerator.isStunted())
        {
            if(answer == "marked")
            {
                scoreController.addToScore(100);
            }
            scoreController.addToTotal(100);
        }
    }
}
