using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionPortals : MonoBehaviour
{
    public GameObject Floor1;
    public GameObject Floor2;
    public GameObject Floor3Enter;
    public GameObject Floor3Exit;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MansionStairs(string name)
    {
        if (name == "Floor1-2")
        {
            Player.transform.position =  Floor2.transform.position;
        }
        else if (name == "Floor2-1")
        {
            Player.transform.position = Floor1.transform.position;
        }
        else if (name == "Floor2-3")
        {
            Player.transform.position = Floor3Exit.transform.position;
        }
        else if (name == "Floor3-2")
        {
            Player.transform.position = Floor3Enter.transform.position;
        }
    }
}
