using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class startFinalBosses : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Transform[] positions;
    [SerializeField] private Color bgColor;
    [SerializeField] private GameObject nobtn;


    public void openStartFinalBossMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        positions[0].position = positions[2].transform.position;
        Time.timeScale = 0f;
        bg.alpha = 0f;
        bg.DOFade(1, 1).SetUpdate(true);
        positions[0].gameObject.SetActive(true);
        positions[0].DOMove(positions[1].transform.position, 1f).SetUpdate(true).OnComplete(() =>
        {
            EventSystem.current.SetSelectedGameObject(nobtn);
        });
    }

    public void yes()
    {
        //move ui out, play final cutscene, yay
        EventSystem.current.SetSelectedGameObject(null);
        bg.gameObject.GetComponent<Image>().DOColor(bgColor, 1f).SetUpdate(true);

        if (BossSaveData.GetNumberOfCondemned() == 3)
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionCondemn");
        }
        else if (BossSaveData.GetNumberOfSaved() == 3)
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionCompassion");
        }
        else
        {
            GameObject.FindObjectOfType<mainDialogueManager>().dialogueSTART("Endings/transitionConflicted");
        }
        positions[0].DOMove(positions[2].transform.position, 2f).SetUpdate(true).OnComplete(() =>
        {
            positions[0].gameObject.SetActive(false);
        });

    }

    public void no()
    {
        EventSystem.current.SetSelectedGameObject(null);
        bg.DOFade(0, 1).SetUpdate(true);
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            positions[0].gameObject.SetActive(false);
        });
    }
}
