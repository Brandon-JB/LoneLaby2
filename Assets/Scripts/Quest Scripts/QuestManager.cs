using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // SO IT DOESNT GET DESTROYED BETWEEN SCENES
    public static QuestManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // keep this alive across scenes
    }

    //ALL OF THE ACTUAL QUEST STUFF

    public static Dictionary<string, QuestData> questStates = new Dictionary<string, QuestData>();

    // Call this to start a quest
    public static void StartQuest(string questID, int requiredProgress)
    {
        if (!questStates.ContainsKey(questID))
            questStates.Add(questID, new QuestData(questID, requiredProgress));
    }

    // Call this when progress is made (e.g., enemy killed)
    public static void AddProgress(string questID, int amount = 1)
    {
        if (!questStates.ContainsKey(questID)) return;
        QuestData quest = questStates[questID];

        if (!quest.isActive || quest.isComplete) return;

        quest.currentProgress += amount;

        if (quest.currentProgress >= quest.requiredProgress)
        {
            quest.currentProgress = quest.requiredProgress;
            quest.isComplete = true;
            quest.isActive = false;
            Debug.Log($"Quest {questID} completed!");
        }
    }

    public static bool IsQuestComplete(string questID)
    {
        return questStates.ContainsKey(questID) && questStates[questID].isComplete;
    }

    public static void ClearAllQuests()
    {
        questStates.Clear();
    }




}
