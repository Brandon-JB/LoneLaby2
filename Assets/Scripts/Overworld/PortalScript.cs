using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    //set #'s for each portal
    public static int whereGo;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TeleportPlayer(string name)
    {
        if (name == null)
        {
            Debug.Log("you fucked up");
        }
        else if (name == "Exit")
        {
            SceneManager.LoadScene("Overworld");
        }
        else if (name == "Church")
        {
            //send to church
        }
        else if (name == "CityPortal")
        {
            SceneManager.LoadScene("NoCombatAreas");
        }
        else if (name == "TowerPortal")
        {
            SceneManager.LoadScene("CombatMaps");
            //set var for where to go here
            whereGo = 1;
        }
        else if (name == "MansionPortal")
        {
            SceneManager.LoadScene("CombatMaps");
            //set var for where to go here
            whereGo = 2;
        }
        else if (name == "ForestPortal")
        {
            SceneManager.LoadScene("CombatMaps");
            //set var for where to go here
            whereGo = 3;
        }
    }
}

