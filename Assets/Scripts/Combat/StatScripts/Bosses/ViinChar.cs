using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViinChar : BaseChar
{

    [SerializeField] private GameObject killSpareMenu;
    [SerializeField] private ViinScript viinScript;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Viin";
        allied = false;

        ChangeStats(14, 0, 4, 320, 0);
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

    public override void Death()
    {
        if (viinScript.thirdCrystalsSpawned)
        {
            audioManager.Instance.playSFX(18);

            //put whatever code to trigger the end of boss fight things
            //SceneManager.LoadScene("Overworld");


            mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("ViinQuest/veinwood_postfight");
            //I don't think I have to do anything else for this, but I can modify this.


            //killSpareMenu.SetActive(true);
            //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
            //killSpare.bossName = "Viin";
            Destroy(this.gameObject);
        }
        else
        {
            //Prevents boss from dying if they haven't done their final move yet
            SetHealth(Mathf.Clamp(GetHealth(), 1, GetMaxHealth()));
        }
    }

    public override void GotDamaged(int incomingDamage, GameObject otherAttacker, float stMod)
    {
        base.GotDamaged(incomingDamage, otherAttacker, stMod);

        if (!viinScript.firstCrystalsSpawned)
        {
            SetHealth(Mathf.Clamp(GetHealth(), GetMaxHealth() - (GetMaxHealth() / 4), GetMaxHealth()));
        }
        else if (!viinScript.secondCrystalsSpawned)
        {
            SetHealth(Mathf.Clamp(GetHealth(), GetMaxHealth() / 2, GetMaxHealth()));
        }
    }
}
