using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFreezeManager : MonoBehaviour
{
    
    public void FreezeTime()
    {
        Time.timeScale = 0;
        OpenPauseMenu.GLOBALcanOpenPause = false;
    }

    public void freezePausing()
    {
        OpenPauseMenu.GLOBALcanOpenPause = false;
    }

    public void unfreezePausing()
    {
        OpenPauseMenu.GLOBALcanOpenPause = true;
    }

    public void UnFreezeTime()
    {
        if (FindObjectOfType<startTutorial>()) // If it can find the tutorial menu, then that means that the tutorial is open. Don't do anything.
        {
            Debug.Log("Tutorial menu open");
            FreezeTime();
            return;
        }
        Time.timeScale = 1;
        OpenPauseMenu.GLOBALcanOpenPause = true;
    }
}
