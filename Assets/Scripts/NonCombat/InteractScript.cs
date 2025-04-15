using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public int InteractionNumber;
    public string interactionName;
    private float maxDistance = 1.5f;
    private float DistanceBetweenObjects;
    private float InteractionLength = 3f;
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
        DistanceBetweenObjects = Vector3.Distance(transform.position, Player.transform.position);
        //The problem is it's somehow 10 units off when you first load into the scene. is it something that has to do with the church?
        if (DistanceBetweenObjects <= maxDistance)
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
}
