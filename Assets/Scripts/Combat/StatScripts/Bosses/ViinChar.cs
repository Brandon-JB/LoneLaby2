using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViinChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Viin";
        allied = false;

        ChangeStats(14, 0, 4, 150, 0);
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
        //put whatever code to trigger the end of boss fight things
        SceneManager.LoadScene("Overworld");
        Destroy(this.gameObject);
    }
}
