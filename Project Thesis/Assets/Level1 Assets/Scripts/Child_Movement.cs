using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Movement : MonoBehaviour
{
    public LayerMask scaleLayer;
    float moveSpeed = 0.1f;
    /*IEnumerator Move(Vector2 moveVec)
    {
        animator.MoveX = moveVec.x;
        animator.MoveY = moveVec.y;

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }*/


    private void CheckForEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, scaleLayer) != null)
        {
            
        }
    }
}
