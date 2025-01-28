using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    //set #'s for each portal

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TeleportPlayer()
    {
        //Send player to Combat
        SceneManager.LoadScene("CombatMaps");
    }
}

