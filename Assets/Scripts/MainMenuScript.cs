using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public mainDialogueManager mainDialogueManager;
    public CanvasGroup bg;

    [SerializeField]private Transform[] optionsLoc;

    public GameObject StartButton;
    public GameObject ContinueButton;
    public GameObject OptionsButton;
    public GameObject BackButton;
    public GameObject noDeleteSave;
    public GameObject deleteSavebtn;
    [SerializeField] public GameObject deleteSaveMenu;

    [SerializeField] private SaveManager saveManager;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1.0f;
        bg.alpha = 1.0f;
        bg.gameObject.SetActive(true);
        bg.DOFade(0, 1f).SetUpdate(true).OnComplete(() => { bg.gameObject.SetActive(false); });
        //audioManager.Instance.playBGM("T1");

        if(SaveManager.Isdata() == true)
        {
            StartButton.SetActive(false);
            ContinueButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ContinueButton);
        }
        else if(SaveManager.Isdata() == false)
        {
            StartButton.SetActive(true);
            ContinueButton.SetActive(false);
            EventSystem.current.SetSelectedGameObject(StartButton);
        }
    }

    public void goToOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        optionsLoc[0].DOMove(optionsLoc[1].position, 1f).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(BackButton);
        });
    }

    public void exitOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        optionsLoc[0].DOMove(optionsLoc[2].position, 1f).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(OptionsButton);
        });
    }

    public void tempTeleportToGame() { Time.timeScale = 1f; SceneManager.LoadScene("NoCombatAreas"); }

    public void GoToGame()
    {
        //AT SOME POINT CHECK IF WE HAVE SAVE DATA!
        EventSystem.current.SetSelectedGameObject(null);
        //If there is save data, go to last saved area. If there is NOT save data, play opening cutscene
        if (SaveManager.Isdata())
        {
            //audioManager.Instance.stopBGM(0.75f);
            ResetStatics();
            bg.gameObject.SetActive(true);
            bg.DOFade(1, 1f).SetUpdate(true).OnComplete(() => {
                if(saveManager == null)
                {
                    saveManager = GameObject.FindObjectOfType<SaveManager>();
                }
                saveManager.LoadGame();
                });

            }
        else
        {
            PortalScript.LastPortal = 1;
            mainDialogueManager.dialogueSTART("openingCutscene");
            bg.gameObject.SetActive(true);
            bg.DOFade(1, 1f).SetUpdate(true);
        }
        
        //SceneManager.LoadScene("Dialogue");
    }

    public void Exit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Application.Quit();
    }
    public void startDeleteSave()
    {
        EventSystem.current.SetSelectedGameObject(null);
        optionsLoc[0].transform.DOMove(optionsLoc[3].position, 1).SetUpdate(true).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(noDeleteSave);
        });
        deleteSaveMenu.SetActive(true);
        Debug.Log("I ahve moved");
    }

    public void exitDeleteSave()
    {
        EventSystem.current.SetSelectedGameObject(null);
        optionsLoc[0].transform.DOMove(optionsLoc[1].position, 1).SetUpdate(true).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(deleteSavebtn);
            deleteSaveMenu.SetActive(false);
        });
    }

    public void DELETESAVE()
    {
        //Delete the save game
        //audioManager.Instance.stopBGM(0.75f);
        EventSystem.current.SetSelectedGameObject(null);
        ResetStatics();
        SaveManager.DeleteSaveData();
        bg.DOFade(1, 1f).SetUpdate(true).SetUpdate(true).OnComplete(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }




    //
    private void ResetStatics()
    {
        MansionDoorManager.hasKey = false;
        MansionDoorManager.DoorOpened = false;
        PortalScript.LastPortal = 1;
        //EDIT DROPMANAGER ENEMY DEATH COUNT
    }
}
