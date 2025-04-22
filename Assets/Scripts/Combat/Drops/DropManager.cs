using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    static private Dictionary<string, int> enemyDeathCount = new Dictionary<string, int>()
    {
        {"Slime", 0},
        {"EarthElement", 0},
        {"TreeMimic", 0},
        {"Sword", 0},
        {"Spirit", 0},
        {"Shield", 0}
    };

    public int pityItemDrop = 30;

    public void RandomizedDrops(Vector3 deathLocation, string enemyName)
    {
        int randomNum = Random.Range(1, 31);

        if (enemyDeathCount.ContainsKey(enemyName))
        {
            enemyDeathCount[enemyName]++;

            if (enemyDeathCount[enemyName] >= pityItemDrop)
            {
                randomNum = 30;
            }
        }

        //randomNum = 30;

        if (randomNum >= 1 && randomNum <= 23)
        {
            //Debug.Log("Nothing");
        }
        else if (randomNum >= 24 &&  randomNum <= 25)
        {
            GameObject drops = Instantiate(dropPrefab, deathLocation, Quaternion.identity);
            Drops dropScript = drops.GetComponent<Drops>();
            dropScript.SetUpItem("Small HP", enemyName);
            //Debug.Log("small hp");
        }
        else if (randomNum >= 26 && randomNum <= 27)
        {
            GameObject drops = Instantiate(dropPrefab, deathLocation, Quaternion.identity);
            Drops dropScript = drops.GetComponent<Drops>();
            dropScript.SetUpItem("Small MP", enemyName);
            //Debug.Log("small mp");
        }
        else if (randomNum == 28)//&& randomNum <= 28)
        {
            GameObject drops = Instantiate(dropPrefab, deathLocation, Quaternion.identity);
            Drops dropScript = drops.GetComponent<Drops>();
            dropScript.SetUpItem("Large HP", enemyName);
            //Debug.Log("large hp");
        }
        else if (randomNum == 29)// && randomNum <= 30)
        {
            GameObject drops = Instantiate(dropPrefab, deathLocation, Quaternion.identity);
            Drops dropScript = drops.GetComponent<Drops>();
            dropScript.SetUpItem("Large MP", enemyName);
            //Debug.Log("large mp");
        }
        else if (randomNum == 30)
        {
            GameObject drops = Instantiate(dropPrefab, deathLocation, Quaternion.identity);
            Drops dropScript = drops.GetComponent<Drops>();
            dropScript.SetUpItem("Item", enemyName);
            //Debug.Log("item");
        }
    }

    public void SpecificDrop(Vector3 dropLocation, string item)
    {

        GameObject drops = Instantiate(dropPrefab, dropLocation, Quaternion.identity);
        Drops dropScript = drops.GetComponent<Drops>();
        dropScript.SetUpItem(item, "");
    }
}
