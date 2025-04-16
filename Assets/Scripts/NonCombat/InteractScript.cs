using System.Collections;
using System.Collections.Generic;
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

    private bool gainedQuest = false; // SAVE THIS!!!!!!

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
        
        if (closeTo != null)
        {
            Debug.Log(closeTo.name);
            CanInteractUI.SetActive(true);

            if (InputManager.interactPressed == true && gainedQuest == false)
            {
                //Start Interaction

                gainedQuest = true;

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
                findDialogueToPlay("SideQuests/getAlanQuest", "SideQuests/getAlanQuest", "SideQuests/finAlanQuest", hasQuest, questCompleted);
                break;
            case "Kisa":
                findDialogueToPlay("SideQuests/getKisaQuest", "SideQuests/getKisaQuest", "SideQuests/finKisaQuest", hasQuest, questCompleted);
                break;
            case "Soph":
                findDialogueToPlay("SideQuests/getSophQuest", "SideQuests/getSophQuest", "SideQuests/finSophQuest", hasQuest, questCompleted);
                break;
            case "Vaang":
                findDialogueToPlay("Vaang/meetingVaang", "Vaang/vaang_condemn", "Vaang/vaang_save", hasQuest, questCompleted);
                break;
            case "Bed Trigger":
                break;

        }
    }

    private void findDialogueToPlay(string dialogue1, string dialogue2, string dialogue3, bool hasQuest, bool questCompleted)
    {
        //Add more to this if we need to do more with side quests
        if (hasQuest && questCompleted)
        {
            mainDialogueManager.dialogueSTART(dialogue3);
        }
        else if (hasQuest)
        {
            mainDialogueManager.dialogueSTART(dialogue2);
        }
        else
        {
            mainDialogueManager.dialogueSTART(dialogue1);
        }
        
        CanInteractUI.SetActive(false);
    }
}
