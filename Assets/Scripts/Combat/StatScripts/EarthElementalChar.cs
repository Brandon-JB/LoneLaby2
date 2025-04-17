using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElementalChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "EarthElement";
        allied = false;

        ChangeStats(20, 99999, 99999, 99999, 99999);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            Transform damagePopupTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
            DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
            damPopScript.SetupInt(0, "Damage");
            audioManager.Instance.playSFX(7);
        }
    }

    public override void Death()
    {
        QuestManager.AddProgress("Alan", 1);
        dropManager.RandomizedDrops(this.transform.position, this.charName);
        Destroy(this.gameObject);
    }
}
