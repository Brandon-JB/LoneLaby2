using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    public GameObject Player;
    public GameObject In;
    public GameObject Out;


    public void Teleport(string name)
    {
        if (name == "In")
        {
            Player.transform.position = Out.transform.position;
        }
        else if (name == "Out")
        {
            Player.transform.position = In.transform.position;
        }
        else if (name == "LowerChurchL")
        {
            //teleport upper L
        }
        else if (name == "LowerChurchR")
        {
            //teleport upper R
        }
        else if (name == "UpperChurchL")
        {
            //teleport Lower L
        }
        else if (name == "UpperChurchR")
        {
            //teleport Lower R
        }
    }
}
