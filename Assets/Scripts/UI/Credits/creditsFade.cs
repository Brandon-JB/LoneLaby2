using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class creditsFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup selectedGameObject;
    [SerializeField] private GameObject nextGameObject;
    [SerializeField] private int time = 13;

    private void OnEnable()
    {
        selectedGameObject.DOFade(1, 1).OnComplete(() => {
            StartCoroutine(waitToDisappear());
        });
    }

    IEnumerator waitToDisappear()
    {
        yield return new WaitForSeconds(time);
        selectedGameObject.DOFade(0, 1).OnComplete(() => {
            nextGameObject.SetActive(true);
            selectedGameObject.gameObject.SetActive(false);
        });
    }
}
