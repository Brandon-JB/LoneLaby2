using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{

    public static bool canOpenPause = false;

    [SerializeField] private GameObject quickPauseMenu;
    [SerializeField] private GameObject pauseMenuObject;


    // Update is called once per frame
    void Update()
    {
        //I don't know how to do the user input stuff so we can do that later

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canOpenPause)
            {
                //Open main pause menu
                pauseMenuObject.SetActive(!pauseMenuObject.activeInHierarchy);
            } else
            {
                //Open quick pause
                quickPauseMenu.SetActive(!quickPauseMenu.activeInHierarchy);
            }
        }
    }
}
