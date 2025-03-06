using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    //set #'s for each portal
    public static int whereGo;
    public static int LastPortal = 0;

    public LevelLoader LevelLoader;
    // 1 = City, 2 = Cave, 3 = Mansion, 4 = Forest, 5 = Church, 6 = Overworld
    
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
            whereGo = 6;
        }
        else if (name == "Church")
        {
            //send to church
            LastPortal = 5;
            whereGo = 5;
        }
        else if (name == "CityPortal")
        {
            LevelLoader.LoadNextLevel();
            LastPortal = 1;
            whereGo = 1;
        }
        else if (name == "CavePortal")
        {
            LevelLoader.LoadNextLevel();
            //set var for where to go here
            LastPortal = 2;
            whereGo = 2;
        }
        else if (name == "MansionPortal")
        {
            LevelLoader.LoadNextLevel();
            //set var for where to go here
            LastPortal = 3;
            whereGo = 3;
        }
        else if (name == "ForestPortal")
        {
            LevelLoader.LoadNextLevel();
            //set var for where to go here
            LastPortal = 4;
            whereGo = 4;
        }
    }
}

