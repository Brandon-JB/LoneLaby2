using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour
{
    EnemyScript enemyAI;
    BaseChar enemyChar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Hitbox" && collision.tag == "Enemy")
        {
            //Debug.Log(collision.gameObject.name);

            enemyChar = collision.GetComponent<BaseChar>();

            if (!enemyChar.allied)
            {
                enemyChar.stunTimer.cooldownTime = 4;
                enemyChar.stunTimer.StartCooldown();
            }
        }

        /*
        enemyAI = collision.gameObject.GetComponent<EnemyScript>();

        if (enemyAI != null)
        {
            //Debug.Log("BLINDED HELP");
            enemyAI.canMove = false;

            //Puts the enemy in attack cooldown, which essentially stuns them.
            enemyAI.StartCooldown();
        }*/
    }
}
