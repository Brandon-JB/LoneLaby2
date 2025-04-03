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

    [SerializeField] private CanvasGroup leoraFade;
    [SerializeField] private CanvasGroup bossSpriteFade;


    [SerializeField] private Transform[] locations;


    [SerializeField] private Image whoIsGettingCooked;
    [SerializeField] private Sprite[] bossSprites;

    [SerializeField] private TextMeshProUGUI killSpareText;

    public string bossName;

    private void OnEnable()
    {
        //check who is on the chopping block. set that to be their image\
        //format:
        //whoIsGettingCooked.sprite = bossSprites[0];


        //move everything to original locations

        sideL.transform.position = locations[4].position;
        sideR.transform.position = locations[5].position;
        leoraAnimator.transform.DOMove(locations[6].position, 0f);
        whoIsGettingCooked.transform.DOMove(locations[7].position, 0f);
        //leoraAnimator.transform.position = locations[6].position;
        //whoIsGettingCooked.transform.position = locations[7].position;

        leoraAnimator.SetTrigger("return");
        leoraAnimator.transform.DOMove(locations[2].position, 2f);
        whoIsGettingCooked.transform.DOMove(locations[3].position, 2f);

        //Change text based on who is killed and spared:
        //killSpareText.text = "Should " + bossName + " be executed for their crimes?";


        //BOSS SPECIFIC
        //LUCAN NONE DEAD: Should Lucan be executed for his crimes?
        //LUCAN SOME DEAD: Does betraying the Order make Lucan unredeemable?
        //LUCAN MANY DEAD: Is loyalty to the Order the same as justice?

        //IVAR NONE DEAD: Should Ivar be executed for his crimes?
        //IVAR SOME DEAD: Does desperation excuse Ivar's crimes?
        //IVAR MANY DEAD: Is it a crime to protect those you love?

        //VIIN NONE DEAD: Should Viin be executed for her crimes?
        //VIIN SOME DEAD: Is it justice if an innocent must die with Viin?
        //VIIN MANY DEAD: Does justice care for mercy?

        //For post igs/if we have time during the Leora boss fight
        //DARK LEORA 75%: Did their crimes warrant death?
        //DARK LEORA 50%: Did they deserve to walk free?
        //DARK LEORA 25%: Are my hands clean?
        //DARK LEORA DEFEATED: Was any of this just?

        //If two are spared: Am I failing my purpose?
        //Alternatives: Are the Order's commands always just?


        ResetLeoraAnimator();
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
        StartCoroutine(endAfterTimePeriod(0.5f));
        
    }

    private IEnumerator endAfterTimePeriod(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        sideL.DOMove(locations[0].position, 1f);
        sideR.DOMove(locations[1].position, 1f);

        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene("Overworld");
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
        StartCoroutine(endAfterTimePeriod(0));
    }

    private void ResetLeoraAnimator()
    {
        //This just undoes all triggers just in case
        leoraAnimator.ResetTrigger("spare");
        leoraAnimator.ResetTrigger("kill");
        leoraAnimator.ResetTrigger("return");
    }
}
