using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animator : MonoBehaviour
{
    [SerializeField] List<Child> children;
    public float MoveX { get; set; }

    public float MoveY { get; set; }

    public bool IsMoving { get; set; }

    Sprite_Animator walkLeftAnim;
    Sprite_Animator walkIdleAnim;


    Sprite_Animator currentAnim;
    bool wasPrevMoving;


    SpriteRenderer spriteRenderer;

    private void Start()
    {
        var child = children[Random.Range(0, children.Count)];
        child.Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkLeftAnim = new Sprite_Animator(child.LeftSprite, spriteRenderer);
        walkIdleAnim = new Sprite_Animator(child.IdleSprite, spriteRenderer);

        currentAnim = walkIdleAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;

        if (MoveX == -1)
            currentAnim = walkLeftAnim;
        else
            currentAnim = walkIdleAnim;

        if (currentAnim != prevAnim || IsMoving != wasPrevMoving)
            currentAnim.Start();
        
        if (IsMoving)
            currentAnim.HandleUpdate();
        else
            spriteRenderer.sprite = currentAnim.Frames[0];
        
        wasPrevMoving = IsMoving;
    }
}
