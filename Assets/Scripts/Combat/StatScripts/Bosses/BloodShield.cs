using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShield : MonoBehaviour
{
    public BloodCrystalScript bloodCrystal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            BaseChar otherCharTrigger;

            HitboxChar hitboxChild;

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

            //if Viin hits the shield
            if (otherCharTrigger.charName == "Viin" && bloodCrystal.isShielded)
            {
                bloodCrystal.DespawnShield();
                bloodCrystal.isShielded = false;
                bloodCrystal.noShieldTimer.StartCooldown();
            }
        }
    }
}
