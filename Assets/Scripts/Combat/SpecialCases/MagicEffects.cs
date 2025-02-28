using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffects : MonoBehaviour
{
    EnemyScript enemyAI;
    BaseChar enemyChar;
    [SerializeField] private LeoraChar2 leoraChar;
    public string magicType = "";

    private void Start()
    {
        //leoraChar = GetComponent<LeoraChar2>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Hitbox" && collision.tag == "Enemy")
        {
            enemyChar = collision.GetComponent<BaseChar>();

            enemyChar.GotDamaged(leoraChar.statsSheet["MagAttack"], enemyChar.gameObject, 0);
            enemyChar.TriggerHurtAnim();

            if (magicType == "lightMag")
            {
                //Debug.Log(collision.gameObject.name);

                if (!enemyChar.allied)
                {
                    enemyChar.stunTimer.cooldownTime = 4;
                    enemyChar.stunTimer.StartCooldown();
                    enemyChar.SpawnParticle("stunFX", collision.transform.position, collision.transform, enemyChar.stunTimer.cooldownTime);
                }
            }
            else
            {
                Debug.Log("No magic type");
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
