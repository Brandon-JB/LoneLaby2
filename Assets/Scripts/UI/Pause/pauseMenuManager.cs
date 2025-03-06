using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject leoraGameObj;
    [SerializeField] private Transform equipMenu;
    [SerializeField] private Transform questMenu;

    [SerializeField] private CanvasGroup mainButtons;


    [SerializeField] private Transform[] locations;


    private void OnEnable()
    {
        //Play Leora on enable anim
    }
}
