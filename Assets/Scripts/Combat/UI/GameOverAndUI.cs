using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameOverAndUI : MonoBehaviour
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private GameObject gameOverHUD;
    public GameObject itemMenuHUD;


    private void Start()
    {
        gameplayHUD.SetActive(true);
        gameOverHUD.SetActive(false);
        itemMenuHUD.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnDeath()
    {
        Time.timeScale = 0;
        gameplayHUD.SetActive(false);
        gameOverHUD.SetActive(true);
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

    public void OpenItemMenu(string itemName)
    {
        itemMenuHUD.SetActive(true);

        Time.timeScale = 0;

        ItemMenu itemMenu = itemMenuHUD.GetComponent<ItemMenu>();

        itemMenu.ChangeTextAndSprite(itemName);
    }
}
