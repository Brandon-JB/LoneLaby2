using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OpenPauseMenu : MonoBehaviour
{
    public InputActionAsset input;
    public InputAction openPauseKey;
    public static bool canOpenPause = true;
    public static bool pauseOpened = false;

    public static bool GLOBALcanOpenPause = false;

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

    [SerializeField] private GameObject quickPauseResumeBtn, pauseResumeBtn;

    private void Start()
    {
        openPauseKey = input.FindAction("Open Pause");
        if (openPauseKey != null)
        {
            Debug.Log(openPauseKey.ToString());
        } else
        {
            Debug.Log("I hate everything");
        }
        //Freeze the game
        StartCoroutine(wait());
    }
    private IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        hudEquipment = FindObjectOfType<HUD_Equipment>();

        //Wait for game to load
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
                    case 0:
                        Debug.Log("Ring effect applied for slot 1");
                        equipmentManager.ApplyRingBonus(equip.Key); break;
                    case 2:
                        Debug.Log("Ring effect applied for slot 2");
                        equipmentManager.ApplyRingBonus(equip.Key); break;
                }
                hudEquipment.changeHUDOnEquip(equip.Key, slotNumber);
                LeoraShadowEquipment[slotNumber].sprite = hudEquipment.uglyAssSwitchStatement(equip.Key, slotNumber == 1 ? amuletsForLeoraShadow : ringsForLeoraShadow);
                //uiImages[i].SetActive(true);
                //Debug.Log("This thing should like. 100% be showing rn. TO PROVE IT TO YOU, HERE IS THE NAME OF THE UI" + uiImages[i].name);
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

        if (openPauseKey.triggered && GLOBALcanOpenPause && SceneManager.GetActiveScene().name != "MainMenu")
        {
            Debug.Log("OPENED");
            if(canOpenPause)
            {
                //Open main pause menu
                //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
                Time.timeScale = Convert.ToInt32(pauseMenuObject.activeInHierarchy);
                pauseMenuObject.SetActive(!pauseMenuObject.activeInHierarchy);
                if (pauseMenuObject.activeInHierarchy)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    //resume btn
                    EventSystem.current.SetSelectedGameObject(pauseResumeBtn);
                    audioManager.Instance.playSFX(38);
                    pauseOpened = true;
                } else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    audioManager.Instance.playSFX(39);
                    pauseOpened = false;
                    if (GameObject.FindObjectOfType<DarknessManager>())
                    {
                        GameObject.FindObjectOfType<DarknessManager>().pauseProgressDarkness();
                    }
                }
            } else
            {
                //Open quick pause
                //Hypothetically, will make time scale 0 if pause menu is closing and 1 if pause menu is opening
                Debug.Log(Convert.ToInt32(quickPauseMenu.activeInHierarchy && SceneManager.GetActiveScene().name == "Overworld"));
                Time.timeScale = Convert.ToInt32(quickPauseMenu.activeInHierarchy && SceneManager.GetActiveScene().name == "Overworld");
                quickPauseMenu.SetActive(!quickPauseMenu.activeInHierarchy);
                if(quickPauseMenu.activeInHierarchy)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    //quick pause resume btn
                    EventSystem.current.SetSelectedGameObject(quickPauseResumeBtn);
                    audioManager.Instance.playSFX(38);
                    pauseOpened = true;
                } else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    audioManager.Instance.playSFX(39);
                    pauseOpened = false;
                    if (GameObject.FindObjectOfType<DarknessManager>())
                    {
                        GameObject.FindObjectOfType<DarknessManager>().pauseProgressDarkness();
                    }
                }
            }
        }
    }

    public void ResumeGame()
    {
        pauseOpened = false;
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
            //Debug.Log(Convert.ToInt32(quickPauseMenu.activeInHierarchy && SceneManager.GetActiveScene().name == "Overworld"));
            Time.timeScale = Convert.ToInt32(quickPauseMenu.activeInHierarchy && SceneManager.GetActiveScene().name == "Overworld");
            quickPauseMenu.SetActive(!quickPauseMenu.activeInHierarchy);
        }
    }

    public void mainMenu()
    {
        pauseOpened = false;
        SceneManager.LoadScene("MainMenu");
    }
}
