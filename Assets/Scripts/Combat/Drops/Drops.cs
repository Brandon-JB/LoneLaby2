using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public string dropName;
    public string itemName = "";
    //public SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private ItemMenu UIHandler;
    [SerializeField] private UIOnGameObject uiongame;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        UIHandler = FindObjectOfType<ItemMenu>();
        animator = GetComponent<Animator>();

        //Used for spawning specific drops
        SetUpItem(dropName, "");
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
                    audioManager.Instance.playSFX(49);
                    charScript.Heal(25);
                    break;
                case "Small MP":
                    charScript.RestoreMana(2);
                    audioManager.Instance.playSFX(50);
                    break;
                case "Large HP":
                    charScript.Heal(50);
                    audioManager.Instance.playSFX(47);
                    break;
                case "Large MP":
                    charScript.RestoreMana(4);
                    audioManager.Instance.playSFX(48);
                    break;
                case "Item":
                    //Debug.Log("Item");
                    if(UIHandler == null)
                    {
                        UIHandler = FindObjectOfType<ItemMenu>();
                    }
                    audioManager.Instance.playSFX(44);
                    UIHandler.openItemMenu(itemName);
                    //UIHandler.OpenItemMenu(itemName, uiongame.spawnUiOnGameObject(itemName));
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
                    if (UIHandler == null)
                    {
                        UIHandler = FindObjectOfType<ItemMenu>();
                    }
                    //Debug.Log("Item");
                    UIHandler.openItemMenu(itemName);
                    //UIHandler.OpenItemMenu(itemName, uiongame.spawnUiOnGameObject(itemName));
                    break;
            }
        }
    }
}
