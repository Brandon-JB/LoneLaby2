using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private GameObject gameOverHUD;


    private void Start()
    {
        gameplayHUD.SetActive(true);
        gameOverHUD.SetActive(false);
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
}
