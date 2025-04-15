using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private mainDialogueManager mdm;
    private string currentlyRunningDialogue;

    private void Start()
    {
        //SEND THE NAME OF THE DIALOGUE TO TEMP DIALOGUE START.FILE NAME INSTEAD OF STARTING IT HERE - Alex

        currentlyRunningDialogue = mainDialogueManager.GLOBALcurrentlyRunningText;

        switch(currentlyRunningDialogue)
        {
            case "IvarQuest/manor_postfight_saveIvar":
                mdm.dialogueSTART("IvarQuest/manor_postfight_saveVerita");
                break;
            case "IvarQuest/manor_postfight_condemnIvar":
                mdm.dialogueSTART("IvarQuest/manor_postfight_condemnSpeaker");
                break;
            case "LucanQuest/cave_postfight_saveLucan":
                mdm.dialogueSTART("LucanQuest/cave_postfight_saveVerita");
                break;
            case "LucanQuest/cave_postfight_condemnLucan":
                mdm.dialogueSTART("LucanQuest/cave_postfight_condemnSpeaker");
                break;
            case "ViinQuest/veinwood_postfight_saveViin":
                mdm.dialogueSTART("ViinQuest/veinwood_postfight_saveVerita");
                break;
            case "ViinQuest/veinwood_postfight_condemnViin":
                mdm.dialogueSTART("ViinQuest/veinwood_postfight_condemnSpeaker");
                break;
            default:
                mdm.dialogueSTART("introducingSuspects");
                break;

        }
    }
}
