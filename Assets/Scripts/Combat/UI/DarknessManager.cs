using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

public class DarknessManager : MonoBehaviour
{
    public CanvasGroup DarknessImage;
    public CanvasGroup DarknessImage2;
    public CanvasGroup DarknessImage3;
    public float ChangeTime = 30f;
    public float timer = 0f;

    private LeoraChar2 leoraChar;

    //private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DarknessImage.enabled = true;
        fadeDarkness1();
        leoraChar = FindObjectOfType<LeoraChar2>();
    }


    //void Update()
    //{
    //    //timer += Time.deltaTime;
    //    //if (timer >= ChangeTime) 
    //    //{
    //    //    timer = 0f;
    //    //    currentIndex++;
    //    //    DarknessImage.sprite = sprites[currentIndex];
    //    //}

    //    //if(InputManager.magicPressed && leoraChar.magicType == "lightMag")
    //    //{
    //    //    //currentIndex = 0;
    //    //    timer = 0f;
    //    //    DarknessImage.DOKill();
    //    //    DarknessImage2.DOKill();
    //    //    DarknessImage3.DOKill();
    //    //    DarknessImage2.DOFade(0, 1f).SetEase(Ease.Linear);
    //    //    DarknessImage3.DOFade(0, 1f).SetEase(Ease.Linear);
    //    //    DarknessImage.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() =>
    //    //    {
    //    //        fadeDarkness1();
    //    //    });
    //    //    //DarknessImage.sprite = sprites[currentIndex];
    //    //}
    //}

    public void turnoffDarkness()
    {
        DarknessImage.DOKill();
        DarknessImage2.DOKill();
        DarknessImage3.DOKill();
        DarknessImage2.DOFade(0, 0.35f).SetEase(Ease.InQuint);
        DarknessImage3.DOFade(0, 0.35f).SetEase(Ease.InQuint);
        DarknessImage.DOFade(0, 0.35f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            fadeDarkness1();
        });
    }

    public void LucanProgressDarkness()
    {
        if (DarknessImage.alpha < 1)
        {
            //Darkness level 1
            DarknessImage.DOKill();
            DarknessImage.DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                fadeDarkness2();
            });
        } else if (DarknessImage2.alpha < 1)
        {
            DarknessImage2.DOKill();
            DarknessImage2.DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                fadeDarkness3();
            });
        } else if (DarknessImage3.alpha < 1)
        {
            DarknessImage3.DOKill();
            DarknessImage3.DOFade(1, 0.5f);
        }
    }

    private void fadeDarkness1()
    {
        DarknessImage.DOFade(1, ChangeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeDarkness2();
        });
    }

    private void fadeDarkness2()
    {
        DarknessImage2.DOFade(1, ChangeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeDarkness3();
        });
    }

    private void fadeDarkness3()
    {
        DarknessImage3.DOFade(1, ChangeTime).SetEase(Ease.Linear);
    }


}
