using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldOnLoad : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] SpawnSpots;
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        if (PortalScript.LastPortal == 1)
        {
            //City
            Animator.SetTrigger("IsTown");
            Animator.SetTrigger("Start");
            Player.transform.position = SpawnSpots[0].transform.position;
            //Animator.SetBool("IsTown", false);
        }
        else if (PortalScript.LastPortal == 2)
        {
            //Cave
            Animator.SetTrigger("IsCave");
            Animator.SetTrigger("Start");
            Player.transform.position = SpawnSpots[1].transform.position;
            //Animator.SetBool("IsCave", false);
        }
        else if (PortalScript.LastPortal == 3)
        {
            //Mansion
            Animator.SetTrigger("IsMansion");
            Animator.SetTrigger("Start");
            //Animator.SetBool("IsMansion", false);
            Player.transform.position = SpawnSpots[2].transform.position;
        }
        else if (PortalScript.LastPortal == 4)
        {
            //Forest
            Animator.SetTrigger("IsForest");
            Animator.SetTrigger("Start");
            Player.transform.position = SpawnSpots[3].transform.position;
            //Animator.SetBool("IsForest", false);
        }
        else if (PortalScript.LastPortal == 6)
        {
            //Random Fight
            Animator.SetTrigger("IsFight");
            Animator.SetTrigger("Start");

            //For now setting at city, change to where the player collided last
            Player.transform.position = SpawnSpots[0].transform.position;
            //Animator.SetBool("IsFight", false);
        }
        else
        {
            //City (base)
            Animator.SetTrigger("IsTown");
            Animator.SetTrigger("Start");

            Player.transform.position = SpawnSpots[0].transform.position;
            //Animator.SetBool("IsTown", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
