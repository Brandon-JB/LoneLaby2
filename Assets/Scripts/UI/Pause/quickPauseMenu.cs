using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quickPauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void resumeGame()
    {
        this.gameObject.SetActive(false);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
