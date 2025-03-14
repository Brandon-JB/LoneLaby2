using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContact : MonoBehaviour
{
    private Rigidbody2D rb;
    public PortalScript portalScript;
    public Spawner spawner;
    private string Location;

    private int randomNumber;
    public int spawnNumber = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            portalScript.TeleportPlayer(collision.name);
        }

        if (collision.tag == "Cave")
        {
            Location = "Cave";

            randomNumber = Random.Range(1, 10);
            if(randomNumber <= spawnNumber)
            {
                spawner.SpawnObject(Location);
            }

        }

    }
}
