using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questID;
    public bool isActive;
    public bool isComplete;
    public int currentProgress;
    public int requiredProgress;

    public QuestData(string id, int required)
    {
        questID = id;
        requiredProgress = required;
        isActive = true;
        isComplete = false;
        currentProgress = 0;
    }

}
