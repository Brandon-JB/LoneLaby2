using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour
{
    EnemyScript enemyAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAI = collision.gameObject.GetComponent<EnemyScript>();

        if (enemyAI != null)
        {
            //Debug.Log("BLINDED HELP");
            enemyAI.canMove = false;

            //Puts the enemy in attack cooldown, which essentially stuns them.
            enemyAI.cooldown.StartCooldown();
        }
    }
}
