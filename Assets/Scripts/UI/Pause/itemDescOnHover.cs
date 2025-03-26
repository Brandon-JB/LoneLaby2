using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class itemDescOnHover : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;
    [SerializeField] private Transform uiElement_name;
    [SerializeField] private Transform uiElement_desc;
    //Instead of making one for each, I'm just gonna move around the same UI element and call it a day
    [SerializeField] private Transform[] uiLocations;

    [SerializeField] private Transform[] uiLocations_name;
    [SerializeField] private Transform[] uiLocations_desc;

    //We may need to make a different one for the rings, but for now I'm just worried about the amulets

    [SerializeField] private TextMeshProUGUI itemNameTXT;
    [SerializeField] private TextMeshProUGUI itemDescTXT;

    [SerializeField] private string[] allItemNames;
    [SerializeField] private string[] allItemDescs;

    public void TriggerWithDelay(string itemName)
    {
        closeItemUI(itemName);
    }

    private IEnumerator ShowUIAfterDelay(string itemName)
    {
        yield return new WaitForSecondsRealtime(0.5f); // Wait for 2 seconds

        //Check if we even have this thing

        int keyForArrays = uglyAssSwitchStatement(itemName);

        uiElement.transform.position = uiLocations[keyForArrays].position;
        uiElement_name.position = uiLocations_name[keyForArrays].position;
        uiElement_desc.position = uiLocations_desc[keyForArrays].position;
        if (!EquipmentManager.equipmentObtained[itemName])
        {
            //change UI text
            itemNameTXT.text = "???";
            //change ui desc
            itemDescTXT.text = "";
        } else
        {
            //change UI text
            itemNameTXT.text = allItemNames[keyForArrays];
            //change ui desc
            itemDescTXT.text = allItemDescs[keyForArrays];
        }
        uiElement.SetActive(true); // Show the UI
        StopAllCoroutines();
    }

    private int uglyAssSwitchStatement(string item)
    {
        switch (item)
        {
            case "BloodAmulet":
                return 0;
            case "MindAmulet":
                return 1;
            case "DarkAmulet":
                return 2;
            case "AlanAmulet":
                return 3;
            case "KisaAmulet":
                return 4;
            case "SophieAmulet":
                return 5;
            case "AdvATKRing":
                return 6;
            case "AdvHPRing":
                return 7;
            case "AdvMPRing":
                return 8;
            case "ATKRing":
                return 9;
            case "MPRing":
                return 10;
            case "HPRing":
                return 11;
            case "ATKMPRing":
                return 12;
            case "HPMPRing":
                return 13;
            case "ATKHPRing":
                return 14;
            default:
                Debug.Log("I couldn't find the item equipped. Sorry pookie");
                return 0;
        }
    }

    public void closeItemUI(string itemName = "")
    {
        //Called when you switch buttons
        StopAllCoroutines();
        uiElement.SetActive(false);
        if (itemName != "")
        {
            StartCoroutine(ShowUIAfterDelay(itemName));
        }
    }
}

