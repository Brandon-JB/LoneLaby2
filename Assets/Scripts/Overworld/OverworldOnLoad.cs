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
            Animator.SetBool("IsTown", true);
            Animator.SetTrigger("Start");
            Player.transform.position = SpawnSpots[0].transform.position;
        }
        else if (PortalScript.LastPortal == 2)
        {
            //Cave
            Player.transform.position = SpawnSpots[1].transform.position;
        }
        else if (PortalScript.LastPortal == 3)
        {
            //Mansion
            Player.transform.position = SpawnSpots[2].transform.position;
        }
        else if (PortalScript.LastPortal == 4)
        {
            //Forest
            Player.transform.position = SpawnSpots[3].transform.position;
        }
        else
        {
            //City
            Animator.SetBool("IsTown", true);
            Animator.SetTrigger("Start");
            Player.transform.position = SpawnSpots[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
