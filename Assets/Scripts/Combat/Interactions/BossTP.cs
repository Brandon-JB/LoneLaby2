using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTP : CombatInteraction
{
    public GameObject bossTP;
    public string bossFightDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (InputManager.interactPressed)
            {
                Player.transform.position = bossTP.transform.position;
                audioManager.Instance.playBGM("T8");
                //Temp way to play boss dialogue
                GameObject.FindGameObjectWithTag("MainDialogueManager").GetComponent<mainDialogueManager>().dialogueSTART(bossFightDialogue);
            }
        }
    }
}
