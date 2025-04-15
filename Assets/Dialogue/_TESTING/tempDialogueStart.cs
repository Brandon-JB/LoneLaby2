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
            mainDialogueManager.GLOBALcurrentlyRunningText = "";
            MDM.dialogueSTART(fileName);
        } else if (SceneManager.GetActiveScene().name == "NoCombatAreas")
        {
            //Another temporary fix
            Time.timeScale = 1f;
        }
    }

    public void StartDialogue()
    {
        MDM.dialogueSTART(fileName);
    }
}
