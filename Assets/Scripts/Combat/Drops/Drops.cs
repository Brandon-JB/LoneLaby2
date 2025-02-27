using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public string dropName;
    public string itemName = "";
    //public SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private GameOverAndUI UIHandler;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        UIHandler = FindObjectOfType<GameOverAndUI>();
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
            /*
            switch(enemyName)
            {
                case "Slime":
                    animator.SetBool("ATKRing", true);
                    itemName = "ATKRing";
                    break;
                case "EarthElement":
                    animator.SetBool("MPRing", true);
                    itemName = "MPRing";
                    break;
                case "TreeMimic":
                    break;
            }*/
            if (enemyName == "Slime")
            {
                animator.SetBool("ATKRing", true);
                itemName = "ATKRing";
            }
            if (enemyName == "EarthElement" || enemyName == "TreeMimic")
            {
                animator.SetBool("MPRing", true);
                itemName = "MPRing";
            }

            //Insert other enemies when they get added
        }
    }

    public void WhatItemDo(BaseChar charScript)
    {
        switch(dropName)
        {
            case "Small HP":
                charScript.Heal(25);
                break;
            case "Small MP":
                charScript.RestoreMana(2);
                break;
            case "Large HP":
                charScript.Heal(50);
                break;
            case "Large MP":
                charScript.RestoreMana(4);
                break;
            case "Item":
                //Debug.Log("Item");

                UIHandler.OpenItemMenu(itemName);

                break;
        }
    }
}
