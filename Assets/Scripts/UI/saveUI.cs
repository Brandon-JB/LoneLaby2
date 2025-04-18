using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class saveUI : MonoBehaviour
{

    [SerializeField] private Color originalColor;
    [SerializeField] private Color highlightColor;

    // first 2 = yes lasst 2 = no
    [SerializeField] private Image[] imagesToHighlight;

    public static bool isSaveOpen;
    [SerializeField] private CanvasGroup leoraforFade;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform leoraToMove;

    [SerializeField] private Animator leoraAnimator;
    [SerializeField] private Animator[] buttonAnimators;

    [SerializeField] private GameObject[] buttonsToDisable;

    [SerializeField] private GameObject menu;

    [SerializeField] private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        isSaveOpen = false;
        leoraforFade.alpha = 0f;
        background.alpha = 0f;
        //leoraToMove.DOMove(locations[1].position, 0.5f).SetUpdate(true).OnComplete(() => {
        //    OPENSAVEMENU();
        //});
    }


    public void OPENSAVEMENU()
    {
        menu.SetActive(true);
        isSaveOpen = true;
        //
        Time.timeScale = 0f;
        leoraforFade.alpha = 0f;
        background.DOFade(1, 0.5f).SetUpdate(true);
        leoraforFade.DOFade(1, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            buttonsToDisable[0].SetActive(true);
            buttonsToDisable[1].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttonsToDisable[0]);
        });
        leoraToMove.DOMove(locations[0].position, 0.25f).SetUpdate(true).SetEase(Ease.InCubic);
    }


    public void CLOSEPAUSEMENU()
    {
        EventSystem.current.SetSelectedGameObject(null);
        buttonsToDisable[0].SetActive(false);
        buttonsToDisable[1].SetActive(false);
        isSaveOpen = false;
        leoraforFade.DOFade(0, 0.5f).SetUpdate(true);
        background.DOFade(0, 0.5f).SetUpdate(true);
        leoraToMove.DOMove(locations[1].position, 0.25f).SetUpdate(true).SetEase(Ease.OutCubic).OnComplete(() => {
            buttonAnimators[0].SetFloat("speed", 1f);
            buttonAnimators[1].SetFloat("speed", 1f);
            buttonAnimators[2].SetFloat("speed", 1f);
            menu.SetActive(false);
            Time.timeScale = 1f;
        });
    }

    public void confirmSave()
    {
        DOTween.To(() => buttonAnimators[0].GetFloat("speed"), x => buttonAnimators[0].SetFloat("speed", x), 7f, 0.5f).SetUpdate(true);
        DOTween.To(() => buttonAnimators[1].GetFloat("speed"), x => buttonAnimators[1].SetFloat("speed", x), 7f, 0.5f).SetUpdate(true);
        DOTween.To(() => buttonAnimators[2].GetFloat("speed"), x => buttonAnimators[2].SetFloat("speed", x), 7f, 0.5f).SetUpdate(true);

        buttonsToDisable[0].SetActive(false);
        buttonsToDisable[1].SetActive(false);
        leoraAnimator.SetTrigger("saved");

        leoraToMove.DOMove(locations[0].position, 1f).SetUpdate(true).OnComplete(() => {
            CLOSEPAUSEMENU();
        });
        //super animate the buttons

        saveManager.SaveGame();
    }


    public void HighlightText(bool isyes)
    {
        if (isyes)
        {
            //We're looking at the yes button
            DOTween.Kill(imagesToHighlight[0]);
            DOTween.Kill(imagesToHighlight[1]);
            imagesToHighlight[1].enabled = true;
            imagesToHighlight[0].DOColor(highlightColor, 0.5f).SetUpdate(true);
            imagesToHighlight[1].DOColor(highlightColor, 0.5f).SetUpdate(true);
        } else
        {
            DOTween.Kill(imagesToHighlight[2]);
            DOTween.Kill(imagesToHighlight[3]);
            imagesToHighlight[3].enabled = true;
            imagesToHighlight[2].DOColor(highlightColor, 0.5f).SetUpdate(true);
            imagesToHighlight[3].DOColor(highlightColor, 0.5f).SetUpdate(true);
            //We're looking at the no button
        }
    }

    public void UnHighlightText(bool isyes)
    {
        DOTween.KillAll();
        if (isyes)
        {
            //We're looking at the yes button
            DOTween.Kill(imagesToHighlight[0]);
            DOTween.Kill(imagesToHighlight[1]);
            imagesToHighlight[1].enabled = false;
            imagesToHighlight[0].DOColor(originalColor, 0.2f).SetUpdate(true);
            imagesToHighlight[1].DOColor(originalColor, 0.2f).SetUpdate(true);
        }
        else
        {
            //We're looking at the no button
            DOTween.Kill(imagesToHighlight[2]);
            DOTween.Kill(imagesToHighlight[3]);
            imagesToHighlight[3].enabled = false;
            imagesToHighlight[2].DOColor(originalColor, 0.2f).SetUpdate(true);
            imagesToHighlight[3].DOColor(originalColor, 0.2f).SetUpdate(true);
        }
    }
}
