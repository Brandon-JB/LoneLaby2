using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucanChar : BaseChar
{
    [SerializeField] private GameObject killSpareMenu;

    [SerializeField] private LucanScript lucanScript;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Lucan";
        allied = false;

        ChangeStats(15, 0, 6, 430, 0);
    }

    

    public override void OnTriggerEnter2D(Collider2D collision)
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
                if (otherCharTrigger.allied != this.allied)
                {
                    hitboxChild.alreadyHit = true;
                    collision.gameObject.SetActive(false);

                    int incomingDamage = otherCharTrigger.statsSheet["Strength"] - statsSheet["Defense"];

                    LeoraChar2 leoraChar = otherCharTrigger.GetComponent<LeoraChar2>();

                    GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);

                }
            }
        }

    }

    public override void GotDamaged(int incomingDamage, GameObject otherAttacker, float stMod)
    {
        base.GotDamaged(incomingDamage, otherAttacker, stMod);

        if (!animator.GetBool("stunned"))
        {
            lucanScript.dmgTaken += incomingDamage;
        }
    }

    public override void Death()
    {
        //if done final attack yet
        if (lucanScript.inFinalPhase)
        {
            audioManager.Instance.playSFX(18);

            mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("LucanQuest/cave_postfight");
            //I don't think I have to do anything else for this, but I can modify this.

            //killSpareMenu.SetActive(true);
            //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
            //killSpare.bossName = "Lucan";
            Destroy(this.gameObject);
        }
        {
            //Prevents boss from dying if they haven't done their final move yet
            SetHealth(Mathf.Clamp(GetHealth(), 1, GetMaxHealth()));
        }
    }
}
