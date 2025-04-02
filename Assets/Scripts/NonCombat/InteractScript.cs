using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public int InteractionNumber;
    private string interactionName;
    private float maxDistance = 1.5f;
    private float DistanceBetweenObjects;
    private float InteractionLength = 3f;
    public GameObject Player;
    public GameObject InteractionUI;
    public GameObject CanInteractUI;

    // Start is called before the first frame update
    void Start()
    {
        interactionName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceBetweenObjects = Vector3.Distance(transform.position, Player.transform.position);
        if (DistanceBetweenObjects <= maxDistance)
        {
            CanInteractUI.SetActive(true);

            if (InputManager.interactPressed == true)
            {
                //Start Interaction
                StartCoroutine(Interaction(InteractionLength, interactionName, InteractionNumber));
                Debug.Log("Pressed");
                
            }
        }
    }

    IEnumerator Interaction(float Seconds, string name, int Number)
    {
        InteractionUI.SetActive(true);
        CanInteractUI.SetActive(false);
        //Do interaction
        yield return new WaitForSeconds(Seconds);
        InteractionUI.SetActive(false);
        CanInteractUI.SetActive(true);
    }
}
