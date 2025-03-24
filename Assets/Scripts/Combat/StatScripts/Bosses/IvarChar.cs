using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IvarChar : BaseChar
{
    public IvarScript ivarScript;

    // Start is called before the first frame update
    void Start()
    {
        allied = false;
        charName = "Ivar";

        ChangeStats(10, 0, 4, 150, 0);
    }

    public override void Update()
    {
        base.Update();

        if (animator.GetBool("stunned") && !stunTimer.isCoolingDown)
        {
            Debug.Log("Fuck");
            ivarScript.StopStunAnim();
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Hitbox" && (this.tag == "Enemy" || this.tag == "Boss") || this.tag == "Player")
        {
            BaseChar otherCharTrigger = null;

            HitboxChar hitboxChild = null;

            //Debug.Log(collision.gameObject.name + " Triggered " + this.gameObject.name);

            if (collision.tag == "Hitbox")
            {
                otherCharTrigger = collision.GetComponent<BaseChar>();

                //Debug.Log("Hitbox triggered");

                if (otherCharTrigger == null)
                {
                    //Debug.Log("Other trigger not found");

                    hitboxChild = collision.GetComponent<HitboxChar>();

                    otherCharTrigger = hitboxChild.parentChar;

                    if (otherCharTrigger == null)
                    {
                        //Debug.Log("Unable to find parent character of hitbox");
                    }
                }

                if (otherCharTrigger != null)
                {
                    if (otherCharTrigger.allied != this.allied || otherCharTrigger.charName == "EarthElement")
                    {
                        hitboxChild.alreadyHit = true;
                        collision.gameObject.SetActive(false);

                        int incomingDamage = otherCharTrigger.statsSheet["Strength"] - statsSheet["Defense"];

                        LeoraChar2 leoraChar = otherCharTrigger.GetComponent<LeoraChar2>();

                        if (ivarScript.bigCasting)
                        {
                            ivarScript.damageTaken += incomingDamage;
                        }

                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                        TriggerHurtAnim();


                        /* Parrying moved to Leora
                        if (hitboxChild.isParryable)
                        {
                            if (isPerfectParrying)
                            {
                                //Debug.Log("Perfect Parry");
                                GotDamaged(incomingDamage / 10, otherCharTrigger.gameObject, 0);
                                otherCharTrigger.TriggerHurtAnim();
                                //Debug.Log(otherCharTrigger.gameObject.name);
                                otherCharTrigger.stunTimer.cooldownTime = 2f;
                                otherCharTrigger.stunTimer.StartCooldown();
                                otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            }
                            else if (isParrying)
                            {
                                //Debug.Log("Parry");
                                GotDamaged(incomingDamage / 2, otherCharTrigger.gameObject, 0.5f);
                                otherCharTrigger.TriggerHurtAnim();
                                otherCharTrigger.stunTimer.cooldownTime = 1f;
                                otherCharTrigger.stunTimer.StartCooldown();
                                otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            }
                            else
                            {
                                GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                                TriggerHurtAnim();
                            }
                        }
                        else
                        {
                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                            TriggerHurtAnim();
                        }*/
                    }
                }
            }
        }
    }

    public override void Death()
    {
        //put whatever code to trigger the end of boss fight things
        Destroy(this.gameObject);
    }
}
