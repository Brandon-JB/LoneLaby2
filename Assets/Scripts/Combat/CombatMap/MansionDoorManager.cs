using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionDoorManager : MonoBehaviour
{
    public static bool hasKey = false;
    public static bool DoorOpened;
    public GameObject Player;
    public GameObject Door;

    private float maxDistance = 3f;
    private float DistanceBetweenObjects;


    // Update is called once per frame
    void Update()
    {
        if (DoorOpened)
        {
            Door.SetActive(false);
        }

        DistanceBetweenObjects = Vector3.Distance(Door.transform.position, Player.transform.position);

        if (DistanceBetweenObjects <= maxDistance)
        {
            
            if (hasKey && InputManager.interactPressed == true)
            {
                Door.SetActive(false);
                DoorOpened = true;
            }
        }
    }
}
