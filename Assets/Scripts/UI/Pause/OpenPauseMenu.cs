using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenPauseMenu : MonoBehaviour
{

    public static bool canOpenPause = true;

    public static bool GLOBALcanOpenPause = true;

    [SerializeField] private GameObject quickPauseMenu;
    [SerializeField] private GameObject pauseMenuObject;

    //public EquipmentMenu equipmentMenu;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] public GameObject[] ringIcons_borders;
    [SerializeField] public GameObject[] amuletIcons_borders;

    private HUD_Equipment hudEquipment;

    [SerializeField] private Sprite[] amuletsForLeoraShadow;
    [SerializeField] private Sprite[] ringsForLeoraShadow;
    [SerializeField] private Image[] LeoraShadowEquipment;

    private void Start()
    {
        checkIfWearing(EquipmentManager.amuletSlot, amuletIcons_borders, 1);
        checkIfWearing(EquipmentManager.ringSlot1, ringIcons_borders, 0);
        checkIfWearing(EquipmentManager.ringSlot2, ringIcons_borders, 2);
    }

    //Slot number is for changing the HUD
    public void checkIfWearing(Dictionary<string, bool> wornEquipment, GameObject[] uiImages, int slotNumber)
    {
        int i = 0;
        foreach (var equip in wornEquipment)
        {
            //Debug.Log(equip);
            if (i >= uiImages.Length) break; // Prevents out-of-bounds errors

            uiImages[i].SetActive(equip.Value);
            if (equip.Value)
            {
                if (hudEquipment == null)
                {
                    hudEquipment = FindObjectOfType<HUD_Equipment>();
                }

                switch (slotNumber)
                {
                    case 1:
                        equipmentManager.ApplyAmuletBonus(equip.Key); break;
                    default:
                        Debug.Log("Ring effect applied");
                        equipmentManager.ApplyRingBonus(equip.Key); break;
                }
                hudEquipment.changeHUDOnEquip(equip.Key, slotNumber);
                LeoraShadowEquipment[slotNumber].sprite = hudEquipment.uglyAssSwitchStatement(equip.Key, slotNumber == 1 ? amuletsForLeoraShadow : ringsForLeoraShadow);
                break; // Just stop the function if equip is true
            }
            //uiImages[i].color = equip.Value ? Color.white : tintColor;
            i++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //I don't know how to do the user input stuff so we can do that later

        //NOTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11 COULD MESS UP TIMESCALE JUNK! 

        if (Input.GetKeyDown(KeyCode.Escape) && GLOBALcanOpenPause)
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

    public void ResumeGame()
    {
        if (canOpenPause)
        {
            //Open main pause menu
            //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
            Time.timeScale = Convert.ToInt32(pauseMenuObject.activeInHierarchy);
            pauseMenuObject.SetActive(!pauseMenuObject.activeInHierarchy);
        }
        else
        {
            //Open quick pause
            //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
            Time.timeScale = Convert.ToInt32(quickPauseMenu.activeInHierarchy);
            quickPauseMenu.SetActive(!quickPauseMenu.activeInHierarchy);
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
