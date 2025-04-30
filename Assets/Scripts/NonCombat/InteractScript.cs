using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public int InteractionNumber;
    public string interactionName;
    private float maxDistance = 1.5f;
    public float[] DistanceBetweenObjects;
    private float InteractionLength = 3f;
    public GameObject[] NPCs;
    //public bool isClose;
    public GameObject Player;
    public GameObject InteractionUI;
    public GameObject CanInteractUI;
    public mainDialogueManager mainDialogueManager;

    public GameObject closeTo;

    //private bool gainedQuest = false; // SAVE THIS!!!!!!
    private Dictionary<string, bool> gainedQuests = new Dictionary<string, bool>()
    {
        {"Alan", false},
        {"Sophie", false},
        {"Vaang", false },
        {"BedTrigger", false },
        {"Kisa", false }
    };

    // Start is called before the first frame update
    void Start()
    {
        //interactionName = gameObject.name;
        mainDialogueManager = GameObject.FindGameObjectWithTag("MainDialogueManager").GetComponent<mainDialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject npc in NPCs)
        {
            for (int i = 0; i < NPCs.Length; i++)
            {
                DistanceBetweenObjects[i] = Vector3.Distance(NPCs[i].transform.position, Player.transform.position);
            }
        }

        closeTo = IsCloseEnough(DistanceBetweenObjects);
        
        //hi katie note that this is gonna break, we need a better system
        if (closeTo != null && gainedQuests[closeTo.name] == false)
        {
            Debug.Log(closeTo.name);
            CanInteractUI.SetActive(true);

            if (InputManager.interactPressed == true && OpenPauseMenu.GLOBALcanOpenPause)
            {
                //Start Interaction
                if (closeTo.name != "BedTrigger")
                {
                    gainedQuests[closeTo.name] = true;
                }
                CanInteractUI.SetActive(false);
                doSomethingBasedOnNPC(closeTo.name);
                Debug.Log(closeTo.name);
                //Debug.Log("THISIS THE NAME OF THE FILE:" + interactionName);


                //mainDialogueManager.dialogueSTART(interactionName);
                //CanInteractUI.SetActive(false);
                //StartCoroutine(Interaction(InteractionLength, interactionName, InteractionNumber));
                Debug.Log("Pressed");

            }
        }
        else
        {
            CanInteractUI.SetActive(false);
        }
    }

    IEnumerator Interaction(float Seconds, string name, int Number)
    {
        if (InteractionUI == null)
        {
            CanInteractUI.SetActive(false);
            StopCoroutine(Interaction(Seconds, name, Number));
        }
        InteractionUI.SetActive(true);
        CanInteractUI.SetActive(false);
        //Do interaction
        yield return new WaitForSeconds(Seconds);
        InteractionUI.SetActive(false);
        CanInteractUI.SetActive(true);
    }

    public GameObject IsCloseEnough(float[] distBtwnObjs)
    {
        int i = 0;
        //Debug.Log("called");
        foreach (float element in distBtwnObjs)
        {
            if (element <= 1.5f)
            {
                Debug.Log(NPCs[i].name + ": " + element);
                return NPCs[i];
            }
            i++;
        }
            return null;
    }

    public void doSomethingBasedOnNPC(string NPCName, bool hasQuest = false, bool questCompleted = false)
    {
        switch (NPCName)
        {
            case "Alan":
                findDialogueToPlay(NPCName, "SideQuests/getAlanQuest", "SideQuests/midAlanQuest", "SideQuests/finAlanQuest", "SideQuests/postAlanQuest", "AlanAmulet");
                break;
            case "Kisa":
                findDialogueToPlay(NPCName, "SideQuests/getKisaQuest", "SideQuests/midKisaQuest", "SideQuests/finKisaQuest", "SideQuests/postKisaQuest", "KisaAmulet");
                break;
            case "Sophie":
                findDialogueToPlay(NPCName, "SideQuests/getSophQuest", "SideQuests/midSophQuest", "SideQuests/finSophQuest", "SideQuests/postSophQuest", "SophieAmulet");
                break;
            case "Vaang":
                switch (BossSaveData.bossStates["Viin"])
                {
                    case 0: // Viin not encountered
                        mainDialogueManager.dialogueSTART("Vaang/meetingVaang"); 
                        break;
                    case 1: // Viin dead
                        mainDialogueManager.dialogueSTART("Vaang/vaang_condemn"); 
                        break;
                    case 2: // Viin saved
                        mainDialogueManager.dialogueSTART("Vaang/vaang_save"); 
                        break;
                }
                //findDialogueToPlay("Vaang/meetingVaang", "Vaang/vaang_condemn", "Vaang/vaang_save", NPCName);
                break;
            case "BedTrigger":
                //open menu asking if you'd like to end your game
                GameObject.FindObjectOfType<startFinalBosses>().openStartFinalBossMenu();
                break;

        }
    }

    private void findDialogueToPlay(string NPCName, string dialogue1, string dialogue2, string dialogue3, string dialogue4, string itemName = "")
    {
        //If the quest has been completed and the dialogue already seen, play dialogue 4
        if (EquipmentManager.equipmentObtained[itemName])
        {
            mainDialogueManager.dialogueSTART(dialogue4);
            CanInteractUI.SetActive(false);
            return;
        }

        try
        {
            //Check if they completed the quest or not
            if (QuestManager.IsQuestComplete(NPCName))
            {
                //Quest HAS been picked up and COMPLETED
                mainDialogueManager.dialogueSTART(dialogue3);
            } else if (QuestManager.IsOnQuest(NPCName))
            {
                //Quest has been picked up, not completed
                Debug.Log("They have the quest picked up");
                mainDialogueManager.dialogueSTART(dialogue2);
            }
            else
            {
                //They haven't started the quest
                Debug.Log("They do NOT have the quest picked up");
                mainDialogueManager.dialogueSTART(dialogue1);
                QuestManager.StartQuest(NPCName, 5);
            }
        }
        catch (Exception)
        {
            //Quest has not been picked up or something went wrong
            mainDialogueManager.dialogueSTART(dialogue1);
        }        
        CanInteractUI.SetActive(false);
    }
}
