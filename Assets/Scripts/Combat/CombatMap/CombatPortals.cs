using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatPortals : MonoBehaviour
{

    public GameObject[] Portal;
    public GameObject[] Destination;
    public GameObject Player;


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
        Debug.Log(name);
        if (name == "Floor1-2")
        {
            Player.transform.position = Destination[0].transform.position;
        }

        if (name == "Floor2-1")
        {
            Player.transform.position = Destination[1].transform.position;
        }

        if (name == "Floor2-3")
        {
            Player.transform.position = Destination[2].transform.position;
        }

        if (name == "Floor3-2")
        {
            Player.transform.position = Destination[3].transform.position;
        }

        if (name == "Exit")
        {
            SceneManager.LoadScene("NoCombatAreas");
        }
    }
}
