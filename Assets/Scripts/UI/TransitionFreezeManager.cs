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

    public void UnFreezeTime()
    {
        Time.timeScale = 1;
        OpenPauseMenu.GLOBALcanOpenPause = true;
    }
}
