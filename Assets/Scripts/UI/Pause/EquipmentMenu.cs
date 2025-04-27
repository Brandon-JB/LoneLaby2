using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : MonoBehaviour
{

    private HUD_Equipment hudEquipment;

    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private TextMeshProUGUI magicType;
    [SerializeField] private Image magicTypeIcon;
    [SerializeField] private Sprite[] magicTypeSprites;

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
    [SerializeField] public GameObject[] ringIcons_borders;

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
    [SerializeField] public GameObject[] amuletIcons_borders;

    [SerializeField] private Sprite[] amuletsForLeoraShadow;

    private GameObject lastActiveAmulet = null;
    private GameObject lastEquippedRing1 = null;
    private GameObject lastEquippedRing2 = null;

    public void OnEnable()
    {
        // Check what items the player has and display them accordingly
        checkIfObtained(EquipmentManager.equipmentObtained, itemIcons);
        //checkIfWearing(EquipmentManager.amuletSlot, amuletIcons_borders, 1);
        //Debug.Log("got to amulets");
        //checkIfWearing(EquipmentManager.amuletSlot, amuletIcons_borders, 1);
        //Debug.Log("got through amulets");
        //checkIfWearing(EquipmentManager.ringSlot1, ringIcons_borders, 0);
        //Debug.Log("got through rings 1");
        //checkIfWearing(EquipmentManager.ringSlot2, ringIcons_borders, 2);
        //Debug.Log("got through rings 2");

        // Finally, if they do have something equipped, show it on Leora's sprite
        hudEquipment = FindObjectOfType<HUD_Equipment>();
    }

    public void setGlow()
    {
        lastActiveAmulet = checkIfWearing(EquipmentManager.amuletSlot, amuletIcons_borders, 1);
        //Debug.Log("got through amulets");
        lastEquippedRing1 = checkIfWearing(EquipmentManager.ringSlot1, ringIcons_borders, 0);
        //Debug.Log("got through rings 1");
        lastEquippedRing2 =  checkIfWearing(EquipmentManager.ringSlot2, ringIcons_borders, 2);
        //Debug.Log("got through rings 2");
        //FOr magic
        foreach (var equip in EquipmentManager.amuletSlot)
        {
            if (equip.Value == true)
            {
                changeMagicType(equip.Key);
                return;
            }
        }
        changeMagicType("");
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

    //Slot number is for changing the HUD
    public GameObject checkIfWearing(Dictionary<string, bool> wornEquipment, GameObject[] uiImages, int slotNumber)
    {
        int i = 0;
        foreach (var equip in wornEquipment)
        {
            if (i >= uiImages.Length) break; // Prevents out-of-bounds errors
            if (equip.Value)
            {
                Debug.Log(equip.Key);
                if (hudEquipment == null)
                {
                    hudEquipment = FindObjectOfType<HUD_Equipment>();
                }
                LeoraShadowEquipment[slotNumber].sprite = hudEquipment.uglyAssSwitchStatement(equip.Key, slotNumber == 1? amuletsForLeoraShadow : ringsForLeoraShadow);
                uiImages[i].SetActive(true);
                return uiImages[i];
            }
            //uiImages[i].color = equip.Value ? Color.white : tintColor;
            i++;
        }
        return null;
    }

    private bool checkIfWearingRing(Dictionary<string, bool> wornEquipment)
    {
        foreach (var equip in wornEquipment)
        {
            if (equip.Value == true)
            {
                Debug.Log(equip.Key); 
                return true;
            }
        }
        return false;
    }

    private bool checkIfWearingItem(Dictionary<string, bool> wornEquipment, string key)
    {
        foreach (var equip in wornEquipment)
        {
            if (equip.Value == true) return true;
        }
        return false;
    }


    public void EquipItem(GameObject glowToCheckIfEquipped)
    {
        if (hudEquipment == null)
        {
            hudEquipment = GameObject.FindWithTag("HUD").GetComponent<HUD_Equipment>();
        }
        texts[0].text = "Didn't even get to the bottom. Item name is " +glowToCheckIfEquipped.name;
        string item = glowToCheckIfEquipped.name;

        //check if we can even equip this thing

        if (EquipmentManager.equipmentObtained[item] == false)
        {
            //Hey loser! You don't have the item! HAHA
            audioManager.Instance.playSFX(37);
            //Play an angry sfx
            return;
        }


        //If this is an amulet, just do it. If this is a ring, figure out what ring we're overwriting.
        if (item.Substring(item.Length - 6) == "Amulet")
        {
            Debug.Log(lastActiveAmulet);

            if (lastActiveAmulet != null && lastActiveAmulet != glowToCheckIfEquipped) 
            {
                lastActiveAmulet.SetActive(false);
            }
            Debug.Log("Trying to equip amulet...");
            equipmentManager.EquipAmulet(item, glowToCheckIfEquipped.activeInHierarchy);
            Debug.Log("Equipped amulet!");
            if (glowToCheckIfEquipped.activeInHierarchy)
            {
                audioManager.Instance.playSFX(41);
                Debug.Log("Change HUD (ring is equipped, unequip it)");
                hudEquipment.changeHUDOnEquip("", 1);
                Debug.Log("Change Leora Sprite (ring is equipped, unequip it)");
                LeoraShadowEquipment[1].sprite = hudEquipment.uglyAssSwitchStatement("", amuletsForLeoraShadow);
                //Update magic type
                changeMagicType("");
            } //Check if there is something else equipped
            //else if (lastActiveAmulet.activeInHierarchy)
            //{
                
            //}
            else
            {
                audioManager.Instance.playSFX(40);
                Debug.Log("Change HUD (ring is NOT equipped)");
                hudEquipment.changeHUDOnEquip(item, 1);
                Debug.Log("Change Leora Sprite (ring is NOT equipped)");
                LeoraShadowEquipment[1].sprite = hudEquipment.uglyAssSwitchStatement(item, amuletsForLeoraShadow);
                //Update magic type
                changeMagicType(item);
            }
            Debug.Log("Set as last active amulet");
            lastActiveAmulet = glowToCheckIfEquipped;
            //glowToCheckIfEquipped.SetActive(!glowToCheckIfEquipped.activeInHierarchy);
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

            //bool wearingRings = checkIfWearingRing(EquipmentManager.equippedRings);

            bool isInRingSlot1 = false;

            //if the clicked ring is equipped
            if (glowToCheckIfEquipped.activeInHierarchy)
            {
                audioManager.Instance.playSFX(40);
                //glowToCheckIfEquipped.SetActive(!glowToCheckIfEquipped.activeInHierarchy);

                //Debug.Log("Glowing Ring is " + item);
                foreach (var ring in EquipmentManager.ringSlot1)
                {
                    if (item == ring.Key && ring.Value == true)
                    {
                        isInRingSlot1 = true;
                    }
                }

                //if the equipped ring is in ring slot 1
                if (isInRingSlot1)
                {
                    lastEquippedRing1 = glowToCheckIfEquipped;

                    equipmentManager.EquipRing1(item, true);

                    if (glowToCheckIfEquipped.activeInHierarchy)
                    {
                        hudEquipment.changeHUDOnEquip("", 0);
                        LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement("", ringsForLeoraShadow);
                    }
                    else
                    {
                        hudEquipment.changeHUDOnEquip(item, 0);
                        LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement(item, ringsForLeoraShadow);
                    }
                }
                //If the equipped ring is in ring slot 2
                else
                {
                    lastEquippedRing2 = glowToCheckIfEquipped;

                    equipmentManager.EquipRing2(item, true);
                    if (glowToCheckIfEquipped.activeInHierarchy)
                    {
                        hudEquipment.changeHUDOnEquip("", 2);
                        LeoraShadowEquipment[2].sprite = hudEquipment.uglyAssSwitchStatement("", ringsForLeoraShadow);
                    }
                    else
                    {
                        hudEquipment.changeHUDOnEquip(item, 2);
                        LeoraShadowEquipment[2].sprite = hudEquipment.uglyAssSwitchStatement(item, ringsForLeoraShadow);
                    }
                }
            }
            //if the clicked ring is not equipped
            else
            {
                audioManager.Instance.playSFX(41);
                //if either ring slot 1 or 2 are available
                if (!checkIfWearingRing(EquipmentManager.ringSlot1) || !checkIfWearingRing(EquipmentManager.ringSlot2))
                {
                    //glowToCheckIfEquipped.SetActive(!glowToCheckIfEquipped.activeInHierarchy);

                    //Ring slot one
                    if (!ringSlot1)
                    {
                        lastEquippedRing1 = glowToCheckIfEquipped;

                        equipmentManager.EquipRing1(item, false);
                        if (glowToCheckIfEquipped.activeInHierarchy)
                        {
                            hudEquipment.changeHUDOnEquip("", 0);
                            LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement("", ringsForLeoraShadow);
                        }
                        else
                        {
                            hudEquipment.changeHUDOnEquip(item, 0);
                            LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement(item, ringsForLeoraShadow);
                        }
                    }
                    //ring slot two
                    else
                    {
                        lastEquippedRing2 = glowToCheckIfEquipped;

                        equipmentManager.EquipRing2(item, false);
                        if (glowToCheckIfEquipped.activeInHierarchy)
                        {
                            hudEquipment.changeHUDOnEquip("", 2);
                            LeoraShadowEquipment[2].sprite = hudEquipment.uglyAssSwitchStatement("", ringsForLeoraShadow);
                        }
                        else
                        {
                            hudEquipment.changeHUDOnEquip(item, 2);
                            LeoraShadowEquipment[2].sprite = hudEquipment.uglyAssSwitchStatement(item, ringsForLeoraShadow);
                        }
                    }
                }
                //Do nothing
                else
                {
                    lastEquippedRing1.SetActive(false);

                    lastEquippedRing1 = glowToCheckIfEquipped;

                    equipmentManager.EquipRing1(item, false);
                    if (glowToCheckIfEquipped.activeInHierarchy)
                    {
                        hudEquipment.changeHUDOnEquip("", 0);
                        LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement("", ringsForLeoraShadow);
                    }
                    else
                    {
                        hudEquipment.changeHUDOnEquip(item, 0);
                        LeoraShadowEquipment[0].sprite = hudEquipment.uglyAssSwitchStatement(item, ringsForLeoraShadow);
                    }
                }
            }


            /*
            if (ringSlot1)
            {
                //Honestly, we just need to check if the first slot is in use. If not, defaults to second slot
                //glow to check if equipped is here to check if someone pressed this to unequip an item. if it's on, that means someone wants it off. if that makes sense
                equipmentManager.EquipRing2(item, glowToCheckIfEquipped.activeInHierarchy);
                hudEquipment.changeHUDOnEquip(item, 2);
                //} else if (ringSlot2)
                //{
                //    equipmentManager.EquipRing1(item);
                //}
                //if nothing is equipped, defaults to slot 1

            }
            else
            {
                equipmentManager.EquipRing1(item, glowToCheckIfEquipped.activeInHierarchy);

                //If HUD isn't set, make sure that it is.
                if (!hudEquipment)
                {
                    hudEquipment = GameObject.FindWithTag("HUD").GetComponent<HUD_Equipment>();
                }
                hudEquipment.changeHUDOnEquip(item, 0);
            }
            */

        }
        texts[0].text = "Item glow is " + glowToCheckIfEquipped.activeInHierarchy + ". Item name is " + item;
        glowToCheckIfEquipped.SetActive(!glowToCheckIfEquipped.activeInHierarchy);
        texts[1].text = "Item glow is " + glowToCheckIfEquipped.activeInHierarchy;
        //hudEquipment.changeHUDOnEquip(item,1);
    }


    public void changeMagicType(string amuletName)
    {
        //Debug.Log("I am changing the thing");
        switch (amuletName)
        {
            case "BloodAmulet":
                magicType.text = "Deals damage, \nsiphon health";
                magicTypeIcon.sprite = magicTypeSprites[1];
                break;
            case "MindAmulet":
                magicType.text = "Deals damage, \nslows enemies";
                magicTypeIcon.sprite = magicTypeSprites[2];
                break;
            case "DarkAmulet":
                magicType.text = "Deals damage \nin pulses";
                magicTypeIcon.sprite = magicTypeSprites[3];
                break;
            default:
                //Light magic
                magicType.text = "Deals damage, \nstuns enemies";
                magicTypeIcon.sprite = magicTypeSprites[0];
                break;
        }

        //change text
        //change icon
    }
}
