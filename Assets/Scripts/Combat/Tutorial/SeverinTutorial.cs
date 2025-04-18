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
    private LeoraChar2 leoraChar;
    public float interactRange;

    private void Start()
    {
        interactable = false;

        leoraChar = FindObjectOfType<LeoraChar2>();
        Player = leoraChar.gameObject;

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
                leoraChar.interactIcon.SetActive(true);

                if (InputManager.interactPressed)
                {
                    //Put interactions here
                    tutorialScript.EndTutorial();
                }
            }
            else
            {
                leoraChar.interactIcon.SetActive(false);
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
