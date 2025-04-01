using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private Animator leoraAnimator;
    //Reference Leora if we want her sprite to change? should I just do an overlay? I might just do that


    [SerializeField] private Sprite[] items;
    //Sprites in the array: 0 for attack, 1 for MP, 2 for HP

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private EquipmentManager equipmentManager;

    //For Movement
    [SerializeField] private Transform infoHolder;
    [SerializeField] private Transform[] locations;

    [SerializeField] private CanvasGroup itemHolder;

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        itemImage.gameObject.SetActive(false);
        leoraAnimator.enabled = true;
        infoHolder.DOMove(locations[1].position, 0.5f).SetUpdate(true).OnComplete(() =>
        //itemHolder.DOFade(0, 1f).SetUpdate(true).OnComplete(() =>
        {
            infoHolder.position = locations[2].position;
            //menu.SetActive(false);
            Time.timeScale = 1;
        });
    }

    public void ChangeTextAndSprite(string dropName)
    {
        //Commented out the lines about assigning the image, will add back later
        //Debug.Log(dropName);
        switch (dropName)
        {
            case "BloodAmulet":
                itemText.text = "Amulet of the Body";
                itemImage.sprite = items[0];
                itemDescription.text = "The gemstone of the amulet is a deep burgundy, so deep it almost looks like blackberry jam. When rubbed, it coats your fingers in an odd red substance, not quite blood but not quite... normal. <color=#92dae8>Allows Leora to use Blood Magic.</color>";
                break;
            case "MindAmulet":
                itemText.text = "Amulet of the Mind";
                itemImage.sprite = items[1];
                itemDescription.text = "Unstable is the only way to describe this amulet, the gemstone cracking from the inside out. The longer it is worn, the more cracks appear. It is only a matter of time before it crumbles, yet it is still passed from wearer to wearer. <color=#92dae8>Allows Leora to use Psychic Magic.</color>";
                break;
            case "DarkAmulet":
                itemText.text = "Amulet of the Heart";
                itemImage.sprite = items[2];
                itemDescription.text = "This amulet is rough around the edges, carved into the shape of a heart with kind yet inexperienced hands. This once pure gemstone has eroded with time, the clear quartz now a dull, muted black. <color=#92dae8>Allows Leora to use Dark magic.</color>";
                break;
            case "AlanAmulet":
                itemText.text = "Amulet of Tenacity";
                itemImage.sprite = items[3];
                itemDescription.text = "Even in the deepest darkness, this amulet shines– it's fiery orange subtle yet persistent. <color=#92dae8>Increases Leora's attack based on how many criminals are condemned.</color>";
                break;
            case "KisaAmulet":
                itemText.text = "Amulet of Performance";
                itemImage.sprite = items[4];
                itemDescription.text = "The amulet emits a gentle hum, even if not worn. It is a calming tune, one that could turn the most horrid monster docile. <color=#92dae8>Health and Mana pickups restore double their original value.</color>";
                break;
            case "SophieAmulet":
                itemText.text = "Amulet of Thunderstorm";
                itemImage.sprite = items[5];
                itemDescription.text = "The gem of this amulet crackles and shimmers, almost as if there was a storm rumbling within a glassy enclosure. <color=#92dae8> Allows Leora to critical hit for double damage but reduces base attack while equipped.</color>";
                break;
            case "AdvATKRing":
                itemText.text = "Ring of Advanced Power";
                itemImage.sprite = items[6];
                itemDescription.text = "The gemstone sparkles a vibrant vermillion, the metal polished and begging to be worn. <color=#92dae8>Greatly increases Leora's attack.</color>";
                break;
            case "AdvHPRing":
                itemText.text = "Ring of Advanced Vitality";
                itemImage.sprite = items[7];
                itemDescription.text = "The gemstone glitters like leaves dancing in the wind, the metal polished and begging to be worn. <color=#92dae8>Increases Leora's Health by 50 points.</color>";
                break;
            case "AdvMPRing":
                itemText.text = "Ring of Advanced Mana";
                itemImage.sprite = items[8];
                itemDescription.text = "The gemstone shimmers like the sun illuminating a sea, the metal polished and begging to be worn. <color=#92dae8>Increases Leora's Mana by 10 points.</color>";
                break;
            case "ATKRing":
                itemText.text = "Ring of Power";
                itemImage.sprite = items[9];
                itemDescription.text = "The gemstone gives a subtle glimpse of crimson, but it is weakening. The metal was once high-quality, but has begun withering with time. <color=#92dae8>Slightly increases Leora's attack.</color>";
                break;
            case "MPRing":
                itemText.text = "Ring of Mana";
                itemImage.sprite = items[10];
                itemDescription.text = "The gemstone gives a subtle glimpse of ocean blue, but it is weakening. The metal was once high-quality, but has begun withering with time. <color=#92dae8>Increases Leora's Mana by 5 points.</color>";
                break;
            case "HPRing":
                itemText.text = "Ring of Vitality";
                itemImage.sprite = items[11];
                itemDescription.text = "The gemstone gives a subtle glimpse of emerald green, but it is aging. The metal was high-quality, but has begun withering with time. <color=#92dae8>Increases Leora's Health by 25 points.</color>";
                break;
            case "ATKMPRing":
                itemText.text = "Ring of Might and Magic";
                itemImage.sprite = items[12];
                itemDescription.text = "Crimson and teal fuse into one in this ring’s gemstone, symbolizing the unity of might and magic. <color=#92dae8>Increase Leora’s Mana by 5 points and slightly increase Leora’s attack.</color>";
                break;
            case "HPMPRing":
                itemText.text = "Ring of Vigor and Magic";
                itemImage.sprite = items[13];
                itemDescription.text = "Forest green and teal fuse into one in this ring’s gemstone, symbolizing the unity of vigor and magic. <color=#92dae8>Increase Leora’s HP by 25 points and her Mana by 5 points</color>";
                break;
            case "ATKHPRing":
                itemText.text = "Ring of Might and Vigor";
                itemImage.sprite = items[14];
                itemDescription.text = "Crimson and forest green fuse into one in this ring’s gemstone, symbolizing the unity of might and magic. <color=#92dae8>Increase Leora’s HP by 25 points and slightly increase Leora’s attack.</color>";
                break;
            default:
                Debug.Log("I couldn't find the item equipped. Sorry pookie");
                break;
        }
    }

    public void openItemMenu(string itemName)
    {
        Time.timeScale = 0;

        ChangeTextAndSprite(itemName);

        equipmentManager.GainedEquipment(itemName);

        infoHolder.DOMove(locations[0].position, 0.5f).SetUpdate(true);
        //itemHolder.DOFade(1, 1f).SetUpdate(true);
        itemImage.gameObject.SetActive(true);
        leoraAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        leoraAnimator.enabled = false;
        leoraAnimator.GetComponent<SpriteRenderer>().sprite = items[15];
    }

    public void Awake()
    {
        equipmentManager = FindObjectOfType<EquipmentManager>();
    }
}
