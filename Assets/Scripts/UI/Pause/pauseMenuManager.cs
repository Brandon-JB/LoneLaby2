using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class pauseMenuManager : MonoBehaviour
{
    [SerializeField] private Animator leoraAnimator;
    [SerializeField] private Transform equipMenu;
    [SerializeField] private Transform questMenu;

    [SerializeField] private CanvasGroup mainButtons;
    [SerializeField] private CanvasGroup questsTXT;
    [SerializeField] private CanvasGroup equipTXT;
    [SerializeField] private CanvasGroup background;


    [SerializeField] private Transform[] startLocations;
    [SerializeField] private Transform[] equipLocations;
    [SerializeField] private Transform[] questsLocations;


    private void OnEnable()
    {
        //Commented out to spare us for now
        ////Reset EVERYTHING
        //equipMenu.position = startLocations[0].position;
        //questMenu.position = startLocations[1].position;
        //leoraAnimator.transform.position = startLocations[2].position;

        ////Play Leora on enable anim
        //background.DOFade(1, 1);
    }

    public void goToQuests()
    {
        questMenu.DOMove(questsLocations[0].position, 1f);
        leoraAnimator.transform.DOMove(questsLocations[1].position, 1f);
        questsTXT.DOFade(1, 1);
    }
    public void goToEquip()
    {
        equipMenu.DOMove(equipLocations[0].position, 1f);
        leoraAnimator.transform.DOMove(equipLocations[1].position, 1f);
        equipTXT.DOFade(1, 1);
    }

    public void returnToCenter()
    {
        //Reset ALL spaces
        equipMenu.DOMove(startLocations[0].position, 1f);
        questMenu.DOMove(startLocations[1].position, 1f);
        leoraAnimator.transform.DOMove(startLocations[2].position, 1f);

        equipTXT.DOFade(0, 1);
        questsTXT.DOFade(0, 1);
    }

    public void seeSideQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f);
    }

    public void backToMainQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f);
    }


    //Put Equipment UI Stuff here
}
