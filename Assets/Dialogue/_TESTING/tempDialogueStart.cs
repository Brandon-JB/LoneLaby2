using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDialogueStart : MonoBehaviour
{
    [SerializeField] mainDialogueManager MDM;

    [SerializeField] private GameObject tutorial;

    [SerializeField] private string fileName = "introducingSuspects";

    private void Start()
    {
        if(mainDialogueManager.GLOBALcurrentlyRunningText == "introducingSuspects")
        {
            Time.timeScale = 0f;
            OpenPauseMenu.GLOBALcanOpenPause = false;
            tutorial.SetActive(true);
        }
    }

    public void StartDialogue()
    {
        MDM.dialogueSTART(fileName);
    }
}
