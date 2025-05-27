using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class QuestMenu : MonoBehaviour
{
    //Eventually this will change based on whose dead and whose alive but thats not important rn

    [SerializeField] private Transform wholeQuestMenu;
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform sideQuestBtn;
    [SerializeField] private Transform mainQuestBtn;
    [SerializeField] private Button backBtn;

    private string[] ivarDescriptions = { 
    //Ivar unobtained
    "Ivar stole the Scepter of Truth, a rod of immense power. Without it, the Order cannot heal the people of Zaro.",
    //Ivar killed
    "I encountered and condemned Ivar. He was mad, and people were being harmed by his ignorance. Justified or not, he broke the law. Verita demands he answer for it.",
    //Ivar spared
    "I encountered Ivar, but I chose to let him go. I did not want to harm his family, I couldn’t live with myself if I knew I was the reason they were slain."
    };
    private string[] viinDescriptions = {
    //Viin unobtained
    "Viin has been a threat to the Order for years, murdering knights for the thrill of it.",
    //Viin killed
    "I encountered and condemned Viin. She had slain too many; she could not be let free. If Vaang suffers because of her, unfortunately, so be it.",
    //Viin spared
    "I encountered Viin, but I chose to let her go. Until a solution is found where Viin is punished and Vaang is safe, Viin runs free."
    };
    private string[] lucanDescriptions = {
    //Lucan unobtained
    "Lucan abandoned his post, causing hundreds of innocents to meet their demise in the Fall of Grest. He must answer for his crimes.",
    //Lucan killed
    "I encountered and condemned Lucan. His actions caused hundreds to fall. Innocent men, women, and children. That cannot be excused.",
    //Lucan spared
    "I encountered Lucan, but I chose to let him go. Lucan followed his heart, and although he disobeyed a direct order, he saved lives. That cannot be disputed."
    };


    private string[] alanDescriptions = {
    //alan unobtained
    "Speak to someone in Zaro to unlock this quest.",
    //working on alan
    "Slay 5 Earth Elementals (0/5)\r\nI made a bet with Alan after his recent... incident. He was wounded protecting me from elementals, and although I appreciate the gesture, earth elementals are simple once you understand them.\r\n",
    //alanfinished
    "I won the bet I had made with Alan, as I knew I would. He gave me an amulet and kind words... the latter a surprise from him. Maybe now he sees the value of life, something I’ve tried to teach him since he first became my apprentice."
    };

    private string[] kisaDescriptions = {
    //kisa unobtained
    "Speak to someone in Zaro to unlock this quest.",
    //working on kisa
    "Slay 5 Swords (0/5)\r\nAn elf in Zaro was looking for adventure but wanted me to scope out Northeast Isen before she made the journey herself. After slaying a few enemies, I should return to her with my findings.\r\n",
    //kisa finished
    "The elf was more of a novice than I thought. I suggested she wait before venturing out or form a party with more experienced adventurers. She was brindling with excitement, haphazardly handing me an amulet before rushing off."
    };

    private string[] sophDescriptions = {
    //sophie unobtained
    "Speak to someone in Zaro to unlock this quest.",
    //working on sophie
    "Slay 5 Spirits (0/5)\r\nA monk heard rumors that the spirits of the Veinwood could be behind the kidnappings; I should investigate it further and report my findings back to her.\r\n",
    //sophie finished
    "The spirits turned out to be a dead end, but it gave us both clarity. She gave me an amulet as thanks."
    };


    [SerializeField] private Color[] ivarColors, viinColors, lucanColors, alanColors, kisaColors, sophieColors;

    [SerializeField] private Image[] ivarImages, viinImages, lucanImages;

    [SerializeField] private GameObject[] alanobj, kisaobj, sophieobj;

    [SerializeField] private TextMeshProUGUI[] description;
    [SerializeField] private TextMeshProUGUI[] alanDesc, kisaDesc, sophieDesc;


    //This will move the back button. Yuck. Maybe turn it off for a minute to turn it back on after the side quests come up?
    //whatever im gonna try this and see

    public void GoToSideQuest()
    {
        wholeQuestMenu.DOMove(locations[1].position, 1f).SetUpdate(true);
        sideQuestBtn.DOMoveX(locations[4].position.x, 0.5f).SetUpdate(true).OnComplete(() => { sideQuestBtn.gameObject.SetActive(false); });
        mainQuestBtn.gameObject.SetActive(true);
        mainQuestBtn.DOMoveX(locations[3].position.x, 1f).SetUpdate(true).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainQuestBtn.gameObject);
            Navigation nav = backBtn.navigation;
            nav.selectOnLeft = mainQuestBtn.gameObject.GetComponent<Selectable>();
            backBtn.navigation = nav;
        });
    }

    public void GoToCoreQuests()
    {
        wholeQuestMenu.DOMove(locations[0].position, 1f).SetUpdate(true);
        sideQuestBtn.gameObject.SetActive(true);
        sideQuestBtn.DOMoveX(locations[2].position.x, 1f).SetUpdate(true).OnComplete(() => {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(sideQuestBtn.gameObject);
            Navigation nav = backBtn.navigation;
            nav.selectOnLeft = sideQuestBtn.gameObject.GetComponent<Selectable>();
            backBtn.navigation = nav;
        });
        mainQuestBtn.DOMoveX(locations[4].position.x, 0.5f).SetUpdate(true).OnComplete(() => { mainQuestBtn.gameObject.SetActive(false); });
    }

    private void OnEnable()
    {
        //Find who has been killed, who has not, and what quests were taken

        //then, assign descriptions
        //Debug.Log(ivarDescriptions[0]);
        //Debug.Log(ivarColors[0]);
        //Debug.Log(ivarImages[0]);
        //Debug.Log(description[0]);
        Debug.Log("Enable quest");
        FindStatus("Ivar", ivarDescriptions, ivarColors, ivarImages, description[0]);
        FindStatus("Viin", viinDescriptions, viinColors, viinImages, description[1]);
        FindStatus("Lucan", lucanDescriptions, lucanColors, lucanImages, description[2]);

        FindStatus("Alan", alanDescriptions, alanColors, alanobj, alanDesc, "AlanAmulet");
        FindStatus("Kisa", kisaDescriptions, kisaColors, kisaobj, kisaDesc, "KisaAmulet");
        FindStatus("Sophie", sophDescriptions, sophieColors, sophieobj, sophieDesc, "SophieAmulet");
        //Move back
        wholeQuestMenu.DOMove(locations[0].position, 0f).SetUpdate(true);
        sideQuestBtn.DOMoveX(locations[4].position.x, 0f).SetUpdate(true).OnComplete(() => { sideQuestBtn.gameObject.SetActive(false); });
        mainQuestBtn.DOMoveX(locations[4].position.x, 0f).SetUpdate(true).OnComplete(() => { mainQuestBtn.gameObject.SetActive(false); });

    }

    private void FindStatus(string questName, string[] descriptionOptions, Color[] colorOptions, GameObject[] characterImages, TextMeshProUGUI[] description, string amuletName)
    {
        //ORDER TO CHECK IN:

        //Do they have the amulet?

        //Have they completed the quest?

        //Have they picked up the quest?

        //If it reaches here, they have not completed the quest


        //try
        //{
            //They have begun the quest.
            characterImages[0].GetComponent<Image>().color = colorOptions[0];
            description[0].text = questName.ToUpper();
            characterImages[1].gameObject.SetActive(true);

            if (EquipmentManager.equipmentObtained[amuletName])
            {
                //They have completed the quest AND obtained the reward
                Debug.Log("finished");
                description[1].text = descriptionOptions[2];
                return;
            }
            //Check if they completed the quest or not
            if (QuestManager.IsQuestComplete(questName))
            {
                Debug.Log("finished, no item");
                //They have completed the quest, but have not gone back for the reward
                description[1].text = "Return to Zaro for your reward!";
                return;
            } else if (QuestManager.IsOnQuest(questName))
            {
                Debug.Log("picked up, on quest");
                //They have picked up the quest
                //Quest has been picked up, not completed
                description[1].text = Regex.Replace(
                    descriptionOptions[1],
                    @"\(\d+\/",
                    $"({QuestManager.questStates[questName].currentProgress}/"
                );
            } else
            {
                Debug.Log("not picked up");
                //Quest has not been picked up
                characterImages[0].GetComponent<Image>().color = colorOptions[1];
                description[0].text = "???";
                description[1].text = descriptionOptions[0];
                characterImages[1].gameObject.SetActive(false);
            }
        //}
        /*catch (Exception)
        {
            //Quest has not been picked up or something went wrong
            characterImages[0].GetComponent<Image>().color = colorOptions[1];
            description[0].text = "???";
            description[1].text = descriptionOptions[0];
            characterImages[1].gameObject.SetActive(false);
        }*/
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


    public void resetBackNav()
    {
        Navigation nav = backBtn.navigation;
        nav.selectOnLeft = sideQuestBtn.gameObject.GetComponent<Selectable>();
        backBtn.navigation = nav;
    }
}
