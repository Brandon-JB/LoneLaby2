using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Severin";
        allied = false;

        ChangeStats(16, 0, 8, 300, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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

                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                        //TriggerHurtAnim();
                    }
                }
            }
        }
    }
}
