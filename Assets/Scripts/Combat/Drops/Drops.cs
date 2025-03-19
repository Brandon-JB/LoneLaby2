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
    [SerializeField] private UIOnGameObject uiongame;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        UIHandler = FindObjectOfType<GameOverAndUI>();
        animator = GetComponent<Animator>();
        uiongame = GetComponent<UIOnGameObject>();
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
            
            switch(enemyName)
            {
                case "Slime":
                    animator.SetBool("ATKHPRing", true);
                    itemName = "ATKHPRing";
                    break;
                case "EarthElement":
                    animator.SetBool("ATKRing", true);
                    itemName = "AdvATKRing";
                    break;
                case "TreeMimic":
                    animator.SetBool("MPRing", true);
                    itemName = "AdvMPRing";
                    break;
                case "Sword":
                    animator.SetBool("ATKMPRing", true);
                    itemName = "ATKMPRing";
                    break;
                case "Spirit":
                    animator.SetBool("HPRing", true);
                    itemName = "AdvHPRing";
                    break;
                case "Shield":
                    animator.SetBool("HPMPRing", true);
                    itemName = "HPMPRing";
                    break;
            }
            /*
            if (enemyName == "Slime" || enemyName == "Sword")
            {
                animator.SetBool("ATKRing", true);
                itemName = "ATKRing";
            }
            if (enemyName == "EarthElement" || enemyName == "TreeMimic")
            {
                animator.SetBool("MPRing", true);
                itemName = "MPRing";
            }
            if (enemyName == "Spirit" || enemyName == "Shield")
            {
                animator.SetBool("HPRing", true);
                itemName = "HPRing";
            }*/
        }
    }

    public void WhatItemDo(BaseChar charScript, bool kisaAmuletEquipped)
    {
        if (!kisaAmuletEquipped)
        {
            switch (dropName)
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
                    UIHandler.OpenItemMenu(itemName, uiongame.spawnUiOnGameObject(itemName));
                    break;
            }
        }
        //double drops from kisa amulet
        else
        {
            switch (dropName)
            {
                case "Small HP":
                    charScript.Heal(50);
                    break;
                case "Small MP":
                    charScript.RestoreMana(4);
                    break;
                case "Large HP":
                    charScript.Heal(100);
                    break;
                case "Large MP":
                    charScript.RestoreMana(8);
                    break;
                case "Item":
                    //Debug.Log("Item");
                    UIHandler.OpenItemMenu(itemName, uiongame.spawnUiOnGameObject(itemName));
                    break;
            }
        }
    }
}
