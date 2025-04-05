using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Xml.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class killSpareManager : MonoBehaviour
{
    [SerializeField] private Animator leoraAnimator;
    [SerializeField] private Transform sideL;
    [SerializeField] private Transform sideR;

    [SerializeField] private CanvasGroup killSpareTextCanvas, yescanvas, nocanvas;


    [SerializeField] private Transform[] locations;


    [SerializeField] private GameObject whoIsGettingCooked;
    [SerializeField] private GameObject[] bossSprites;

    [SerializeField] private TextMeshProUGUI killSpareText;

    [SerializeField] private mainDialogueManager mainDialogueManager;
    [SerializeField] private GameObject menu;

    public string bossName;

    public void EnableKillSpare(string bossName)
    {
        menu.SetActive(true);
        audioManager.Instance.playBGM("T13");
        killSpareTextCanvas.alpha = 0;
        yescanvas.alpha = 0;
        nocanvas.alpha = 0;


        sideL.transform.position = locations[4].position;
        sideL.transform.DOMove(locations[4].position, 1f);
        sideR.transform.DOMove(locations[5].position, 1f);
        leoraAnimator.transform.DOMove(locations[6].position, 0f).SetUpdate(true);
        whoIsGettingCooked.transform.DOMove(locations[7].position, 0f).SetUpdate(true);
        //leoraAnimator.transform.position = locations[6].position;
        //whoIsGettingCooked.transform.position = locations[7].position;

        leoraAnimator.SetTrigger("return");
        leoraAnimator.transform.DOMove(locations[2].position, 1f).SetUpdate(true).SetEase(Ease.InOutQuad);
        whoIsGettingCooked.transform.DOMove(locations[3].position, 1f).SetUpdate(true).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            killSpareTextCanvas.DOFade(1, 1f).SetUpdate(true).OnComplete(() =>
            {
                yescanvas.gameObject.SetActive(true);
                yescanvas.DOFade(1, 0.5f).SetUpdate(true).OnComplete(() => {
                    nocanvas.gameObject.SetActive(true);
                    nocanvas.DOFade(1, 0.5f).SetUpdate(true);
                });
            });
        });

        changeBasedOnBoss();

        ResetLeoraAnimator();
    }

    public void changeBasedOnBoss()
    {
        switch (bossName)
        {
            case "Ivar":
                //Show Ivar's sprite
                //whoIsGettingCooked.sprite = bossSprites[2];
                bossSprites[0].SetActive(false);
                bossSprites[1].SetActive(false);
                bossSprites[2].SetActive(true);
                int viinStatus = BossSaveData.bossStates["Viin"];
                int lucanStatus = BossSaveData.bossStates["Lucan"];

                //How many bosses have been killed?
                if (viinStatus == 1 && lucanStatus == 1)
                {
                    killSpareText.text = "Is it a crime to protect those you love?";
                } else if (viinStatus == 2 && lucanStatus == 2)
                {
                    killSpareText.text = "Am I failing my purpose?";
                } else if (viinStatus == 1 || lucanStatus == 1)
                {
                    killSpareText.text = "Does desperation excuse Ivar's crimes?";
                } else
                {
                    killSpareText.text = "Should " + bossName + " be executed for his crimes?";
                }

                break;
            case "Viin":
                //Show Viin's sprite
                bossSprites[0].SetActive(true);
                bossSprites[1].SetActive(false);
                bossSprites[2].SetActive(false);
                int ivarStatus = BossSaveData.bossStates["Ivar"];
                int lucanStatus2 = BossSaveData.bossStates["Lucan"];

                //How many bosses have been killed?
                if (ivarStatus == 1 && lucanStatus2 == 1)
                {
                    killSpareText.text = "Would mercy be unjust?"; //TODO COME BACK AND CHANGE THIS LATER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
                }
                else if (ivarStatus == 2 && lucanStatus2 == 2)
                {
                    killSpareText.text = "Am I failing my purpose?";
                }
                else if (ivarStatus == 1 || lucanStatus2 == 1)
                {
                    killSpareText.text = "Is it justice if an innocent must die with Viin?";
                }
                else
                {
                    killSpareText.text = "Should " + bossName + " be executed for her crimes?";
                }

                break;
            case "Lucan":
                //Show Viin's sprite
                bossSprites[0].SetActive(false);
                bossSprites[1].SetActive(true);
                bossSprites[2].SetActive(false);
                int ivarStatus2 = BossSaveData.bossStates["Ivar"];
                int viinStatus2 = BossSaveData.bossStates["Viin"];

                //How many bosses have been killed?
                if (ivarStatus2 == 1 && viinStatus2 == 1)
                {
                    killSpareText.text = "Is loyalty to the Order the same as justice?";
                }
                else if (ivarStatus2 == 2 && viinStatus2 == 2)
                {
                    killSpareText.text = "Am I failing my purpose?";
                }
                else if (ivarStatus2 == 1 || viinStatus2 == 1)
                {
                    killSpareText.text = "Does betraying the Order make Lucan unredeemable?";
                }
                else
                {
                    killSpareText.text = "Should " + bossName + " be executed for his crimes?";
                }

                break;
        }
    }

    public void killBoss()
    {
        switch (bossName)
        {
            case "Ivar":
                BossSaveData.bossStates[bossName] = 1;
                break;

            case "Lucan":
                BossSaveData.bossStates[bossName] = 1;
                break;

            case "Viin":
                BossSaveData.bossStates[bossName] = 1;
                break;
        }

        foreach (var boss in BossSaveData.bossStates)
        {
            Debug.Log(boss);
        }

        leoraAnimator.SetTrigger("kill");
        //wait 0.5 seconds
        StartCoroutine(endAfterTimePeriod(0.75f, true));
        
    }

    private IEnumerator endAfterTimePeriod(float time, bool isKilling = false)
    {
        yield return new WaitForSecondsRealtime(time);

        mainDialogueManager = GameObject.FindObjectOfType<mainDialogueManager>();

        sideL.DOMove(locations[0].position, 0.75f).SetUpdate(true).SetEase(Ease.InOutBack);
        sideR.DOMove(locations[1].position, 0.75f).SetUpdate(true).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(1.5f);

        if(isKilling)
        {
            switch (bossName)
            {
                case "Ivar":
                    mainDialogueManager.dialogueSTART("IvarQuest/manor_postfight_condemnIvar");
                    break;

                case "Lucan":
                    mainDialogueManager.dialogueSTART("LucanQuest/cave_postfight_condemnLucan");
                    break;

                case "Viin":
                    mainDialogueManager.dialogueSTART("ViinQuest/veinwood_postfight_condemnViin");
                    break;
            }
        } else
        {
            audioManager.Instance.playBGM("T12");
            switch (bossName)
            {
                case "Ivar":
                    mainDialogueManager.dialogueSTART("IvarQuest/manor_postfight_saveIvar");
                    break;

                case "Lucan":
                    mainDialogueManager.dialogueSTART("LucanQuest/cave_postfight_saveLucan");
                    break;

                case "Viin":
                    mainDialogueManager.dialogueSTART("ViinQuest/veinwood_postfight_saveViin");
                    break;
            }
        }
        StopAllCoroutines();
    }


    public void spareBoss()
    {
        switch (bossName)
        {
            case "Ivar":
                BossSaveData.bossStates[bossName] = 2;
                break;

            case "Lucan":
                BossSaveData.bossStates[bossName] = 2;
                break;

            case "Viin":
                BossSaveData.bossStates[bossName] = 2;
                break;
        }

        leoraAnimator.SetTrigger("spare");
        StartCoroutine(endAfterTimePeriod(0.25f));
    }

    private void ResetLeoraAnimator()
    {
        //This just undoes all triggers just in case
        leoraAnimator.ResetTrigger("spare");
        leoraAnimator.ResetTrigger("kill");
        leoraAnimator.ResetTrigger("return");
    }
}




