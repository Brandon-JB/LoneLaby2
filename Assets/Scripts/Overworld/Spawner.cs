using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CaveSpawn;
    public GameObject MansionSpawn;
    public GameObject ForestSpawn;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Set the width & height of the area


    public void SpawnObject(string Location)
    {
        // Generate a random position within the defined area
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(randomX, randomY);

        // Instantiate the prefab
        if(Location == "Cave")
        {
            Instantiate(CaveSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
