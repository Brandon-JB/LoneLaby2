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
    public bool isClose;
    public GameObject Player;
    public GameObject InteractionUI;
    public GameObject CanInteractUI;
    public mainDialogueManager mainDialogueManager;

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
        if(IsCloseEnough(DistanceBetweenObjects))
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }

        if (isClose)
        {
            CanInteractUI.SetActive(true);

            if (InputManager.interactPressed == true && gainedQuest == false)
            {
                //Start Interaction
                gainedQuest = true;
                Debug.Log("THISIS THE NAME OF THE FILE:" + interactionName);
                mainDialogueManager.dialogueSTART(interactionName);
                CanInteractUI.SetActive(false);
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

    public bool IsCloseEnough(float[] distBtwnObjs)
    {
        //Debug.Log("called");
        foreach (int element in distBtwnObjs)
        {
            if (element <= 1.5f)
            {
                Debug.Log("true");
                return true;
            }
        }
            return false;
    }
}
