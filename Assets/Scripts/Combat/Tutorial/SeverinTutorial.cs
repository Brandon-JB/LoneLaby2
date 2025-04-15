using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinTutorial : BaseChar
{
    public Tutorial tutorialScript;
    public Sprite idleSprite;
    public SpriteRenderer spriteRenderer;
    public bool interactable;

    private float DistanceBetweenObjectAndPlayer;
    public GameObject Player;
    public float interactRange;

    private void Start()
    {
        interactable = false;

        Player = FindObjectOfType<LeoraChar2>().gameObject;

        animator.SetBool("Tutorial", true);

        ChangeStats(0, 0, 99999999, 9999999, 9999999);
        allied = false;
        charName = "SevTutorial";
    }

    public override void Update()
    {
        base.Update();

        if (interactable)
        {
            DistanceBetweenObjectAndPlayer = Vector2.Distance(transform.position, Player.transform.position);

            if (DistanceBetweenObjectAndPlayer <= interactRange)
            {
                if (InputManager.interactPressed)
                {
                    //Put interactions here
                    
                }
            }
        }
    }

    public void Parried()
    {
        animator.enabled = false;
        spriteRenderer.sprite = idleSprite;
        //tutorialScript.EndTutorial();
        tutorialScript.progressTutorial();
    }
}
