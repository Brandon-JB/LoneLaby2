using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPauseMenu : MonoBehaviour
{

    public static bool canOpenPause = true;

    [SerializeField] private GameObject quickPauseMenu;
    [SerializeField] private GameObject pauseMenuObject;


    // Update is called once per frame
    void Update()
    {
        //I don't know how to do the user input stuff so we can do that later

        //NOTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11 COULD MESS UP TIMESCALE JUNK! 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canOpenPause)
            {
                //Open main pause menu
                //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
                Time.timeScale = Convert.ToInt32(pauseMenuObject.activeInHierarchy);
                pauseMenuObject.SetActive(!pauseMenuObject.activeInHierarchy);
            } else
            {
                //Open quick pause
                //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
                Time.timeScale = Convert.ToInt32(quickPauseMenu.activeInHierarchy);
                quickPauseMenu.SetActive(!quickPauseMenu.activeInHierarchy);
            }
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
