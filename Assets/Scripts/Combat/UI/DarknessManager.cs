using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DarknessManager : MonoBehaviour
{
    public CanvasGroup DarknessImage;
    public float ChangeTime = 30f;
    public float timer = 0f;

    //private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DarknessImage.enabled = true;
        DarknessImage.DOFade(1, 30f);
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

        if(InputManager.magicPressed)
        {
            //currentIndex = 0;
            timer = 0f;
            DarknessImage.DOKill();
            DarknessImage.DOFade(0, 1f).OnComplete(() =>
            {
                DarknessImage.DOFade(1, 30f);
            });
            //DarknessImage.sprite = sprites[currentIndex];
        }
    }


}
