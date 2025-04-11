using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDialogueStart : MonoBehaviour
{
    [SerializeField] mainDialogueManager MDM;

    [SerializeField] private string fileName = "introducingSuspects";

    private void Start()
    {
        MDM.dialogueSTART(fileName);
    }
}
