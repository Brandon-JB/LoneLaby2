using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class flashWhenTextDone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CanvasGroup fadeContinue;
    [SerializeField] private TextMeshProUGUI buttonToPress;


    private void OnEnable()
    {
        fadeContinue.DOComplete();
        fadeContinue.alpha = 0;
        buttonToPress.text = audioStatics.interractButton.ToUpper();
        fadeContinue.DOFade(1, 2).SetUpdate(true).OnComplete(() =>
        {
            continueFlashing();
        });
    }

    private void continueFlashing()
    {
        fadeContinue.DOFade(0.25f, 2).SetUpdate(true).OnComplete(() =>
        {
            fadeContinue.DOFade(1, 2).SetUpdate(true).OnComplete(() =>
            {
                if (gameObject.activeSelf)
                {
                    continueFlashing();
                }
            });
        });

    }
}
