using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucanSoundTrigger : MonoBehaviour
{
    [SerializeField] static private Cooldown soundCooldown = new Cooldown();

    // Start is called before the first frame update
    void Start()
    {
        soundCooldown.cooldownTime = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox" && !soundCooldown.isCoolingDown)
        {
            HitboxChar hitboxChar = collision.GetComponent<HitboxChar>();

            if (hitboxChar.parentChar.charName == "Lucan" || hitboxChar.parentChar.charName == "Lucora")
            {
                soundCooldown.StartCooldown();

                audioManager.Instance.playSFX(66);
            }
        }
    }
}
