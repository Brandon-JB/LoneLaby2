using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class PlayerContact : MonoBehaviour
{
    private Rigidbody2D rb;
    public PortalScript portalScript;
    public CityPortalManager cityPortalManager;
    public RoomTeleport roomTP;
    public Spawner spawner;
    private string Location;
    public LevelLoader levelLoader;

    private int randomNumber;
    public int spawnNumber = 1;

    public Animator animator;
    public string[] SceneNames;

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

        if (collision.tag == "CityPortals")
        {
            cityPortalManager.LoadCityArea(collision.name);
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

        if (collision.tag == "Room")
        {
            roomTP.Teleport(collision.name);
        }

        if (collision.tag == "OverworldEnemy")
        {
            if (collision.name == "Slime")
            {
                PlayerMovement.CanWalk = false;
                PortalScript.LastPortal = 5;
                StartCoroutine(LoadRandomFight());
            }
            else if(collision.name == "Ghost")
            {

            }
            else if (collision.name == "Sword")
            {

            }
        }
    }

    IEnumerator LoadRandomFight()
    {
        animator.SetBool("IsFight", true);
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene("RandomFight");
        //Call Static Int for where to fight enemy and while enemies
        PlayerMovement.CanWalk = true;
    }
}
