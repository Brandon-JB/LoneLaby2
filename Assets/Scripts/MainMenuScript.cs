using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuScript : MonoBehaviour
{
    public mainDialogueManager mainDialogueManager;
    public CanvasGroup bg;

    private void Start()
    {
        bg.alpha = 1.0f;
        bg.DOFade(0, 1f);
    }

    public void GoToGame()
    {
        //AT SOME POINT CHECK IF WE HAVE SAVE DATA!

        //If there is save data, go to last saved area. If there is NOT save data, play opening cutscene

        mainDialogueManager.dialogueSTART("openingCutscene");
        bg.DOFade(1, 1f);
        //SceneManager.LoadScene("Dialogue");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
