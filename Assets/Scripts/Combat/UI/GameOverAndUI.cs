using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using DG.Tweening;

public class GameOverAndUI : MonoBehaviour
{

    [SerializeField] private HueShiftManager hsm; // For making the hue change of the bg
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private GameObject gameOverHUD;
    [SerializeField] private CanvasGroup battleUI;
    public GameObject itemMenuHUD;


    [SerializeField] private CanvasGroup gameOverText;
    [SerializeField] private CanvasGroup buttonGraphics;
    [SerializeField] private GameObject buttons;


    private void Start()
    {
        gameplayHUD.SetActive(true);
        gameOverHUD.SetActive(false);
        //itemMenuHUD.SetActive(false);
        //Time.timeScale = 1;
    }

    public void OnDeath()
    {
        //Time.timeScale = 0;
        gameplayHUD.SetActive(false);
        gameOverHUD.SetActive(true);
        

        Time.timeScale = 0;

        gameOverText.DOFade(1, 2).SetUpdate(true).OnComplete(() => {
            buttonGraphics.DOFade(1, 1).SetUpdate(true).OnComplete(() => {
                buttons.SetActive(true);
            });
        });

    }

    //Yes yes I know this just calls another function from another script but the battle character has too much going on and this is easier
    public void StartGameOverHueShift()
    {
        hsm.StartHueTransitionSingle();
    }

    public void Continue()
    {
        Time.timeScale = 1;
        audioManager.Instance.stopBGM(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        audioManager.Instance.stopBGM(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenItemMenu(string itemName, RectTransform uiToMove)
    {
        itemMenuHUD.SetActive(true);

        Time.timeScale = 0;

        ItemMenu itemMenu = itemMenuHUD.GetComponent<ItemMenu>();

        itemMenu.ChangeTextAndSprite(itemName);
    }

    //Fades out All UI while death anim plays
    public void FadeOutUI()
    {
        battleUI.DOFade(0, 2).SetUpdate(true);
    }
}
