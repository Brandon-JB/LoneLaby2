using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempDialogueStart : MonoBehaviour
{
    [SerializeField] mainDialogueManager MDM;

    [SerializeField] private GameObject tutorial;

    [SerializeField] private string fileName = "introducingSuspects";


    private void Start()
    {
        if(mainDialogueManager.GLOBALcurrentlyRunningText == "introducingSuspects" && SceneManager.GetActiveScene().name == "NoCombatAreas")
        {
            mainDialogueManager.GLOBALcurrentlyRunningText = "";
            Time.timeScale = 0f;
            OpenPauseMenu.GLOBALcanOpenPause = false;
            tutorial.SetActive(true);
        } 
        //TEMPORARY!!
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Time.timeScale = 1f;
            mainDialogueManager.GLOBALcurrentlyRunningText = "";
        } else if (SceneManager.GetActiveScene().name == "NoCombatAreas")
        {
            //Another temporary fix
            Time.timeScale = 1f;
        } else if (SceneManager.GetActiveScene().name == "Cutscenes")
        {
            switch (mainDialogueManager.GLOBALcurrentlyRunningText)
            {
                case "IvarQuest/manor_postfight_saveIvar":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 1;
                    fileName = "IvarQuest/manor_postfight_saveVerita";
                    break;
                case "IvarQuest/manor_postfight_condemnIvar":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 0;
                    fileName = "IvarQuest/manor_postfight_condemnSpeaker";
                    break;
                case "LucanQuest/cave_postfight_saveLucan":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 1;
                    fileName = "LucanQuest/cave_postfight_saveVerita";
                    break;
                case "LucanQuest/cave_postfight_condemnLucan":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 0;
                    fileName = "LucanQuest/cave_postfight_condemnSpeaker";
                    break;
                case "ViinQuest/veinwood_postfight_saveViin":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 1;
                    fileName = "ViinQuest/veinwood_postfight_saveVerita";
                    break;
                case "ViinQuest/veinwood_postfight_condemnViin":
                    CutsceneSpawnManager.CutsceneSpawnpoint = 0;
                    fileName = "ViinQuest/veinwood_postfight_condemnSpeaker";
                    break;
                default:
                    CutsceneSpawnManager.CutsceneSpawnpoint = 0;
                    fileName = "introducingSuspects";
                    break;

            }
        }
    }

    public void StartDialogue()
    {
        MDM.dialogueSTART(fileName);
    }

}
