using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    static public Dictionary<string, bool> equipmentObtained = new Dictionary<string, bool>()
    {
        {"BloodAmulet", false },
        {"MindAmulet", false },
        {"DarkAmulet", false },
        {"AlanAmulet", false },
        {"KisaAmulet", false },
        {"SophieAmulet", false },
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", false },
        {"ATKRing", false },
        {"MPRing", false },
        {"HPRing", false },
        {"ATKMPRing", false },
        {"HPMPRing", false },
        {"ATKHPRing", false }
    };

    static public Dictionary<string, bool> amuletSlot = new Dictionary<string, bool>()
    {
        {"BloodAmulet", false },
        {"MindAmulet", false },
        {"DarkAmulet", false },
        {"AlanAmulet", false },
        {"KisaAmulet", false },
        {"SophieAmulet", false }
    };

    static public Dictionary<string, bool> ringSlot1 = new Dictionary<string, bool>()
    {
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", false },
        {"ATKRing", false },
        {"MPRing", false },
        {"HPRing", false }
    };

    static public Dictionary<string, bool> ringSlot2 = new Dictionary<string, bool>()
    {
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", false },
        {"ATKRing", false },
        {"MPRing", false },
        {"HPRing", false },
        {"ATKMPRing", false },
        {"HPMPRing", false },
        {"ATKHPRing", false }
    };

    static public Dictionary<string, bool> equippedRings = new Dictionary<string, bool>()
    {
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", false },
        {"ATKRing", false },
        {"MPRing", false },
        {"HPRing", false },
        {"ATKMPRing", false },
        {"HPMPRing", false },
        {"ATKHPRing", false }
    };

    private List<string> itemsToUnEquip = new List<string>();

    private LeoraChar2 leoraChar;

    private void Start()
    {
        //leoraChar = GameObject.Find("CombatPlayer").GetComponent<LeoraChar2>();
    }

    private void Awake()
    {
        leoraChar = GameObject.FindObjectOfType<LeoraChar2>();
    }

    public void GainedEquipment(string itemName)
    {
        equipmentObtained[itemName] = true;
    }

    public void EquipAmulet(string amuletChosen, bool isUnequipping = false)
    {
        foreach(var amulet in amuletSlot)
        {
            //Unequipping the active amulet
            if (amulet.Value == true)
            {
                //amuletSlot[amulet.Key] = false;
                itemsToUnEquip.Add(amulet.Key);
            }
        }

        if (itemsToUnEquip != null)
        {
            for (int i = 0; i < itemsToUnEquip.Count; i++)
            {
                //Debug.Log(itemsToUnEquip[i]);
                amuletSlot[itemsToUnEquip[i]] = false;
                RemoveAmuletBonus(itemsToUnEquip[i]);
                //Debug.Log("Removed " + itemsToUnEquip[i] + " bonus");
            }
        }

        /*foreach (var amulet in amuletSlot)
        {
            Debug.Log(amulet.Key + ": " + amulet.Value + "\n");
        }*/

        //equipping the chosen amulet
        if (!isUnequipping)
        {
            ApplyAmuletBonus(amuletChosen);
            //Debug.Log("Added " + amuletChosen + " bonus");
            amuletSlot[amuletChosen] = true;

            /*foreach (var amulet in amuletSlot)
            {
                Debug.Log("After equipping: \n" + amulet.Key + ": " + amulet.Value + "\n");
            }*/
        }

        foreach (var amulet in amuletSlot)
        {
            //Debug.Log("Equipped Amulet: \n" + amulet.Key + ": " + amulet.Value + "\n");
        }

        itemsToUnEquip.Clear();
    }

    public void EquipRing1(string ringChosen, bool isUnequipping = false)
    {
        //Debug.Log("Ring Slot 1 Equipment");
        foreach (var ring in ringSlot1)
        {
            //unequipping the active ring
            if (ring.Value == true)
            {
                itemsToUnEquip.Add(ring.Key);
                //ringSlot1[ring.Key] = false;
                //equippedRings[ring.Key] = false;
            }
        }

        if (itemsToUnEquip != null)
        {
            for (int i = 0; i < itemsToUnEquip.Count; i++)
            {
                //Debug.Log(itemsToUnEquip[i]);
                RemoveRingBonus(itemsToUnEquip[i]);
                ringSlot1[itemsToUnEquip[i]] = false;
                equippedRings[itemsToUnEquip[i]] = false;
            }
        }

        /*foreach (var ring in ringSlot1)
        {
            Debug.Log("After unequipping: \n" + ring.Key + ": " + ring.Value + "\n");
        }*/

        //equipping the ring
        if (!isUnequipping)
        {
            ApplyRingBonus(ringChosen);
            ringSlot1[ringChosen] = true;
            equippedRings[ringChosen] = true;

            /*foreach (var ring in ringSlot1)
            {
                Debug.Log("After equipping: \n" + ring.Key + ": " + ring.Value + "\n");
            }*/
        }

        /*foreach (var ring in equippedRings)
        {
            Debug.Log("All equipped rings: \n" + ring.Key + ": " + ring.Value + "\n");
        }*/

        itemsToUnEquip.Clear();
    }

    public void EquipRing2(string ringChosen, bool isUnequipping = false)
    {
        //Debug.Log("Ring Slot 2 Equipment");
        foreach (var ring in ringSlot2)
        {
            //unequipping the active ring
            if (ring.Value == true)
            {
                itemsToUnEquip.Add(ring.Key);
            }
        }

        if (itemsToUnEquip != null)
        {
            for (int i = 0; i < itemsToUnEquip.Count; i++)
            {
                //Debug.Log(itemsToUnEquip[i]);
                RemoveRingBonus(itemsToUnEquip[i]);
                ringSlot2[itemsToUnEquip[i]] = false;
                equippedRings[itemsToUnEquip[i]] = false;
            }
        }

        /*foreach (var ring in ringSlot2)
        {
            Debug.Log("After unequipping: \n" + ring.Key + ": " + ring.Value + "\n");
        }*/

        //equipping the ring
        if (!isUnequipping)
        {
            ApplyRingBonus(ringChosen);
            ringSlot2[ringChosen] = true;
            equippedRings[ringChosen] = true;

            /*foreach (var ring in ringSlot2)
            {
                Debug.Log("After equipping: \n" + ring.Key + ": " + ring.Value + "\n");
            }*/
        }

        /*foreach (var ring in equippedRings)
        {
            Debug.Log("All equipped rings: \n" + ring.Key + ": " + ring.Value + "\n");
        }*/

        itemsToUnEquip.Clear();
    }

    public void RemoveRingBonus(string ring)
    {
        Dictionary<string, int> baseStatSheet = new Dictionary<string, int>(leoraChar.statsSheet);

        switch (ring)
        {
            case "AdvATKRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] - 10);
                break;
            case "AdvHPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] - 50);
                break;
            case "AdvMPRing":
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] - 10);
                break;
            case "ATKRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] - 5);
                break;
            case "MPRing":
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] - 5);
                break;
            case "HPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] - 25);
                break;
            case "ATKMPRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] - 5);
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] - 5);
                break;
            case "HPMPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] - 25);
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] - 5);
                break;
            case "ATKHPRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] - 5);
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] - 25);
                break;
        }
    }

    public void ApplyRingBonus(string ring)
    {
        Dictionary<string, int> baseStatSheet = new Dictionary<string, int>(leoraChar.statsSheet);

        switch (ring)
        {
            case "AdvATKRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] + 10);
                break;
            case "AdvHPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] + 50);
                break;
            case "AdvMPRing":
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] + 10);
                break;
            case "ATKRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] + 5);
                break;
            case "MPRing":
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] + 5);
                break;
            case "HPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] + 25);
                break;
            case "ATKMPRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] + 5);
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] + 5);
                break;
            case "HPMPRing":
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] + 25);
                leoraChar.ChangeSpecificStat("MaxMana", baseStatSheet["MaxMana"] + 5);
                break;
            case "ATKHPRing":
                leoraChar.ChangeSpecificStat("Strength", baseStatSheet["Strength"] + 5);
                leoraChar.ChangeSpecificStat("MaxHealth", baseStatSheet["MaxHealth"] + 25);
                break;
        }
    }

    public void RemoveAmuletBonus(string amulet)
    {
        switch (amulet)
        {
            case "BloodAmulet":
                leoraChar.magicType = "lightMap";
                break;
            case "MindAmulet":
                leoraChar.magicType = "lightMag";
                break;
            case "DarkAmulet":
                leoraChar.magicType = "lightMag";
                break;
            case "AlanAmulet":
                leoraChar.alanAmuletActive = false;
                break;
            case "KisaAmulet":
                leoraChar.kisaAmuletActive = false;
                break;
            case "SophieAmulet":
                leoraChar.sophieAmuletActive = false;
                break;
        }
    }

    public void ApplyAmuletBonus(string amulet)
        {
            switch (amulet)
            {
                case "BloodAmulet":
                leoraChar.magicType = "bloodMag";
                    break;
                case "MindAmulet":
                leoraChar.magicType = "mindMag";
                    break;
                case "DarkAmulet":
                leoraChar.magicType = "darkMag";
                    break;
                case "AlanAmulet":
                leoraChar.alanAmuletActive = true;
                    break;
                case "KisaAmulet":
                leoraChar.kisaAmuletActive = true;
                    break;
                case "SophieAmulet":
                leoraChar.sophieAmuletActive = true;
                    break;
            }
        }
}
