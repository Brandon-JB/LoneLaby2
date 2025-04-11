using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLeoraChar : BaseChar
{

    //Variable for the combo window.
    //If the combo timer is "cooling down" then that just means it's in the combo window.
    [SerializeField] public Cooldown comboTimer = new Cooldown();

    //Variable for the cooldown time.
    [SerializeField] public Cooldown attackCooldown = new Cooldown();

    [SerializeField] public Cooldown magicCooldown = new Cooldown();

    [SerializeField] private GameOverAndUI gameOverManager;

    [SerializeField] private CombatPlayerMovement playerMovement;

    [SerializeField] private GameObject magHitbox;

    [SerializeField] private GameObject darkMagFollowup;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private float projectileSpawnOffset;

    public bool secondPhaseActive;

    public string magicType = "";

    //[SerializeField] private BoxCollider2D hurtbox;

    private bool hyperArmor;

    //For Darkness
    private DarknessManager darknessManager;

    //public bool isInFirstCombo = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing Leora
        charName = "DarkLeora";
        allied = false;
        magicType = "darkMag";

        ChangeStats(10, 10, 4, 12, 10);

        animator.SetFloat("LastH", 0);
        animator.SetFloat("LastV", -1);

        magHitbox.SetActive(false);
        darknessManager = GameObject.FindObjectOfType<DarknessManager>();
    }

    public override void TriggerHurtAnim()
    {
        if (!hyperArmor)
        {
            base.TriggerHurtAnim();
            isParrying = false;
            isPerfectParrying = false;
            EnableHurtbox();

            DisableMagHitbox();
        }
    }

    public void ToggleHyperArmor()
    {
        hyperArmor = !hyperArmor;
    }

    public override void StopHurtAnim()
    {
        base.StopHurtAnim();

        if (hyperArmor)
        {
            ToggleHyperArmor();
        }

        isParrying = false;
        isPerfectParrying = false;

        DisableMagHitbox();
    }

    public override void Update()
    {
        base.Update();

        //Once the combo timer ends, then set the combo bool in the animator to false;
        if (!comboTimer.isCoolingDown)
        {
            animator.SetBool("isInCombo", false);
        }

        if (Time.timeScale == 0)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
        }
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
                hitboxChild = collision.GetComponent<HitboxChar>();

                if (hitboxChild == null)
                {
                    hitboxChild = collision.GetComponentInParent<HitboxChar>();
                    Debug.Log("Child not found");
                }

                otherCharTrigger = hitboxChild.parentChar;

                if (otherCharTrigger == null)
                {
                    Debug.Log("Unable to find parent character of hitbox");
                }
            }

            if (otherCharTrigger != null)
            {
                hitboxChild = collision.GetComponent<HitboxChar>();
                LeoraChar2 leoraChar = otherCharTrigger.GetComponent<LeoraChar2>();

                if (otherCharTrigger.allied != this.allied)
                {
                    if (hitboxChild == null)
                    {
                        hitboxChild = collision.GetComponentInParent<HitboxChar>();
                        //Debug.Log("Child not found");
                    }

                    hitboxChild.alreadyHit = true;
                    collision.gameObject.SetActive(false);

                    int incomingDamage = otherCharTrigger.statsSheet["Strength"] - statsSheet["Defense"];

                    if (hitboxChild.isParryable)
                    {
                        if (isPerfectParrying)
                        {
                            //Debug.Log("Perfect Parry");
                            GotDamaged(incomingDamage / 10, otherCharTrigger.gameObject, 0);
                            leoraChar.TriggerHurtAnim();
                            //Debug.Log(otherCharTrigger.gameObject.name);
                            otherCharTrigger.stunTimer.cooldownTime = 1.5f;
                            otherCharTrigger.stunTimer.StartCooldown();
                            otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            leoraChar.stunned = true;
                            if (otherCharTrigger.tag != "Boss")
                            {
                                otherCharTrigger.StartCoroutine(otherCharTrigger.Knockback(this.gameObject, 0));
                            }
                        }
                        else if (isParrying)
                        {
                            //Debug.Log("Parry");
                            GotDamaged(incomingDamage / 2, otherCharTrigger.gameObject, 0.5f);
                            leoraChar.TriggerHurtAnim();
                            otherCharTrigger.stunTimer.cooldownTime = 1f;
                            otherCharTrigger.stunTimer.StartCooldown();
                            otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            leoraChar.stunned = true;
                            if (otherCharTrigger.tag != "Boss")
                            {
                                //Debug.Log("Parry");
                                otherCharTrigger.StartCoroutine(otherCharTrigger.Knockback(this.gameObject, 0));
                            }
                        }
                        else
                        {
                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                            TriggerHurtAnim();
                        }
                        
                    }
                    else
                    {
                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                        TriggerHurtAnim();
                    }
                }
            }
        }
    }

    

    public override IEnumerator Knockback(GameObject otherAttacker, float stMod)
    {
        if (hyperArmor)
        {
            stMod = 0;
        }

        return base.Knockback(otherAttacker, stMod);
    }


    //A event to be called in the animator that starts both cooldownsa and preps the animator for whether the user continues the combo or not.
    public void StartComboTimer()
    {
        animator.SetBool("isInCombo", true);
        animator.SetBool("Attacking", false);
        comboTimer.StartCooldown();
        attackCooldown.StartCooldown();
    }

    public override void Death()
    {
        charRB.constraints = RigidbodyConstraints2D.FreezeAll;
        charRB.velocity = Vector2.zero;
        playerMovement.canMove = false;
        animator.SetBool("Death", true);
        gameOverManager.FadeOutUI();
        ParryIndicator.SetActive(false);
        isParrying = false;
        isPerfectParrying = false;
        gameOverManager.StartGameOverHueShift();
        audioManager.Instance.playBGM("T2");
    }

    //used for invincibility
    public void DisableHurtbox()
    {
        hurtbox.enabled = false;
    }

    public void EnableHurtbox()
    {
        hurtbox.enabled = true;
    }

    public void GameOver()
    {
        gameOverManager.OnDeath();
    }

    //An event to be called in the animator that goes at the end of the combo
    public void endCombo()
    {
        attackCooldown.StartCooldown();
        animator.SetBool("Attacking", false);
    }
    public void DoNextCombo()
    {
        animator.SetBool("Attacking", true);
    }

    public void MagAttack()
    {
        animator.SetBool("Magicing", true);

        MagicEffects magEffects = magHitbox.GetComponent<MagicEffects>();

        magEffects.magicType = magicType;

        SetMana(GetMana() - 2);
    }

    public void EnableMagHitbox()
    {
        magHitbox.SetActive(true);
        if (darknessManager != null && magicType == "lightMag")
        {
            darknessManager.turnoffDarkness();
        }
    }

    public void DisableMagHitbox()
    {
        magHitbox.SetActive(false);
        //Debug.Log("Disable mag hitbox");
    }

    public void EndMagick()
    {
        animator.SetBool("Magicing", false);

        ToggleHyperArmor();

        if (magicType == "darkMag")
        {
            GameObject tempMagPart = Instantiate(darkMagFollowup, this.transform.position, Quaternion.identity);

            EvilDarkMagAoE tempMagManager = tempMagPart.GetComponent<EvilDarkMagAoE>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool("darkFollowup", true);

            if (secondPhaseActive)
            {
                Vector2 firstProjSpawn = new Vector2(projectileSpawnPoint.transform.position.x + projectileSpawnOffset, projectileSpawnPoint.transform.position.y);
                Vector2 secondProjSpawn = new Vector2(projectileSpawnPoint.transform.position.x - projectileSpawnOffset, projectileSpawnPoint.transform.position.y);

                GameObject projectile = Instantiate(projectilePrefab, firstProjSpawn, Quaternion.identity);
                IvarProjectile projScript = projectile.GetComponent<IvarProjectile>();
                projScript.parentChar = this;

                GameObject secondProjectile = Instantiate(projectilePrefab, secondProjSpawn, Quaternion.identity);
                IvarProjectile secondProjScript = secondProjectile.GetComponent<IvarProjectile>();
                secondProjScript.parentChar = this;
            }
        }
    }
}
