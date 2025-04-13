using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverSFX : MonoBehaviour, IPointerDownHandler
{
    //ISelectHandler, IPointerEnterHandler

    //public int sfxID_Hover = 35;
    public int sfxID_Press = 34;

    //private void Awake()
    //{
    //    button = GetComponent<Button>();
    //    if (button != null)
    //    {
    //        button.onClick.AddListener(PlayConfirmSFX);
    //    }
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    audioManager.Instance.playSFX(sfxID_Hover);
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        audioManager.Instance.playSFX(sfxID_Press);
    }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    audioManager.Instance.playSFX(sfxID_Hover); // reuse hover sound if selected by keyboard/controller
    //}

    //private void PlayConfirmSFX()
    //{
    //    audioManager.Instance.playSFX(sfxID_Press);
    //}
}
