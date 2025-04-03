using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    public void RandomizedDrops(Vector3 deathLocation, string enemyName)
    {
        int randomNum = Random.Range(1, 31);

        randomNum = 30;

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
