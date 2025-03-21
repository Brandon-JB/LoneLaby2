using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCrystalScript : MonoBehaviour
{

    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {

        }
    }

    public void DestroyCrystal()
    {
        Destroy(this.gameObject);
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

            //if the player hits the crystal
            if (otherCharTrigger.allied == true)
            {
                health--;
            }
        }
    }
}
