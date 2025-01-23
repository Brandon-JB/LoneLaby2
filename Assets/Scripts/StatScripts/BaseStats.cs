using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public Dictionary<string, int> statsSheet = new Dictionary<string, int>()
    {
        {"Strength", 10},
        {"Defense", 4},
        {"Health", 20},
        {"MaxHealth", 20},
        {"Mana", 4},
        {"MaxMana", 4}
    };

    protected string charName = "";

    protected bool allied = true;

    protected Animator animator = null;

    protected void SetMaxHealth()
    {
        statsSheet["Health"] = statsSheet["MaxHealth"];
    }

    protected void SetMaxMana()
    {
        statsSheet["Mana"] = statsSheet["MaxMana"];
    }

    protected int GetHealth()
    {
        return statsSheet["Health"];
    }

    protected void SetHealth(int health)
    {
        statsSheet["Health"] = health;
    }

    protected int GetMana()
    {
        return statsSheet["Mana"];
    }

    protected void SetMana(int mana)
    {
        statsSheet["Mana"] = mana;
    }

    protected void GotDamaged(int incomingDamage)
    {
        SetHealth(GetHealth() - incomingDamage);
    }

    protected void Death()
    {

    }

    protected void TriggerAttackAnim()
    {
        animator.SetBool("Attacking", true);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
