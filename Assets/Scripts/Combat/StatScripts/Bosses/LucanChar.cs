using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucanChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Lucan";
        allied = false;

        ChangeStats(10, 0, 4, 100, 0);
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

                    GotDamaged(incomingDamage, otherCharTrigger.gameObject, 0);
                }
            }
        }

    }
}
