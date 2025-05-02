using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject Player;
    public saveUI saveui;
    private float maxDistance = 1.5f;
    private float DistanceBetweenObjects;
    public GameObject interactionPopup;
    private bool wasCloseTo = false;
    //private float InteractionLength = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DistanceBetweenObjects = Vector2.Distance(transform.position, Player.transform.position);
        //Debug.Log("Distance: " + DistanceBetweenObjects + " / Max Distance: " + maxDistance);
        if (DistanceBetweenObjects <= maxDistance)
        {
            if(interactionPopup == null)
            {
                interactionPopup = GameObject.FindObjectOfType<KeyPromptUI>().gameObject;
            }
            wasCloseTo = true;
            Debug.Log("I AM SETTING IT ACTIVE");
            interactionPopup.SetActive(true);

            if (InputManager.interactPressed == true && saveUI.isSaveOpen == false && OpenPauseMenu.GLOBALcanOpenPause)
            {
                saveui.OPENSAVEMENU();
            }
        }
        else if (wasCloseTo)
        {
            interactionPopup.SetActive(false);
            wasCloseTo = false;
        }
    }
}
