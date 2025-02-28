using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;


    [SerializeField] private Sprite[] items;
    //Sprites in the array: 0 for attack, 1 for MP, 2 for HP

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemText;

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void ChangeTextAndSprite(string dropName)
    {
        switch (dropName)
        {
            case "ATKRing":
                itemText.text = "ADV Attack Ring";
                itemImage.sprite = items[0];
                break;
            case "MPRing":
                itemText.text = "ADV Mana Ring";
                itemImage.sprite = items[1];
                break;
            case "HPRing":
                itemText.text = "ADV Health Ring";
                itemImage.sprite = items[2];
                break;
        }
    }
}
