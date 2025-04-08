using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentEntry
{
    public string itemName;
    public bool isUnlocked;

    public EquipmentEntry(string name, bool unlocked)
    {
        itemName = name;
        isUnlocked = unlocked;
    }
}
