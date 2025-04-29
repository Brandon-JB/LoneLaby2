using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionDoorManager : CombatInteraction
{
    public static bool hasKey = false;
    public static bool DoorOpened;
    //public GameObject Player;
    public GameObject Door;
    [SerializeField] private GameObject keyUI;


    // Update is called once per frame
    public override void Update()
    {
        if (DoorOpened)
        {
            Door.SetActive(false);
        }

        DistanceBetweenObjectAndPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (alreadyInteracted == false && DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (hasKey)
            {
                leoraChar.closestInteractable = this.gameObject;
                leoraChar.interactIcon.SetActive(true);

                if (InputManager.interactPressed == true)
                {
                    audioManager.Instance.playSFX(60);
                    keyUI.SetActive(false);
                    alreadyInteracted = true;
                    Door.SetActive(false);
                    DoorOpened = true;
                }
            }
        }
        else if (leoraChar.closestInteractable != null && leoraChar.closestInteractable == this.gameObject)
        {
            leoraChar.interactIcon.SetActive(false);
        }
    }
}
