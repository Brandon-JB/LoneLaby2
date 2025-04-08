using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    public GameObject Player;
    public EquipmentManager equipmentManager;
    public LeoraChar2 leoraChar;
    //public QuestManager questManager;


    public void SaveGame()
    {
        SaveData data = new SaveData();

        //Equipment
        data.equipmentObtained = ConvertDictToList(EquipmentManager.equipmentObtained);
        data.equippedAmuletSlot = ConvertDictToList(EquipmentManager.amuletSlot);
        data.equippedRingSlot1 = ConvertDictToList(EquipmentManager.ringSlot1);
        data.equippedRingSlot2 = ConvertDictToList(EquipmentManager.ringSlot2);
        data.equippedRings = ConvertDictToList(EquipmentManager.equippedRings);

        //Quests

        //Bosses Dead
        data.bossStates = ConvertBossDictToList(BossSaveData.bossStates);

        //Other Stats
        data.currentScene = SceneManager.GetActiveScene().name;
        data.playerPosition = Player.transform.position;
        data.currentHP = leoraChar.GetHealth();
        data.currentMana = leoraChar.GetMana();
        data.mansionDoorOpened = MansionDoorManager.DoorOpened;


        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();

        if (data == null) return;

        SceneManager.LoadScene(data.currentScene);
        StartCoroutine(LoadAfterSceneLoad(data));
    }

    private IEnumerator LoadAfterSceneLoad(SaveData data)
    {
        yield return new WaitForSeconds(0.5f);

        //Load all data

        //Equipment
        RebuildDictFromList(data.equipmentObtained, EquipmentManager.equipmentObtained);
        RebuildDictFromList(data.equippedAmuletSlot, EquipmentManager.amuletSlot);
        RebuildDictFromList(data.equippedRingSlot1, EquipmentManager.ringSlot1);
        RebuildDictFromList(data.equippedRingSlot2, EquipmentManager.ringSlot2);
        RebuildDictFromList(data.equippedRings, EquipmentManager.equippedRings);

        //Quests

        //Bosses Dead
        RebuildBossDictFromList(data.bossStates, BossSaveData.bossStates);

        //Other Stats
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = data.playerPosition;
        MansionDoorManager.DoorOpened = data.mansionDoorOpened;
    }

    private List<EquipmentEntry> ConvertDictToList(Dictionary<string, bool> dict)
    {
        List<EquipmentEntry> list = new List<EquipmentEntry>();
        foreach (var pair in dict)
        {
            list.Add(new EquipmentEntry(pair.Key, pair.Value));
        }
        return list;
    }

    private void RebuildDictFromList(List<EquipmentEntry> list, Dictionary<string, bool> dict)
    {
        foreach (var entry in list)
        {
            if (dict.ContainsKey(entry.itemName))
                dict[entry.itemName] = entry.isUnlocked;
        }
    }

    private List<BossStateEntry> ConvertBossDictToList(Dictionary<string, int> dict)
    {
        List<BossStateEntry> list = new List<BossStateEntry>();
        foreach (var kvp in dict)
            list.Add(new BossStateEntry(kvp.Key, kvp.Value));
        return list;
    }

    private void RebuildBossDictFromList(List<BossStateEntry> list, Dictionary<string, int> dict)
    {
        foreach (var entry in list)
        {
            if (dict.ContainsKey(entry.bossName))
                dict[entry.bossName] = entry.state;
        }
    }

}
