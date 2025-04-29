using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointTigger : MonoBehaviour
{
    public static bool isFirstTime = true;

    public GameObject MDMGO;
    public mainDialogueManager mainDialogueManager;

    private void Start()
    {
        MDMGO = GameObject.FindGameObjectWithTag("MainDialogueManager");
        mainDialogueManager = MDMGO.GetComponent<mainDialogueManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.tag == "Player" && isFirstTime)
        {
            isFirstTime = false;
            mainDialogueManager.dialogueSTART("findSavePoint");
        }    
    }
}
