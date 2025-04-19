using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class gainQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questStatus, questTitle, questdesc;
    [SerializeField] private Transform endPos, npcPos;
    [SerializeField] private GameObject NPCWeAreTalkingTo, contButton;
    [SerializeField] private Transform menuMover;
    [SerializeField] private CanvasGroup npcCanvas, nameCanvas;

    private string[] alanDescriptions = {
    //working on alan
    "I made a bet with Alan after his recent... incident. He was wounded protecting me from elementals, and although I appreciate the gesture, earth elementals are simple once you understand them.\r\n",
    //alanfinished
    "I won the bet I had made with Alan, as I knew I would. He gave me an amulet and kind words... the latter a surprise from him. Maybe now he sees the value of life, something I’ve tried to teach him since he first became my apprentice."
    };
    private string[] alanTitles = {
    //working on alan
    "Slay 5 Earth Elementals",
    //alanfinished
    "Amulet gained!"
    };

    private string[] kisaDescriptions = {
    //working on kisa
    "An elf in Zaro was looking for adventure but wanted me to scope out Northeast Isen before she made the journey herself. After slaying a few enemies, I should return to her with my findings.\r\n",
    //kisa finished
    "The elf was more of a novice than I thought. I suggested she wait before venturing out or form a party with more experienced adventurers. She was brindling with excitement, haphazardly handing me an amulet before rushing off."
    };
    private string[] kisaTitles = {
    //working on kisa
    "Slay 5 Swords",
    //kisa finished
    "Amulet gained!"
    };

    private string[] sophDescriptions = {
    //working on sophie
    "A monk heard rumors that the spirits of the Veinwood could be behind the kidnappings; I should investigate it further and report my findings back to her.\r\n",
    //sophie finished
    "The spirits turned out to be a dead end, but it gave us both clarity. She gave me an amulet as thanks."
    };

    private string[] sophTitles = {
    //working on sophie
    "Slay 5 Spirits",
    //sophie finished
    "Amulet gained!"
    };


    // Start is called before the first frame update
    public void ShowQuestStart(string whoIsTalking, bool isQuestCompleted)
    {

        nameCanvas.DOFade(0, 1f).SetUpdate(true);
        //Duplicate the NPC we are talking to
        //update quest log with quest data
        OpenPauseMenu.GLOBALcanOpenPause = false;

        if (isQuestCompleted == true)
        {
            questStatus.text = "QUEST COMPLETED!";
        } else
        {
            questStatus.text = "QUEST GAINED!";
        }

        if (whoIsTalking.EndsWith("AlanQuest"))
        {
            NPCWeAreTalkingTo = GameObject.Find("[VN Controller]/Root/Canvas - Main/Layers/2 - Characters/Character - [alan]");
            questTitle.text = alanTitles[Convert.ToInt32(isQuestCompleted)];
            questdesc.text = alanDescriptions[Convert.ToInt32(isQuestCompleted)];
        }
        else if (whoIsTalking.EndsWith("KisaQuest"))
        {
            NPCWeAreTalkingTo = GameObject.Find("[VN Controller]/Root/Canvas - Main/Layers/2 - Characters/Character - [kisa]");
            questTitle.text = kisaTitles[Convert.ToInt32(isQuestCompleted)];
            questdesc.text = kisaDescriptions[Convert.ToInt32(isQuestCompleted)];
            //npcsWeCanTalkTo[1].SetActive(true);
        }
        else
        {
            NPCWeAreTalkingTo = GameObject.Find("[VN Controller]/Root/Canvas - Main/Layers/2 - Characters/Character - [sophie]");
            questTitle.text = sophTitles[Convert.ToInt32(isQuestCompleted)];
            questdesc.text = sophDescriptions[Convert.ToInt32(isQuestCompleted)];
            //npcsWeCanTalkTo[2].SetActive(true);
        }
        npcCanvas = NPCWeAreTalkingTo.GetComponent<CanvasGroup>();
        NPCWeAreTalkingTo.transform.DOMoveX(npcPos.transform.position.x, 1f).SetUpdate(true).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(contButton);
        });
    }

    public void closeQuestGainedMenu()
    {
        //When the button is clicked. close menu by moving it out of the way
        Time.timeScale = 1f;
        OpenPauseMenu.canOpenPause = true;
        OpenPauseMenu.GLOBALcanOpenPause = true;
        menuMover.DOMove(endPos.transform.position, 1).SetUpdate(true).SetEase(Ease.OutCubic);
        NPCWeAreTalkingTo.transform.DOMoveX(endPos.transform.position.x, 0.5f).SetUpdate(true);
        npcCanvas.DOFade(0, 0.5f);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
