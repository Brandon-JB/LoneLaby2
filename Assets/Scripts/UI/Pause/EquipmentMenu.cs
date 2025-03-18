using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : MonoBehaviour
{

    [SerializeField] private Color tintColor;

    [SerializeField] private Image[] LeoraShadowEquipment;
    // 0 = Amulet
    // 1 = ring 1
    // 2 = ring 2

    [Header("Rings")]

    //[SerializeField] private bool[] ringsObtained;
    // ^^^ This is a placeholder for where we will store the data!
    // 0, 1 = Adv Attack ring
    // 2, 3 = Adv Mana ring
    // 4, 5 = Adv HP ring
    // 6 = Atk ring
    // 7 = MP ring
    // 8 = HP ring


    [SerializeField] private Image[] ringIcons;
    [SerializeField] private GameObject[] ringIcons_borders;

    //This is all of them. ik its not conventional shut up
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private GameObject[] itemIcons_borders;

    [SerializeField] private Sprite[] ringsForLeoraShadow;

    [Header("Amulets")]
    //[SerializeField] private bool[] amuletsObtained;
    [SerializeField] private EquipmentManager equipmentManager;
    // ^^^ This is a placeholder for where we will store the data!
    // 0 = Amulet of the Body (blood? I'm considering changing the name of this one)
    // 1 = Amulet of the Heart
    // 2 = Amulet of the Mind
    // 7 = Amulet of Tenacity
    // 8 = Amulet of Performance
    // 9 = Amulet of Thunderstorm

    [SerializeField] private Image[] amuletIcons;
    [SerializeField] private GameObject[] amuletIcons_borders;

    [SerializeField] private Sprite[] amuletsForLeoraShadow;
    public void Start()
    {
        // Check what items the player has and display them accordingly
        checkIfObtained(EquipmentManager.equipmentObtained, itemIcons);
        //checkIfObtained(amuletsObtained, amuletIcons);

        // Then, do they have anything equipped? If so, change the border of the box
        checkIfWearing(EquipmentManager.amuletSlot, amuletIcons_borders);
        checkIfWearing(EquipmentManager.ringSlot1, ringIcons_borders);
        checkIfWearing(EquipmentManager.ringSlot2, ringIcons_borders);


        // Finally, if they do have something equipped, show it on Leora's sprite
    }


    private void checkIfObtained(Dictionary<string, bool> equipmentObtained, Image[] uiImages)
    {
        int i = 0;
        foreach (var equip in equipmentObtained)
        {
            if (i >= uiImages.Length) break; // Prevents out-of-bounds errors

            uiImages[i].color = equip.Value ? Color.white : tintColor;
            i++;
        }
    }

    private void checkIfWearing(Dictionary<string, bool> wornEquipment, GameObject[] uiImages)
    {
        int i = 0;
        foreach (var equip in wornEquipment)
        {
            if (i >= uiImages.Length) break; // Prevents out-of-bounds errors

            uiImages[i].SetActive(equip.Value);
            if (equip.Value) break; // Just stop the function if equip is true
            //uiImages[i].color = equip.Value ? Color.white : tintColor;
            i++;
        }
    }

    private bool checkIfWearingRing(Dictionary<string, bool> wornEquipment)
    {
        foreach (var equip in wornEquipment)
        {
            if (equip.Value == true) return true;
        }
        return false;
    }


    public void EquipItem(GameObject glowToCheckIfEquipped)
    {
        string item = glowToCheckIfEquipped.name;// hey YOU! RENAME ALL THE GLOW ITEMS IN THE MORNING!! CALL THIS SCRIPT!! thanks pookums

        //If this is an amulet, just do it. If this is a ring, figure out what ring we're overwriting.
        if (item.Substring(item.Length - 6) == "Amulet")
        {
            equipmentManager.EquipAmulet(item, glowToCheckIfEquipped.activeInHierarchy);
        }
        else
        {
            //Is there a ring equipped in either the first or second ring slot?
            bool ringSlot1 = checkIfWearingRing(EquipmentManager.ringSlot1);
            //bool ringSlot2 = checkIfWearingRing(EquipmentManager.ringSlot1);
            //if (ringSlot1 && ringSlot2)
            //{
            //    //Two rings are equipped. Overwrite first one
            //    equipmentManager.EquipRing1(item);
            //} else
            if (ringSlot1)
            {
                //Honestly, we just need to check if the first slot is in use. If not, defaults to second slot
                //glow to check if equipped is here to check if someone pressed this to unequip an item. if it's on, that means someone wants it off. if that makes sense
                equipmentManager.EquipRing2(item, glowToCheckIfEquipped.activeInHierarchy);
                //} else if (ringSlot2)
                //{
                //    equipmentManager.EquipRing1(item);
                //}
                //if nothing is equipped, defaults to slot 1
                equipmentManager.EquipRing1(item, glowToCheckIfEquipped.activeInHierarchy);
            }
        }

        glowToCheckIfEquipped.SetActive(!glowToCheckIfEquipped.activeInHierarchy);
    }

}
