using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViinChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Viin";
        allied = false;

        ChangeStats(14, 0, 4, 100, 0);
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

                    //Crit possible from sophie amulet
                    if (leoraChar.sophieAmuletActive)
                    {
                        int critOrNo = Random.Range(1, 21);

                        critOrNo = 20;

                        if (critOrNo == 20)
                        {
                            incomingDamage = incomingDamage * 2;
                        }

                        Vector3 critPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                        Transform damagePopupTransform = Instantiate(damagePopup, critPosition, Quaternion.identity);
                        DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
                        damPopScript.SetupString("Critical!");
                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                    }
                    else
                    {
                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                    }
                }
            }
        }

    }
}
