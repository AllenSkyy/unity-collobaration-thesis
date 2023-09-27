using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Controller : MonoBehaviour
{
    //[SerializeField] private LayerMask scaleLayer;

    private Character_Controller character;


    private void Awake()
    {
        character = GetComponent<Character_Controller>();
        Walk();
        
    }

    private void Update()
    {
        

        //StartCoroutine(character.Move(new Vector2(-1, 0)));
        character.HandleUpdate();
    }

   public void Walk()
    {
       StartCoroutine(character.Move(new Vector2(-13, 0)));
    } 
  

}   

