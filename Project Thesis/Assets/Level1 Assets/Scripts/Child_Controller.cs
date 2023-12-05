using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Controller : MonoBehaviour
{

    private Character_Controller character;


    private void Awake()
    {
        character = GetComponent<Character_Controller>();
        
    }

    private void Start()
    {
        Walk(-9);
    }

    private void Update()
    {
        character.HandleUpdate();
    }

    public void Walk(int steps)
    {
       
        StartCoroutine(character.Move(new Vector2(steps, 0)));
        
    }

    public void randomWalk(int steps)
    {
        StartCoroutine(character.Move(new Vector2(steps, 0)));
        StartCoroutine(character.Move(new Vector2(-steps, 0)));
    } 
  

}   

