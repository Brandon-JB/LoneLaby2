using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startFinalBosses : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Transform[] positions;
    [SerializeField] private Color bgColor;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        bg.alpha = 0f;
        bg.DOFade(1, 1).SetUpdate(true);
        positions[0].DOMove(positions[1].transform.position, 1f).SetUpdate(true);
    }

    public void yes()
    {
        //move ui out, play final cutscene, yay
        bg.gameObject.GetComponent<Image>().DOColor(bgColor, 1f);
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            this.gameObject.SetActive(false);
        });
        if (BossSaveData.GetNumberOfCondemned() == 3)
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionCondemn");
        } else if (BossSaveData.GetNumberOfSaved() == 3)
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionCompassion");
        } else
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionConflicted");
        }

    }

    public void no()
    {
        bg.DOFade(0, 1).SetUpdate(true);
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            this.gameObject.SetActive(false);
        });
    }
}
