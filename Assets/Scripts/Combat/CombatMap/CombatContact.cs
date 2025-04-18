using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatContact : MonoBehaviour
{

    public CombatPortals combatPortals;
    public MansionPortals mansionPortals;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            Debug.Log("colliding");
            combatPortals.TeleportPlayer(collision.name);
        }

        if (collision.tag == "Chest" && InputManager.interactPressed == true)
        {
            //Open Chest
        }

        if (collision.tag == "Stairs")
        {
            mansionPortals.MansionStairs(collision.name);
        }
    }
}
