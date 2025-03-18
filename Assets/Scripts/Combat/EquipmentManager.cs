using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    static public Dictionary<string, bool> equipmentObtained = new Dictionary<string, bool>()
    {
        {"BloodAmulet", true },
        {"MindAmulet", true },
        {"DarkAmulet", false },
        {"AlanAmulet", false },
        {"KisaAmulet", false },
        {"SophieAmulet", false },
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", true },
        {"ATKRing", true },
        {"MPRing", true },
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
            }
        }

        /*foreach (var amulet in amuletSlot)
        {
            Debug.Log(amulet.Key + ": " + amulet.Value + "\n");
        }*/

        //equipping the chosen amulet
        if (!isUnequipping)
        {
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
        switch (ring)
        {
            case "AdvATKRing":

                break;
        }
    }

    public void ApplyRingBonus(string ring)
    {
        switch (ring)
        {
            case "AdvATKRing":

                break;
        }
    }
}
