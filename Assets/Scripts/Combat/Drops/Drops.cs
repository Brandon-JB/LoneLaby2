using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public string dropName;
    //public SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetUpItem(string lName, string enemyName)
    {
        dropName = lName;

        if (dropName == "Small HP")
        {
            animator.SetBool("smallHP", true);
        }
        else if (dropName == "Small MP")
        {
            animator.SetBool("smallMP", true);
        }
        else if (dropName == "Large HP")
        {
            animator.SetBool("largeHP", true);
        }
        else if (dropName == "Large MP")
        {
            animator.SetBool("largeMP", true);
        }
        else if (dropName == "Item")
        {
            if (enemyName == "Slime")
            {
                animator.SetBool("ATKRing", true);
            }
            if (enemyName == "EarthElement")
            {
                animator.SetBool("MPRing", true);
            }
            //Insert other enemies when they get added
        }
    }

    public void WhatItemDo(BaseChar charScript)
    {
        if (dropName == "Small HP")
        {
            charScript.Heal(25);
        }
        else if (dropName == "Small MP")
        {
            charScript.RestoreMana(2);
        }
        else if (dropName == "Large HP")
        {
            charScript.Heal(50);
        }
        else if (dropName == "Large MP")
        {
            charScript.RestoreMana(4);
        }
        else if (dropName == "Item")
        {

        }
    }
}
