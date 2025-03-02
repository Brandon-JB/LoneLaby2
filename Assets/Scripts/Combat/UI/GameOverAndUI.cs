using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using DG.Tweening;

public class GameOverAndUI : MonoBehaviour
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private GameObject gameOverHUD;
    [SerializeField] private CanvasGroup battleUI;
    public GameObject itemMenuHUD;


    [SerializeField] private CanvasGroup gameOverText;
    [SerializeField] private CanvasGroup buttonGraphics;
    [SerializeField] private GameObject buttons;

    // Item Obtained Menu
    [SerializeField] private Transform itemTransform;
    [SerializeField] private RectTransform itemOnScreen;


    private void Start()
    {
        gameplayHUD.SetActive(true);
        gameOverHUD.SetActive(false);
        itemMenuHUD.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnDeath()
    {
        //Time.timeScale = 0;
        gameplayHUD.SetActive(false);
        gameOverHUD.SetActive(true);

        gameOverText.DOFade(1, 2).OnComplete(() => {
            buttonGraphics.DOFade(1, 1).OnComplete(() => {
                buttons.SetActive(true);
            });
        });

    }

    public void Continue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CombatMaps");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenItemMenu(string itemName, RectTransform uiToMove)
    {
        //Delete the old one
        if (itemOnScreen != null)
        {
            Destroy(itemOnScreen.gameObject);
        }

        //Assign the new one
        itemOnScreen = uiToMove;

        //Make it a child to this game object
        itemOnScreen.SetParent(itemTransform);

        itemMenuHUD.SetActive(true);

        itemOnScreen.DOMove(itemTransform.position, 1);

        //Time.timeScale = 0;

        ItemMenu itemMenu = itemMenuHUD.GetComponent<ItemMenu>();

        itemMenu.ChangeTextAndSprite(itemName);
    }

    //Fades out All UI while death anim plays
    public void FadeOutUI()
    {
        battleUI.DOFade(0, 2);
    }
}