//OLD




//private void OnEnable()
//{
//    //check who is on the chopping block. set that to be their image\
//    //format:
//    //whoIsGettingCooked.sprite = bossSprites[0];




//    //move everything to original locations

//    //audioManager.Instance.playBGM("T13");
//    killSpareTextCanvas.alpha = 0;
//    yescanvas.alpha = 0;
//    nocanvas.alpha = 0;


//    sideL.transform.position = locations[4].position;
//    sideL.transform.DOMove(locations[4].position, 1f);
//    sideR.transform.DOMove(locations[5].position, 1f);
//    leoraAnimator.transform.DOMove(locations[6].position, 0f).SetUpdate(true);
//    whoIsGettingCooked.transform.DOMove(locations[7].position, 0f).SetUpdate(true);
//    //leoraAnimator.transform.position = locations[6].position;
//    //whoIsGettingCooked.transform.position = locations[7].position;

//    leoraAnimator.SetTrigger("return");
//    leoraAnimator.transform.DOMove(locations[2].position, 1f).SetUpdate(true).SetEase(Ease.InOutQuad);
//    whoIsGettingCooked.transform.DOMove(locations[3].position, 1f).SetUpdate(true).SetEase(Ease.InOutQuad).OnComplete(() =>
//    {
//        killSpareTextCanvas.DOFade(1,1f).SetUpdate(true).OnComplete(() =>
//        {
//            yescanvas.gameObject.SetActive(true);
//            yescanvas.DOFade(1,0.5f).SetUpdate(true).OnComplete(() => { 
//                nocanvas.gameObject.SetActive(true);
//                nocanvas.DOFade(1,0.5f).SetUpdate(true); });
//        });
//    });

//    changeBasedOnBoss();

//    //Change text based on who is killed and spared:
//    //killSpareText.text = "Should " + bossName + " be executed for their crimes?";


//    //BOSS SPECIFIC
//    //LUCAN NONE DEAD: Should Lucan be executed for his crimes?
//    //LUCAN SOME DEAD: Does betraying the Order make Lucan unredeemable?
//    //LUCAN MANY DEAD: Is loyalty to the Order the same as justice?

//    //IVAR NONE DEAD: Should Ivar be executed for his crimes?
//    //IVAR SOME DEAD: Does desperation excuse Ivar's crimes?
//    //IVAR MANY DEAD: Is it a crime to protect those you love?

//    //VIIN NONE DEAD: Should Viin be executed for her crimes?
//    //VIIN SOME DEAD: Is it justice if an innocent must die with Viin?
//    //VIIN MANY DEAD: Does justice care for mercy? 

//    //For post igs/if we have time during the Leora boss fight
//    //DARK LEORA 75%: Did their crimes warrant death?
//    //DARK LEORA 50%: Did they deserve to walk free?
//    //DARK LEORA 25%: Are my hands clean?
//    //DARK LEORA DEFEATED: Was any of this just?

//    //If two are spared: Am I failing my purpose?

//    ResetLeoraAnimator();
//}