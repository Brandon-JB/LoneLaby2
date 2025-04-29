using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public GameObject Player;
    public EquipmentManager equipmentManager;
    public LeoraChar2 leoraChar;
    //public QuestManager questManager;

    // SO IT DOESNT GET DESTROYED BETWEEN SCENES
    public static SaveManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance.Player = GameObject.Find("CombatPlayer");
            Instance.leoraChar = GameObject.Find("CombatPlayer").GetComponent<LeoraChar2>();

            /*if (Instance.Player != null)
            {
                Instance.leoraChar = Instance.Player.gameObject.GetComponent<LeoraChar2>();
            }*/

            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
        Instance.Player = GameObject.Find("CombatPlayer");
        Instance.leoraChar = GameObject.Find("CombatPlayer").GetComponent<LeoraChar2>();
        DontDestroyOnLoad(gameObject); // keep this alive across scenes

        
    }


    //SAVE STUFF BELOW





    public void SaveGame()
    {
        Instance.leoraChar.SetMaxHealth();
        Instance.leoraChar.SetMaxMana();

        SaveData data = new SaveData();

        //Equipment
        data.equipmentObtained = ConvertDictToList(EquipmentManager.equipmentObtained);
        data.equippedAmuletSlot = ConvertDictToList(EquipmentManager.amuletSlot);
        data.equippedRingSlot1 = ConvertDictToList(EquipmentManager.ringSlot1);
        data.equippedRingSlot2 = ConvertDictToList(EquipmentManager.ringSlot2);
        data.equippedRings = ConvertDictToList(EquipmentManager.equippedRings);

        //Quests
        data.quests = ConvertQuestsToList(QuestManager.questStates);

        //Bosses Dead
        data.bossStates = ConvertBossDictToList(BossSaveData.bossStates);

        //Other Stats
        data.currentScene = SceneManager.GetActiveScene().name;
        data.playerPosition = Player.transform.position;
        //data.currentHP = leoraChar.GetHealth();
        //data.currentMana = leoraChar.GetMana();
        data.mansionDoorOpened = MansionDoorManager.DoorOpened;
        data.LastPortal = PortalScript.LastPortal;


        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();

        if (data == null) return;

        
        StartCoroutine(LoadAfterSceneLoad(data));
    }

    public static void DeleteSaveData()
    {

        SaveSystem.Deletegame();

    }

    private IEnumerator LoadAfterSceneLoad(SaveData data)
    {
        SceneManager.LoadScene(data.currentScene);
        yield return new WaitForSeconds(0.5f);

        //Load all data

        //Equipment
        RebuildDictFromList(data.equipmentObtained, EquipmentManager.equipmentObtained);
        RebuildDictFromList(data.equippedAmuletSlot, EquipmentManager.amuletSlot);
        RebuildDictFromList(data.equippedRingSlot1, EquipmentManager.ringSlot1);
        RebuildDictFromList(data.equippedRingSlot2, EquipmentManager.ringSlot2);
        RebuildDictFromList(data.equippedRings, EquipmentManager.equippedRings);

        //Quests
        QuestManager.ClearAllQuests();
        RebuildQuestDict(data.quests);

        //Bosses Dead
        RebuildBossDictFromList(data.bossStates, BossSaveData.bossStates);

        //Other Stats
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = data.playerPosition;
        MansionDoorManager.DoorOpened = data.mansionDoorOpened;
        PortalScript.LastPortal = data.LastPortal;
        Instance.leoraChar.SetMaxHealth();
        Instance.leoraChar.SetMaxMana();

        Debug.Log(MansionDoorManager.DoorOpened);
        Debug.Log(PortalScript.LastPortal);
    }

    private List<EquipmentEntry> ConvertDictToList(Dictionary<string, bool> dict)
    {
        if (dict == null)
        {
            return null;
        }

        List<EquipmentEntry> list = new List<EquipmentEntry>();
        foreach (var pair in dict)
        {
            list.Add(new EquipmentEntry(pair.Key, pair.Value));
        }
        return list;
    }

    private void RebuildDictFromList(List<EquipmentEntry> list, Dictionary<string, bool> dict)
    {
        if(list != null)
        {
            foreach (var entry in list)
            {
                if (dict.ContainsKey(entry.itemName))
                    dict[entry.itemName] = entry.isUnlocked;
            }
        }
        else
        {
            return;
        }
        
    }

    private List<BossStateEntry> ConvertBossDictToList(Dictionary<string, int> dict)
    {
        if (dict == null)
        {
            return null;
        }

        List<BossStateEntry> list = new List<BossStateEntry>();
        foreach (var kvp in dict)
            list.Add(new BossStateEntry(kvp.Key, kvp.Value));
        return list;
    }

    private void RebuildBossDictFromList(List<BossStateEntry> list, Dictionary<string, int> dict)
    {
        if (list == null)
        {
            return;
        }

        foreach (var entry in list)
        {
            if (dict.ContainsKey(entry.bossName))
                dict[entry.bossName] = entry.state;
        }
    }

    private List<QuestSaveEntry> ConvertQuestsToList(Dictionary<string, QuestData> dict)
    {
        if (dict == null)
        {
            return null;
        }

        List<QuestSaveEntry> list = new List<QuestSaveEntry>();
        foreach (var quest in dict.Values)
        {
            list.Add(new QuestSaveEntry
            {
                questID = quest.questID,
                isActive = quest.isActive,
                isComplete = quest.isComplete,
                currentProgress = quest.currentProgress,
                requiredProgress = quest.requiredProgress
            });
        }
        return list;
    }

    private void RebuildQuestDict(List<QuestSaveEntry> list)
    {
        if (list == null)
        {
            Debug.LogError("Quest list is null, cannot rebuild quest dictionary.");
            return; // Or handle the error appropriately
        }

        foreach (var entry in list)
        {
            var newQuest = new QuestData(entry.questID, entry.requiredProgress)
            {
                isActive = entry.isActive,
                isComplete = entry.isComplete,
                currentProgress = entry.currentProgress
            };

            QuestManager.questStates[entry.questID] = newQuest;
        }
    }

    public static bool Isdata()
    {
        if(SaveSystem.DataExists())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
