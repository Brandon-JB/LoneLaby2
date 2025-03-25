using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDialogueStart : MonoBehaviour
{
    [SerializeField] mainDialogueManager MDM;

    private void Start()
    {
        Debug.Log("I am in the dialogue scene");
        MDM.dialogueSTART("introducingSuspects");
    }
}
