using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinTutorial : BaseChar
{
    public Tutorial tutorialScript;
    public Sprite idleSprite;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator.SetBool("Tutorial", true);

        ChangeStats(0, 0, 99999999, 9999999, 9999999);
        allied = false;
        charName = "SevTutorial";
    }

    public void Parried()
    {
        animator.enabled = false;
        spriteRenderer.sprite = idleSprite;
        tutorialScript.EndTutorial();
    }
}
