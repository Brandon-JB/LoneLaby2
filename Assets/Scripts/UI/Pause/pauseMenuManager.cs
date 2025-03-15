using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
        equipMenu.position = startLocations[0].position;
        questMenu.position = startLocations[1].position;
        leoraAnimator.transform.position = startLocations[2].position;
        leoraAnimator.SetTrigger("enterPause");
        ResetLeoraAnimator();

        //Play Leora on enable anim
        background.DOFade(1, 1);
        mainButtons.alpha = 0;
        mainButtons.gameObject.SetActive(true);
        mainButtons.DOFade(1, 1);
    }

    public void goToQuests()
    {
        questMenu.DOMove(questsLocations[0].position, 1f);
        leoraAnimator.transform.DOMove(questsLocations[1].position, 1f);
        equipMenu.DOMove(questsLocations[2].position, 1f);
        questsTXT.DOFade(1, 1);

        mainButtons.DOFade(0, 0.5f).OnComplete(() => { mainButtons.gameObject.SetActive(false); });

        leoraAnimator.SetTrigger("enterQuest");
    }
    public void goToEquip()
    {
        equipMenu.DOMove(equipLocations[0].position, 1f);
        leoraAnimator.transform.DOMove(equipLocations[1].position, 1f);
        questMenu.DOMove(equipLocations[2].position, 1f);
        equipTXT.DOFade(1, 1);

        mainButtons.DOFade(0, 0.5f).OnComplete(() => { mainButtons.gameObject.SetActive(false); });

        leoraAnimator.SetTrigger("enterEquip");
    }

    public void returnToCenter()
    {
        //Reset ALL spaces
        equipMenu.DOMove(startLocations[0].position, 1f);
        questMenu.DOMove(startLocations[1].position, 1f);
        leoraAnimator.transform.DOMove(startLocations[2].position, 1f);

        mainButtons.alpha = 0;
        mainButtons.gameObject.SetActive(true);
        mainButtons.DOFade(1, 1);

        equipTXT.DOFade(0, 1);
        questsTXT.DOFade(0, 1);
        leoraAnimator.SetTrigger("enterPause");
        ResetLeoraAnimator();
    }

    public void seeSideQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f);
    }

    public void backToMainQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f);
    }


    private void ResetLeoraAnimator()
    {
        //This just undoes all triggers just in case
        leoraAnimator.ResetTrigger("enterPause");
        leoraAnimator.ResetTrigger("enterEquip");
        leoraAnimator.ResetTrigger("enterQuest");
        leoraAnimator.ResetTrigger("exitQuest");
        leoraAnimator.ResetTrigger("exitEquip");
    }



    //Put Equipment UI Stuff here
}
