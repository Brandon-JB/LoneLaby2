using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (collision.tag != "Hitbox" && ( collision.tag == "Enemy" || collision.tag == "Boss" ))
        {
            enemyChar = collision.GetComponent<BaseChar>();

            enemyChar.GotDamaged(leoraChar.statsSheet["MagAttack"], enemyChar.gameObject, 0);
            enemyChar.TriggerHurtAnim();

            if (magicType == "lightMag")
            {
                //Debug.Log(collision.gameObject.name);

                if (!enemyChar.allied || collision.tag != "Boss")
                {
                    enemyChar.stunTimer.cooldownTime = 3;
                    enemyChar.stunTimer.StartCooldown();
                    enemyChar.SpawnParticle("stunFX", collision.transform.position, collision.transform, enemyChar.stunTimer.cooldownTime);
                }
                else if (collision.tag == "Boss")
                {
                    /*enemyChar.stunTimer.cooldownTime = 1.5f;
                    enemyChar.stunTimer.StartCooldown();
                    enemyChar.SpawnParticle("stunFX", collision.transform.position, collision.transform, enemyChar.stunTimer.cooldownTime);*/
                }
            }
            else if (magicType == "bloodMag")
            {
                leoraChar.Heal(5);
            }
            else if (magicType == "mindMag")
            {
                if (collision.tag != "Boss")
                {
                    enemyChar.slowTimer.StartCooldown();
                    enemyChar.animator.speed = 0.5f;
                }
                else
                {
                    enemyChar.slowTimer.cooldownTime = 3;
                    enemyChar.slowTimer.StartCooldown();
                    enemyChar.animator.speed = 0.5f;
                }
            }
            else if (magicType == "darkMag")
            {
                //Debug.Log("Do nothing");
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
