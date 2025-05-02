using UnityEngine;
using DG.Tweening;

public class ivarDark : MonoBehaviour
{
    [SerializeField] public CanvasGroup darkness;
    private void OnEnable()
    {
        darkness.alpha = 0.0f;
        darkness.DOFade(1, 2f); 
    }

    public void closeMenu()
    {
        darkness.alpha = 1.0f;
        darkness.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { this.gameObject.SetActive(false); });
    }
}
