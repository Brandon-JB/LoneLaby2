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
    [SerializeField] private CanvasGroup optionsTXT;
    [SerializeField] private CanvasGroup background;

    [SerializeField] private CanvasGroup equipBack;
    [SerializeField] private CanvasGroup questBack;

    [SerializeField] private CanvasGroup leoraAnimatorEquipment;

    [SerializeField] private Transform[] startLocations;
    [SerializeField] private Transform[] equipLocations;
    [SerializeField] private Transform[] questsLocations;
    [SerializeField] private GameObject[] objectsToTurnOff;


    private void OnEnable()
    {
        //Commented out to spare us for now
        ////Reset EVERYTHING
        equipMenu.position = startLocations[0].position;
        questMenu.position = startLocations[1].position;
        leoraAnimator.transform.position = startLocations[2].position;
        this.transform.position = startLocations[3].position;
        leoraAnimator.SetTrigger("enterPause");
        ResetLeoraAnimator();

        //Play Leora on enable anim
        background.DOFade(1, 1).SetUpdate(true);
        mainButtons.alpha = 0;
        mainButtons.gameObject.SetActive(true);
        mainButtons.DOFade(1, 1).SetUpdate(true);

        equipBack.gameObject.SetActive(false);
        questBack.gameObject.SetActive(false);

        questsTXT.alpha = 0;
        equipTXT.alpha = 0;
        optionsTXT.alpha = 0;
        leoraAnimatorEquipment.alpha = 0;

        //Make sure everything that should be off is off
        foreach (GameObject obj in objectsToTurnOff)
        {
            obj.SetActive(false);
        }
    }

    public void goToQuests()
    {
        questMenu.DOMove(questsLocations[0].position, 1f).SetUpdate(true);
        leoraAnimator.transform.DOMove(questsLocations[1].position, 1f).SetUpdate(true);
        equipMenu.DOMove(questsLocations[2].position, 1f).SetUpdate(true);
        questsTXT.DOFade(1, 1).SetUpdate(true);

        mainButtons.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { 
            mainButtons.gameObject.SetActive(false);
            questBack.alpha = 0f;
            questBack.gameObject.SetActive(true);
            questBack.DOFade(1, 1f).SetUpdate(true);
        });

        leoraAnimator.SetTrigger("enterQuest");
    }
    public void goToEquip()
    {
        equipMenu.DOMove(equipLocations[0].position, 1f).SetUpdate(true);
        leoraAnimator.transform.DOMove(equipLocations[1].position, 1f).SetUpdate(true);
        questMenu.DOMove(equipLocations[2].position, 1f).SetUpdate(true);
        equipTXT.DOFade(1, 1).SetUpdate(true);

        mainButtons.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { 
            mainButtons.gameObject.SetActive(false);
            equipBack.alpha = 0f;
            equipBack.gameObject.SetActive(true);
            equipBack.DOFade(1, 1f).SetUpdate(true);
            leoraAnimatorEquipment.DOFade(1, 0.25f).SetUpdate(true);
        });

        leoraAnimator.SetTrigger("enterEquip");
    }


    /// <summary>
    /// NOTE!! LATER CHANGE THIS DEPENDING ON WHERE THEY LAST WERE!!!
    /// </summary>
    public void returnToCenter()
    {
        //Reset ALL spaces
        equipMenu.DOMove(startLocations[0].position, 1f).SetUpdate(true);
        questMenu.DOMove(startLocations[1].position, 1f).SetUpdate(true);
        leoraAnimator.transform.DOMove(startLocations[2].position, 1f).SetUpdate(true);

        mainButtons.alpha = 0;
        mainButtons.gameObject.SetActive(true);
        mainButtons.DOFade(1, 1).SetUpdate(true);
        leoraAnimatorEquipment.DOFade(0, 0.5f).SetUpdate(true);

        equipTXT.DOFade(0, 1).SetUpdate(true);
        questsTXT.DOFade(0, 1).SetUpdate(true);
        ResetLeoraAnimator();

        if (equipBack.gameObject.activeInHierarchy) // We're exiting the 
        {
            leoraAnimator.SetTrigger("exitEquip");
        } else
        {
            leoraAnimator.SetTrigger("exitQuest");
        }

        equipBack.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => {
            equipBack.gameObject.SetActive(false);
        });

        questBack.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => {
            questBack.gameObject.SetActive(false);
        });
    }

    public void seeSideQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f).SetUpdate(true);
    }

    public void backToMainQuests()
    {
        questMenu.DOMove(questsLocations[2].position, 1f).SetUpdate(true);
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

    public void goToOptions()
    {
        //move the entiiiirrreeee UI
        OpenPauseMenu.GLOBALcanOpenPause = false;
        this.transform.DOMove(startLocations[4].position,1).SetUpdate(true);
        optionsTXT.DOFade(1, 1).SetUpdate(true);
        mainButtons.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => {
            mainButtons.gameObject.SetActive(false);
            equipBack.alpha = 0f;
        });
    }

    public void exitOptions()
    {
        //move the entiiiirrreeee UI
        this.transform.DOMove(startLocations[3].position, 1).SetUpdate(true);
        optionsTXT.DOFade(0, 1).SetUpdate(true);
        mainButtons.alpha = 0;
        mainButtons.gameObject.SetActive(true);
        mainButtons.DOFade(1, 1).SetUpdate(true);
    }
}
