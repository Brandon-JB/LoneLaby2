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

    public string charName = "";

    public bool allied = true;

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
}
