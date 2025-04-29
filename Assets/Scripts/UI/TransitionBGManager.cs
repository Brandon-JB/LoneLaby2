using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBGManager : MonoBehaviour
{
    public void StopMusic()
    {
        audioManager.Instance.stopBGM(1);
    }

    public void PlayChurch()
    {
        audioManager.Instance.playBGM("T7");
    }

    public void PlayCity()
    {
        audioManager.Instance.playBGM("T3");
    }
}
