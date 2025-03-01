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
    [SerializeField] private TMP_Text itemDescription;

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
                itemText.text = "Gained a new Ring!";
                itemImage.sprite = items[0];
                itemDescription.text = "Plcaeholder";
                break;
            case "MPRing":
                itemText.text = "Gained a new Ring!";
                itemImage.sprite = items[1];
                itemDescription.text = "Plcaeholder";
                break;
            case "HPRing":
                itemText.text = "Gained a new Ring!";
                itemImage.sprite = items[2];
                itemDescription.text = "Plcaeholder";
                break;
        }
    }
}
