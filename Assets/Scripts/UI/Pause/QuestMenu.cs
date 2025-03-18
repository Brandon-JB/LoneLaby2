using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestMenu : MonoBehaviour
{
    //Eventually this will change based on whose dead and whose alive but thats not important rn

    [SerializeField] private Transform wholeQuestMenu;
    [SerializeField] private Transform[] locations;
    //This will move the back button. Yuck. Maybe turn it off for a minute to turn it back on after the side quests come up?
    //whatever im gonna try this and see

    public void GoToSideQuest()
    {
        wholeQuestMenu.DOMove(locations[1].position, 1f);
    }

    public void GoToCoreQuests()
    {
        wholeQuestMenu.DOMove(locations[0].position, 1f);
    }
}
