using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

public class QuestMenu : MonoBehaviour
{
    //Eventually this will change based on whose dead and whose alive but thats not important rn

    [SerializeField] private Transform wholeQuestMenu;
    [SerializeField] private Transform[] locations;

    private string[] ivarDescriptions = { 
    //Ivar unobtained
    "Ivar hasn't been obtained",
    //Ivar killed
    "Ivar got cooked",
    //Ivar spared
    "Ivar is off the hook"
    };
    private string[] viinDescriptions = {
    //Viin unobtained
    "Viin hasn't been obtained",
    //Viin killed
    "Viin got cooked",
    //Viin spared
    "Viin is off the hook"
    };
    private string[] lucanDescriptions = {
    //Lucan unobtained
    "Lucan hasn't been obtained",
    //Lucan killed
    "Lucan got cooked",
    //Lucan spared
    "Lucan is off the hook"
    };


    [SerializeField] private Color[] ivarColors, viinColors, lucanColors;

    [SerializeField] private Image[] ivarImages, viinImages, lucanImages;

    [SerializeField] private TextMeshProUGUI[] description;


    //This will move the back button. Yuck. Maybe turn it off for a minute to turn it back on after the side quests come up?
    //whatever im gonna try this and see

    public void GoToSideQuest()
    {
        wholeQuestMenu.DOMove(locations[1].position, 1f).SetUpdate(true);
    }

    public void GoToCoreQuests()
    {
        wholeQuestMenu.DOMove(locations[0].position, 1f).SetUpdate(true);
    }

    private void OnEnable()
    {
        //Find who has been killed, who has not, and what quests were taken

        //then, assign descriptions
        //Debug.Log(ivarDescriptions[0]);
        //Debug.Log(ivarColors[0]);
        //Debug.Log(ivarImages[0]);
        //Debug.Log(description[0]);
        FindStatus("Ivar", ivarDescriptions, ivarColors, ivarImages, description[0]);
        FindStatus("Viin", viinDescriptions, viinColors, viinImages, description[1]);
        FindStatus("Lucan", lucanDescriptions, lucanColors, lucanImages, description[2]);

        //change ui color 
    }


    private void FindStatus(string bossName, string[] descriptionOptions, Color[] colorOptions, Image[] imageoptions, TextMeshProUGUI description)
    {
        switch (BossSaveData.bossStates[bossName])
        {
            case 0: // Not encountered
                imageoptions[0].color = colorOptions[0];
                imageoptions[1].color = colorOptions[0];
                description.text = descriptionOptions[0];
                break;
            case 1: // Encountered, killed
                imageoptions[0].color = colorOptions[1];
                imageoptions[1].color = colorOptions[1];
                description.text = descriptionOptions[1];
                imageoptions[3].gameObject.SetActive(true);
                break;
            case 2: // Encountered, spared
                imageoptions[0].color = colorOptions[0];
                imageoptions[1].color = colorOptions[0];
                description.text = descriptionOptions[2];
                imageoptions[2].gameObject.SetActive(true);
                break;
        }

    }

}
