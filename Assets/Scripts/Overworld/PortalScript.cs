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
    public GameObject Player;
    public GameObject ChurchGO;
    public GameObject ChurchLEAVE;
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
            whereGo = 6;
            SceneManager.LoadScene("Overworld");
        }
        else if (name == "Church")
        {
            Player.transform.position = ChurchGO.transform.position;
            LastPortal = 5;
            whereGo = 5;
        }
        else if (name == "ChurchLeave")
        {
            Player.transform.position = ChurchLEAVE.transform.position;
            LastPortal = 5;
            whereGo = 5;
        }
        else if (name == "CityPortal")
        {
            LastPortal = 1;
            whereGo = 1;
            PlayerMovement.CanWalk = false;
            LevelLoader.LoadNextLevel();
            

        }
        else if (name == "CavePortal")
        {
            //set var for where to go here
            LastPortal = 2;
            whereGo = 2;
            PlayerMovement.CanWalk = false;
            LevelLoader.LoadNextLevel();

        }
        else if (name == "MansionPortal")
        {
            LastPortal = 3;
            whereGo = 3;
            PlayerMovement.CanWalk = false;
            LevelLoader.LoadNextLevel();

        }
        else if (name == "ForestPortal")
        {
            LastPortal = 4;
            whereGo = 4;
            PlayerMovement.CanWalk = false;
            LevelLoader.LoadNextLevel();

        }
        else if (name == "Training")
        {
            SceneManager.LoadScene("TrainingGrounds");
        }
    }
}

