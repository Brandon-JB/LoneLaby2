using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuScript : MonoBehaviour
{
    public mainDialogueManager mainDialogueManager;
    public CanvasGroup bg;

    [SerializeField]private Transform[] optionsLoc;

    private void Start()
    {
        Time.timeScale = 1.0f;
        bg.alpha = 1.0f;
        bg.DOFade(0, 1f).SetUpdate(true);
        //audioManager.Instance.playBGM("T1");
    }

    public void goToOptions()
    {
        optionsLoc[0].DOMove(optionsLoc[1].position, 1f);
    }

    public void exitOptions()
    {
        optionsLoc[0].DOMove(optionsLoc[2].position, 1f);
    }

    public void tempTeleportToGame() { Time.timeScale = 1f; SceneManager.LoadScene("NoCombatAreas"); }

    public void GoToGame()
    {
        //AT SOME POINT CHECK IF WE HAVE SAVE DATA!

        //If there is save data, go to last saved area. If there is NOT save data, play opening cutscene
        mainDialogueManager.dialogueSTART("openingCutscene");
        bg.DOFade(1, 1f).SetUpdate(true).SetUpdate(true);
        //SceneManager.LoadScene("Dialogue");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
