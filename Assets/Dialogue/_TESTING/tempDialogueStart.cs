using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDialogueStart : MonoBehaviour
{
    [SerializeField] mainDialogueManager MDM;

    [SerializeField] private string fileName = "introducingSuspects";

    private void Start()
    {
        Debug.Log("I am in the dialogue scene");
        MDM.dialogueSTART(fileName);
    }
}
