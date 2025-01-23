using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats
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

    public string name = "";

    public bool allied = true;
}
