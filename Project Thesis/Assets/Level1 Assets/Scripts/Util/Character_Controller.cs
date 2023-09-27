using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    Character_Animator animator;
    public float speed;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Character_Animator>();
    }
    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver=null)                                                                        
    {    
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;  

        if (!isWalkable(targetPos))
            yield break;

        IsMoving = true;                                                                                       
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)                                   
        {                                                                                                      
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);   
            yield return null;
        }
        transform.position = targetPos;
        IsMoving = false;

        OnMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool isWalkable(Vector3 targetPos)                                        
    {                                                                                 
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidObjects) != null)
        {
            return false;
        }

        return true;
    }

    public Character_Animator Animator
    {
        get => animator;
    }
}
