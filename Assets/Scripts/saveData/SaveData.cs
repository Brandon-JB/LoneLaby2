using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fucking kill me
[System.Serializable]
public class SaveData
{
    public string currentScene;
    public Vector3 playerPosition;

    public List<EquipmentEntry> equipmentObtained;
    public List<EquipmentEntry> equippedAmuletSlot;
    public List<EquipmentEntry> equippedRingSlot1;
    public List<EquipmentEntry> equippedRingSlot2;
    public List<EquipmentEntry> equippedRings;

    public int bossesBeaten;
    public int bossesKilled;
    public int bossesSpared;

    public float currentHP;
    public float currentMana;

    public bool hasMansionKey;

    public List<SideQuestData> activeSideQuests;
    public List<string> completedSideQuests;
}

[System.Serializable]
public class SideQuestData
{
    public string questName;
    public int progress; 
}
