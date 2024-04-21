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
    [SerializeField] GameObject child, endingPanel;
    [SerializeField] GameObject[] PhaseOneDialogues, PhaseTwoDialogues, PhaseThreeDialogues, PhaseEndDialogues;

    float timer, timerForPhase3;
    int seconds, secondsPhase3;

    public Child_Controller childcontrol;
    GameObject newChild;
    GameState state;
    GamePhase phase;

    bool phase1TextIsDone = false;
    bool phase2TextIsDone = false;
    bool phase3TextIsDone = false;
    bool phaseEndTextIsDone = false;
    int phaDiaNum = 0;

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
            if (!phaseEndTextIsDone)
            {
                Time.timeScale = 0f;
                if(phaDiaNum == 0)
                {
                    NextPage();
                }
            }
            else
            {
                endingPanel.SetActive(true);
            }
        }
        else if(phase == GamePhase.PhaseThree)
        {
            if (!phase3TextIsDone && state == GameState.Weighed)
            {
                Time.timeScale = 0f;
                if(phaDiaNum == 0)
                {
                    NextPage();
                }
            }
            else
            {
                timerForPhase3 += Time.deltaTime;
                secondsPhase3 = Mathf.FloorToInt(timer % 60);
                GamePhase3();
            }
        }
        else if (phase == GamePhase.PhaseTwo)
        {
            if (!phase2TextIsDone && state == GameState.Weighed)
            {
                Time.timeScale = 0f;
                if(phaDiaNum == 0)
                {
                    NextPage();
                }
            }
            else
            {
                menuController.StartPhase2();
                GamePhase2();
            }
        }
        else 
        {
            if (!phase1TextIsDone && state == GameState.Weighed)
            {
                Time.timeScale = 0f;
                if(phaDiaNum == 0)
                {
                    NextPage();
                }
            }
            else
            {
                GamePhase1();
            }
        }
        

        Debug.Log("Phase is: " + phase);
        Debug.Log("Phase3Seconds are: " + timerForPhase3);

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

        // Check if 23 seconds have passed
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
            else if(seconds >= 20)
            {
                phase = GamePhase.PhaseThree;
            }
            else if(seconds >= 10)
            {
                phase = GamePhase.PhaseTwo;
            }

            if (menuController.GetToggleValue() == 1)
            {
                Debug.Log("You have marked this child Stunted");
                checkStunted("marked");
            }else{checkStunted("unmarked");}
            timerForPhase3 = 0;
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

    public void GoToNextLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void NextPage()
    {
        if(phase == GamePhase.PhaseOne)
        {
            if(phaDiaNum < PhaseOneDialogues.Length)
            {
                PhaseOneDialogues[phaDiaNum].SetActive(true);
                phaDiaNum++;
            }
            else
            {
                PhaseOneDialogues[0].SetActive(false);
                PhaseOneDialogues[1].SetActive(false);
                PhaseOneDialogues[2].SetActive(false);
                PhaseOneDialogues[3].SetActive(false);
                phase1TextIsDone = true;
                phaDiaNum = 0;
                Time.timeScale = 1f;
            }
        }
        else if(phase == GamePhase.PhaseTwo)
        {
            if(phaDiaNum < PhaseTwoDialogues.Length)
            {
                PhaseTwoDialogues[phaDiaNum].SetActive(true);
                phaDiaNum++;
            }
            else
            {
                PhaseTwoDialogues[0].SetActive(false);
                phase2TextIsDone = true;
                phaDiaNum = 0;
                Time.timeScale = 1f;
            }
        }
        else if(phase == GamePhase.PhaseThree)
        {
            if(phaDiaNum < PhaseThreeDialogues.Length)
            {
                PhaseThreeDialogues[phaDiaNum].SetActive(true);
                phaDiaNum++;
            }
            else
            {
                PhaseThreeDialogues[0].SetActive(false);
                PhaseThreeDialogues[1].SetActive(false);
                phase3TextIsDone = true;
                phaDiaNum = 0;
                Time.timeScale = 1f;
            }
        }
        else if(phase == GamePhase.End)
        {
            if(phaDiaNum < PhaseEndDialogues.Length)
            {
                PhaseEndDialogues[phaDiaNum].SetActive(true);
                phaDiaNum++;
            }
            else
            {
                PhaseEndDialogues[0].SetActive(false);
                PhaseEndDialogues[1].SetActive(false);
                PhaseEndDialogues[2].SetActive(false);
                phaseEndTextIsDone = true;
                phaDiaNum = 0;
                Time.timeScale = 1f;
            }
        }
    }
}
