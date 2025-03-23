using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCrystalScript : MonoBehaviour
{
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //The Viin script needs to be assigned manually in the editor.


    public int health = 4;

    public Animator animator;

    public bool isShielded;

    public BoxCollider2D hurtbox;

    public ViinScript viinScript;

    public Animator bloodShieldAnimator;

    public Cooldown noShieldTimer;

    private DropManager dropManager;

    // Start is called before the first frame update
    void Start()
    {
        dropManager = FindObjectOfType<DropManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            animator.SetBool("Death", true);
        }

        if (!animator.GetBool("Death") && !isShielded && !noShieldTimer.isCoolingDown)
        {
            bloodShieldAnimator.SetBool("despawn", false);
            bloodShieldAnimator.SetBool("spawn", true);
            isShielded = true;
        }
    }

    public void RemoveCrystalBuffs()
    {
        viinScript.viinChar.AddToSpecificStat("Strength", -2);
        viinScript.attackCooldown.cooldownTime += 0.5f;
        viinScript.attackLimit -= 5;
    }

    public void EnableHurtBox()
    {
        isShielded = true;
        bloodShieldAnimator.SetBool("spawn", false);
        hurtbox.enabled = true;
    }

    public void DisableHurtbox()
    {
        hurtbox.enabled = false;
    }

    public void TriggerDespawnAnimation()
    {
        animator.SetBool("despawning", true);
        bloodShieldAnimator.SetBool("despawn", true);
    }

    public void StopDespawnAnimation()
    {
        animator.SetBool("despawning", false);
        bloodShieldAnimator.SetBool("despawn", false);
        RemoveCrystalBuffs();
    }

    public void StopSpawnAnimation()
    {
        animator.SetBool("spawning", false);
    }

    public void StartSpawnAnimation()
    {
        animator.SetBool("spawning", true);
    }

    public void SpawnShield()
    {
        bloodShieldAnimator.SetBool("spawn", true);
        bloodShieldAnimator.SetBool("despawn", false);
    }


    public void DespawnShield()
    {
        bloodShieldAnimator.SetBool("despawn", true);
        bloodShieldAnimator.SetBool("spawn", false);
    }

    public void DestroyCrystal()
    {
        dropManager.SpecificDrop(this.transform.position, "Small HP");
        
        viinScript.MarkAsDestroyed();
        viinScript.DespawnBloodOrbs();
        RemoveCrystalBuffs();

        viinScript.WarningObject.SetActive(false);
        viinScript.viinChar.DisableHitbox();

        //viinScript.transform.position = new Vector2((viinScript.bottomLeftArenaBounds.x + viinScript.topRightArenaBounds.x) / 2, (viinScript.bottomLeftArenaBounds.y + viinScript.topRightArenaBounds.y) / 2);
        viinScript.viinChar.StopAttackAnim();
        viinScript.viinChar.animator.SetBool("stunned", true);
        viinScript.AttackCount = 0;
        viinScript.viinChar.animator.SetBool("shortStun", false);
    }

    public void StopHurtAnim()
    {
        animator.SetBool("hurt", false);
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

            //if the player hits the crystal while the crystal is not shielded
            if (otherCharTrigger.allied == true && isShielded != true)
            {
                health--;
                Transform damagePopupTransform = Instantiate(viinScript.viinChar.damagePopup, transform.position, Quaternion.identity);
                DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
                damPopScript.SetupInt(otherCharTrigger.GetSpecificStat("Strength"), "Damage");
                animator.SetBool("hurt", true);
            }

            //if Viin hits the shield
            if (otherCharTrigger.charName == "Viin" && isShielded)
            {
                DespawnShield();
                isShielded = false;
                noShieldTimer.StartCooldown();
            }
        }
    }
}
