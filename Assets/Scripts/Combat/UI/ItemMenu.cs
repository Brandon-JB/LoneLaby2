using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;


    [SerializeField] private Sprite[] items;
    //Sprites in the array: 0 for attack, 1 for MP, 2 for HP

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text itemDescription;

    //For Movement

    [SerializeField] private Transform infoHolder;
    [SerializeField] private Transform[] locations;

    [SerializeField] private CanvasGroup itemHolder;

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        infoHolder.DOMove(locations[1].position, 1f).SetUpdate(true);
        itemHolder.DOFade(0, 1f).SetUpdate(true).OnComplete(() =>
        {
            menu.SetActive(false);
        });
    }

    public void ChangeTextAndSprite(string dropName)
    {
        //Commented out the lines about assigning the image, will add back later

        switch (dropName)
        {
            case "AdvATKRing":
                itemText.text = "Gained a new Ring!";
                //itemImage.sprite = items[0];
                itemDescription.text = "When worn, increases the wearer's attack by 25.";
                break;
            case "AdvMPRing":
                itemText.text = "Gained a new Ring!";
                //itemImage.sprite = items[1];
                itemDescription.text = "When worn, increases the wearer's Mana by 25.";
                break;
            case "AdvHPRing":
                itemText.text = "Gained a new Ring!";
                //itemImage.sprite = items[2];
                itemDescription.text = "When worn, increases the wearer's HP by 50.";
                break;
        }
    }

    private void OnEnable()
    {
        infoHolder.DOMove(locations[0].position,1f).SetUpdate(true);
        itemHolder.DOFade(1, 1.25f).SetUpdate(true);
    }
}
