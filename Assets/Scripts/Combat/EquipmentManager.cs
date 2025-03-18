using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    static public Dictionary<string, bool> equipmentObtained = new Dictionary<string, bool>()
    {
        {"BloodAmulet", true },
        {"MindAmulet", false },
        {"DarkAmulet", false },
        {"AlanAmulet", false },
        {"KisaAmulet", false },
        {"SophieAmulet", false },
        {"AdvATKRing", false },
        {"AdvHPRing", false },
        {"AdvMPRing", false },
        {"ATKRing", true },
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

    static public Dictionary<string, bool> ringSlot3 = new Dictionary<string, bool>()
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

    public void EquipAmulet(string amuletChosen, bool isUnequipping = false)
    {
        foreach(var amulet in amuletSlot)
        {
            //Unequipping the active amulet
            if (amulet.Value == true)
            {
                amuletSlot[amulet.Key] = false;
            }
        }
        //equipping the chosen amulet
        if (!isUnequipping)
        {
            amuletSlot[amuletChosen] = true;
        }
    }

    public void EquipRing1(string ringChosen, bool isUnequipping = false)
    {
        foreach (var ring in ringSlot1)
        {
            //unequipping the active ring
            if (ring.Value == true)
            {
                ringSlot1[ring.Key] = false;
                equippedRings[ring.Key] = false;
            }
        }

        //equipping the ring
        if (!isUnequipping)
        {
            ringSlot1[ringChosen] = true;
            equippedRings[ringChosen] = true;
        }
    }

    public void EquipRing2(string ringChosen, bool isUnequipping = false)
    {
        foreach (var ring in ringSlot2)
        {
            //unequipping the active ring
            if (ring.Value == true)
            {
                ringSlot2[ring.Key] = false;
                equippedRings[ring.Key] = false;
            }
        }

        //equipping the ring
        if (!isUnequipping)
        {
            ringSlot2[ringChosen] = true;
            equippedRings[ringChosen] = true;
        }
    }

    public void EquipRing3(string ringChosen, bool isUnequipping = false)
    {
        foreach (var ring in ringSlot3)
        {
            //unequipping the active ring
            if (ring.Value == true)
            {
                ringSlot3[ring.Key] = false;
                equippedRings[ring.Key] = false;
            }
        }
        //equipping the ring
        if (!isUnequipping)
        {
            ringSlot3[ringChosen] = true;
            equippedRings[ringChosen] = true;
        }
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
