using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class HUD_Equipment : MonoBehaviour
{
    [SerializeField] private Image[] equipment;
    [SerializeField] private Sprite[] amuletUI;
    [SerializeField] private Sprite[] ringUI;

    [SerializeField] private Sprite voidImage;

    void Start()
    {
        checkIfEquipped(EquipmentManager.ringSlot1, 0);
        checkIfEquipped(EquipmentManager.amuletSlot, 1);
        checkIfEquipped(EquipmentManager.ringSlot2, 2);
    }


    private void checkIfEquipped(Dictionary<string, bool> dict, int equipmentSpace)
    {
        int i = 0;
        //Check if anything is equipped rn. if so, show it on screen
        foreach (var equip in dict)
        {
            if (equip.Value) {
                //This is what is equipped. Check if it's a amulet or not
                if (equip.Key.Substring(equip.Key.Length - 6) == "Amulet")
                {
                    equipment[equipmentSpace].sprite = amuletUI[i];
                } else
                {
                    equipment[equipmentSpace].sprite = ringUI[i];
                }
                return;
            }
            i++;
        }
    }

    public void changeHUDOnEquip(string item, int location)
    {
        if (item == null) { equipment[location].sprite = voidImage; }

        //If null is not passed in, that means it is being equipped somewhere.

        equipment[location].sprite = uglyAssSwitchStatement(item, (location == 0 || location == 2)? ringUI : amuletUI);
    }


    private Sprite uglyAssSwitchStatement(string item, Sprite[] arrayOfSprites)
    {
        switch (item)
        {
            case "BloodAmulet":
                return arrayOfSprites[0];
            case "MindAmulet":
                return arrayOfSprites[1];
            case "DarkAmulet":
                return arrayOfSprites[2];
            case "AlanAmulet":
                return arrayOfSprites[3];
            case "KisaAmulet":
                return arrayOfSprites[4];
            case "SophieAmulet":
                return arrayOfSprites[5];
            case "AdvATKRing":
                return arrayOfSprites[0];
            case "AdvHPRing":
                return arrayOfSprites[1];
            case "AdvMPRing":
                return arrayOfSprites[2];
            case "ATKRing":
                return arrayOfSprites[3];
            case "MPRing":
                return arrayOfSprites[4];
            case "HPRing":
                return arrayOfSprites[5];
            case "ATKMPRing":
                return arrayOfSprites[6];
            case "HPMPRing":
                return arrayOfSprites[7];
            case "ATKHPRing":
                return arrayOfSprites[8];
            default:
                Debug.Log("I couldn't find the item equipped. Sorry pookie");
                return voidImage;
    }
    }
}
