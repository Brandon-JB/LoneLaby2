using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Dialogue");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
