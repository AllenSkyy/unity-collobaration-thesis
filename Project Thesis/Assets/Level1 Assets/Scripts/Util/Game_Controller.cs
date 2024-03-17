using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {noState, Weighing, Weighed}
public class Game_Controller : MonoBehaviour
{
    Menu_Controller menuController;
    HeightWeightGenerator heightWeightGenerator;
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
        heightWeightGenerator = GetComponent<HeightWeightGenerator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        menuController.onMenuSelected += onMenuSelected;
        menuController.ButtonOff();
        state = GameState.noState;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogue.activeSelf)
        {
            childcontrol.ActiveChild();
        }
        
        if (Physics2D.OverlapCircle(child.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null && state == GameState.noState)
        {
            menuController.OpenMenu();
            //menuController.ButtonOn();
            state = GameState.Weighing;
        }
        else if (newChild != null && Physics2D.OverlapCircle(newChild.transform.position, 0.2f, GameLayers.i.ScaleLayer) != null && state == GameState.noState)
        {
            //menuController.ButtonOn();
            state = GameState.Weighing;
        }

        if (state == GameState.Weighing)
        {
            timer += Time.deltaTime;
            int seconds = Mathf.FloorToInt(timer % 60); 
            if(seconds == 5)
            {
                menuController.ButtonOn();
                heightWeightGenerator.GenerateRandomNumberForHeight();
                state = GameState.Weighed;
                timer = 0;
            }
        }

        Debug.Log("State is: " + state);

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
        heightWeightGenerator.ResetDisplay();
        menuController.ButtonOff();
        state = GameState.noState;
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
