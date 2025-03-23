using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        DarknessImage.DOFade(1, 30f);
        leoraChar = FindObjectOfType<LeoraChar2>();
    }


    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= ChangeTime) 
        //{
        //    timer = 0f;
        //    currentIndex++;
        //    DarknessImage.sprite = sprites[currentIndex];
        //}

        if(InputManager.magicPressed && leoraChar.magicType == "lightMag")
        {
            //currentIndex = 0;
            timer = 0f;
            DarknessImage.DOKill();
            DarknessImage2.DOKill();
            DarknessImage3.DOKill();
            DarknessImage2.DOFade(0, 1f).SetEase(Ease.Linear);
            DarknessImage3.DOFade(0, 1f).SetEase(Ease.Linear);
            DarknessImage.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                fadeDarkness1();
            });
            //DarknessImage.sprite = sprites[currentIndex];
        }
    }

    private void fadeDarkness1()
    {
        DarknessImage.DOFade(1, 30f).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeDarkness2();
        });
    }

    private void fadeDarkness2()
    {
        DarknessImage2.DOFade(1, 30f).SetEase(Ease.Linear).OnComplete(() =>
        {
            DarknessImage.DOFade(1, 30f);
        });
    }

    private void fadeDarkness3()
    {
        DarknessImage3.DOFade(1, 30f).SetEase(Ease.Linear);
    }


}
