using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IvarChar : BaseChar
{
    public IvarScript ivarScript;

    [SerializeField] private GameObject killSpareMenu;

    // Start is called before the first frame update
    void Start()
    {
        allied = false;
        charName = "Ivar";

        ChangeStats(12, 0, 4, 475, 0);
    }

    public override void Update()
    {
        base.Update();

        if (animator.GetBool("stunned") && !stunTimer.isCoolingDown)
        {
            //Debug.Log("Fuck");
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
                            AddToSpecificStat("Health", incomingDamage);
                        }

                        if (ivarScript.firstTeleportHappened && !ivarScript.secondTeleportHappened && stunTimer.isCoolingDown && GetHealth() <= GetMaxHealth() / 2)
                        {
                            FakeGotDamaged(incomingDamage);
                            SetHealth(Mathf.Clamp(GetHealth(), GetMaxHealth() / 2, GetMaxHealth()));
                            Debug.Log("nuh uh, you can't hurt him yet");
                        }
                        else
                        {
                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                        }
                        //TriggerHurtAnim();


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

    public virtual void FakeGotDamaged(int incomingDamage) //to show damage number but nothing happening
    {


            if (incomingDamage < 0)
            {
                incomingDamage = 0;
            }

            GameObject damagePopupTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
            DamagePopUp damPopScript = damagePopupTransform.GetComponentInChildren<DamagePopUp>();
            damPopScript.SetupInt(incomingDamage, "Damage");
            //Debug.Log(charName + " After damage health: " + GetHealth());

            if (allied)
            {
                audioManager.Instance.playSFX(1);
            }
            else
            {
                audioManager.Instance.playSFX(7);
            }

    }

    public override void Death()
    {
        if (ivarScript.secondTeleportHappened)
        {

            audioManager.Instance.playSFX(18);
            //put whatever code to trigger the end of boss fight things
            //SceneManager.LoadScene("Overworld");
            foreach (var enemy in ivarScript.enemyList)
            {
                Destroy(enemy.gameObject);
            }

            mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("IvarQuest/manor_postfight");
            //I don't think I have to do anything else for this, but I can modify this.

            //This opens kill/spare
            //killSpareMenu.SetActive(true);
            //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
            //killSpare.bossName = "Ivar";
            Destroy(this.gameObject);
        }
        else
        {
            //Prevents boss from dying if they haven't done their final move yet
            SetHealth(Mathf.Clamp(GetHealth(), 1, GetMaxHealth()));
        }
    }
}
