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

    [SerializeField] private bool[] ringsObtained;
    // ^^^ This is a placeholder for where we will store the data!
    // 0, 1 = Adv Attack ring
    // 2, 3 = Adv Mana ring
    // 4, 5 = Adv HP ring
    // 6 = Atk ring
    // 7 = MP ring
    // 8 = HP ring


    [SerializeField] private Image[] ringIcons;
    [SerializeField] private GameObject[] ringIcons_borders;

    [SerializeField] private Sprite[] ringsForLeoraShadow;

    [Header("Amulets")]
    [SerializeField] private bool[] amuletsObtained;
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
        checkIfObtained(ringsObtained, ringIcons);
        checkIfObtained(amuletsObtained, amuletIcons);
        // Then, do they have anything equipped? If so, change the border of the box

        // Finally, if they do have something equipped, show it on Leora's sprite
    }


    private void checkIfObtained(bool[] boolsToCheck, Image[] uiImages)
    {
        for (int i = 0; i < boolsToCheck.Length; i++)
        {
            switch (boolsToCheck[i])
            {
                case true:
                    uiImages[i].color = Color.white;
                    break;

                case false:
                    uiImages[i].color = tintColor;
                    break;
            }
        }
    }
}
