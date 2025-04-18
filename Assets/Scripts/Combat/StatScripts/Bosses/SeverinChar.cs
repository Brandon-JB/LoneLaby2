using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinChar : BaseChar
{
    [SerializeField] private SeverinScript sevScript;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Severin";
        allied = false;

        ChangeStats(13, 0, 8, 350, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (animator.GetBool("stunned") && !stunTimer.isCoolingDown)
        {
            animator.SetBool("Attacking", false);
            animator.SetBool("stunned", false);
            hbChildScript.alreadyHit = false;
            sevScript.cooldown.cooldownTime = 1;
            sevScript.cooldown.StartCooldown();
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
                        //if Severin is not charging the massive attack
                        if (/*!animator.GetBool("charging")*/ !sevScript.parrying && !sevScript.screenFlash.activeInHierarchy)
                        {
                            hitboxChild.alreadyHit = true;
                            collision.gameObject.SetActive(false);

                            int incomingDamage = otherCharTrigger.statsSheet["Strength"] - statsSheet["Defense"];

                            LeoraChar2 leoraChar = otherCharTrigger.GetComponent<LeoraChar2>();

                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                            //TriggerHurtAnim();
                        }
                        else
                        {
                            animator.SetBool("endCharge", true);
                        }
                    }
                }
            }
        }
    }

    public override void Death()
    {
        audioManager.Instance.playSFX(18);

        mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
        mdm.dialogueSTART("Endings/Compassion/severin_postfight");
        //I don't think I have to do anything else for this, but I can modify this.

        //killSpareMenu.SetActive(true);
        //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
        //killSpare.bossName = "Lucan";
        Destroy(this.gameObject);
    }
}
