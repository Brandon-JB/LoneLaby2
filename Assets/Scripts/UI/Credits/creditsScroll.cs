using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class creditsScroll : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeIn;
    [SerializeField] private Transform[] locations;

    [SerializeField] private Transform titletext, scrollingText;
    [SerializeField] private GameObject firsttextToAppear;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        fadeIn.alpha = 1.0f;
        fadeIn.DOFade(0, 3f).OnComplete(() => {
            titletext.DOMove(locations[0].position, 2f).SetEase(Ease.Linear).OnComplete(() => {
                firsttextToAppear.SetActive(true);
                //scrollingText.DOMove(locations[1].position, 36f).SetEase(Ease.Linear);
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickGoToTitle()
    {
        fadeIn.DOFade(1, 3f).OnComplete(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
