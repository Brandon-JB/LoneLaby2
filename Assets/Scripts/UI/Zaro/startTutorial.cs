using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class startTutorial : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Transform[] positions;

    private void OnEnable()
    {
        bg.alpha = 0f;
        bg.DOFade(1, 1).SetUpdate(true);
        positions[0].DOMove(positions[1].transform.position, 1f).SetUpdate(true);
    }

    public void yes()
    {
        //transition out to tutorial
        SpawnManager.SpawnNumber = 1;
        SceneManager.LoadScene("Tutorial");
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
