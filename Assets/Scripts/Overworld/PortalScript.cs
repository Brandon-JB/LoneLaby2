using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    //set #'s for each portal
    public static int whereGo;
    public static int LastPortal = 0;
    // 1 = City, 2 = Cave, 3 = Mansion, 4 = Forest, 5 = Church
    
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
            LastPortal = 5;
        }
        else if (name == "CityPortal")
        {
            SceneManager.LoadScene("NoCombatAreas");
            LastPortal = 1;
        }
        else if (name == "CavePortal")
        {
            SceneManager.LoadScene("CombatMaps");
            //set var for where to go here
            LastPortal = 2;
        }
        else if (name == "MansionPortal")
        {
            SceneManager.LoadScene("CombatMansion");
            //set var for where to go here
            LastPortal = 3;
        }
        else if (name == "ForestPortal")
        {
            SceneManager.LoadScene("CombatForest");
            //set var for where to go here
            LastPortal = 4;
        }
    }
}

