using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class BaseChar : MonoBehaviour
{
    public Dictionary<string, int> statsSheet = new Dictionary<string, int>()
    {
        {"Strength", 14},
        {"MagAttack", 10},
        {"Defense", 4},
        {"Health", 50},
        {"MaxHealth", 50},
        {"Mana", 4},
        {"MaxMana", 4}
    };

    public void ChangeStats(int strength, int magATK, int Defense, int HP, int MP)
    {
        statsSheet["Strength"] = strength;
        statsSheet["MagAttack"] = magATK;
        statsSheet["Defense"] = Defense;
        statsSheet["Health"] = HP;
        statsSheet["MaxHealth"] = HP;
        statsSheet["Mana"] = MP;
        statsSheet["MaxMana"] = MP;
    }

    public void ChangeSpecificStat(string stat, int newValue)
    {
        bool atMaxHealth = false;
        bool atMaxMana = false;
        float statPercent = 0;

        if (stat == "MaxHealth")
        {
            if (GetHealth() == GetMaxHealth())
            {
                atMaxHealth = true;
            }
            else
            {
                statPercent = (float)GetHealth() / (float)GetMaxHealth();
            }
        }

        if (stat == "MaxMana")
        {
            if (GetMana() == GetMaxMana())
            {
                atMaxMana = true;
            }
            else
            {
                statPercent = (float)GetMana() / (float)GetMaxMana();
            }
        }

        //if the stat being changed is not health for mana
        if ((stat != "MaxHealth" && stat != "MaxMana") || (atMaxHealth || atMaxMana))
        {
            statsSheet[stat] = newValue;
        }
        else
        {
            //If the stat needs to be changed to a percentage of what it originally was
            if (statPercent != 0)
            {
                statsSheet[stat] = newValue;

                if (stat == "MaxHealth")
                {
                    //Only changes the players health to be a percentage of their max health if they lose max health while not being at max health
                    if (statsSheet[stat] < GetHealth())
                    {
                        //Debug.Log("Stat percent: " + statPercent);
                        statsSheet["Health"] = (int)((float)newValue * statPercent);
                    }
                }
                else if (stat == "MaxMana")
                {
                    //Only changes the players health to be a percentage of their max mana if they lose max mana while not being at max mana
                    if (statsSheet[stat] < GetMana())
                    {
                        statsSheet["Mana"] = (int)((float)newValue * statPercent);
                    }
                }
            }
        }


        if (GetHealth() > GetMaxHealth() || atMaxHealth)
        {
            SetMaxHealth();
        }

        if (GetMana() > GetMaxMana() || atMaxMana)
        {
            SetMaxMana();
        }

        if (allied)
        {
            if (stat == "MaxHealth")
            {
                healthBar.text = GetHealth() + "/" + GetMaxHealth();
                hpSlider.value = ((float)GetHealth()) / GetMaxHealth();
            }
            else if (stat == "MaxMana")
            {
                manaBar.text = GetMana() + "/" + GetMaxMana();
                mpSlider.value = ((float)GetMana()) / GetMaxMana();
            }
        }
    }

    public int GetSpecificStat(string stat)
    {
        return statsSheet[stat];
    }

    public void AddToSpecificStat(string stat, int addedValue)
    {
        ChangeSpecificStat(stat, GetSpecificStat(stat) + addedValue);
    }

    public string charName = "";

    public bool allied = true;

    public Animator animator = null;

    private DropManager dropManager;

    private LeoraChar2 leoraChar2;

    [Header("UI")]
    //Only for use with Leora. Kept here so there's only one damage script
    [SerializeField] public TMP_Text healthBar;
    [SerializeField] public TMP_Text manaBar;
    [SerializeField] Slider hpSlider;
    [SerializeField] public Slider mpSlider;

    [Header("Knockback")]
    public Rigidbody2D charRB;
    [SerializeField] private Vector2 knockbackDirection = Vector2.zero;
    private float strength = 25f;

    [SerializeField] private GameObject hitboxChild = null;
    [SerializeField] private HitboxChar hbChildScript;

    [SerializeField] public Transform damagePopup;
    



    [Header("Parrying")]
    public bool isParrying;
    public bool isPerfectParrying;

    public GameObject ParryIndicator;
    [SerializeField] private SpriteRenderer testParrySprite;
    [SerializeField] public Cooldown parryCooldown = new Cooldown();

    [SerializeField] public Cooldown stunTimer = new Cooldown();

    [Header("MagicEffects")]
    [SerializeField] private MagicParticles particleManager;

    public Cooldown slowTimer;


    public virtual void Update()
    {
       if (!slowTimer.isCoolingDown)
       {
            animator.speed = 1f;
       }
    }


    #region Stats

    protected void SetMaxHealth()
    {
        statsSheet["Health"] = GetMaxHealth();
    }

    protected void SetMaxMana()
    {
        statsSheet["Mana"] = GetMaxMana();
    }

    public int GetHealth()
    {
        return statsSheet["Health"];
    }

    public int GetMaxHealth()
    {
        return statsSheet["MaxHealth"];
    }

    protected int GetMaxMana()
    {
        return statsSheet["MaxMana"];
    }

    public void SetHealth(int health)
    {
        statsSheet["Health"] = health;

        if (GetHealth() > GetMaxHealth())
        {
            SetMaxHealth();
        }

        if (GetHealth() < 0)
        {
            statsSheet["Health"] = 0;
        }

        if (allied)
        {
            healthBar.text = GetHealth() + "/" + GetMaxHealth();
            hpSlider.value = ((float)GetHealth()) / GetMaxHealth();
        }

        
    }

    public void Heal(int healAmount)
    {
        SetHealth(GetHealth() + healAmount);

        Transform healPopUpTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
        DamagePopUp damPopScript = healPopUpTransform.GetComponent<DamagePopUp>();
        damPopScript.SetupInt(healAmount, "Health");
    }

    public int GetMana()
    {
        return statsSheet["Mana"];
    }

    public void SetMana(int mana)
    {
        statsSheet["Mana"] = mana;

        if (GetMana() > GetMaxMana())
        {
            SetMaxMana();
        }

        if (GetMana() < 0)
        {
            statsSheet["Mana"] = 0;
        }

        if (allied)
        {
            manaBar.text = GetMana() + "/" + GetMaxMana();
            mpSlider.value = ((float)GetMana()) / GetMaxMana();
        }
      
    }

    public void RestoreMana(int restoreAmount)
    {
        SetMana(GetMana() + restoreAmount);

        Transform manaPopUpTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
        DamagePopUp damPopScript = manaPopUpTransform.GetComponent<DamagePopUp>();
        damPopScript.SetupInt(restoreAmount, "Mana");
    }

    #endregion

    #region Animator

    public void TriggerAttackAnim()
    {
        animator.SetBool("Attacking", true);
    }

    public void StopAttackAnim()
    {
        animator.SetBool("Attacking", false);
    }

    public virtual void TriggerHurtAnim()
    {
        animator.SetBool("Hurt", true);
        animator.SetBool("Attacking", false);
        DisableHitbox();

        if (allied)
        {
            ParryIndicator.SetActive(false);
            animator.SetBool("Parrying", false);
            animator.SetBool("Magicing", false);
            animator.SetBool("isInCombo", false);
        }
    }

    public virtual void StopHurtAnim()
    {
        animator.SetBool("Hurt", false);
        DisableHitbox();

        if (allied)
        {
            ParryIndicator.SetActive(false);
        }
    }

    private bool isAttacking()
    {
        return animator.GetBool("Attacking");
    }

    public void TriggerParryAnim()
    {
        animator.SetBool("Parrying", true);
    }

    public void StopParryAnim()
    {
        animator.SetBool("Parrying", false);
        parryCooldown.StartCooldown();
    }

    public void SpawnParticle(string particleName, Vector3 position, Transform parentTransform, float lifetime)
    {
        MagicParticles particle = Instantiate(particleManager, position, Quaternion.identity, parentTransform);
        particle.lifetime.cooldownTime = lifetime;
        particle.lifetime.StartCooldown();
        particle.animator.SetBool(particleName, true);
        particle.startedAnimating = true;
    }

    #endregion

    #region Getting hit and Also TriggerEnter

    #region Parrying

    public void StartParryWindow()
    {
        //testParrySprite.color = Color.yellow;
        ParryIndicator.SetActive(true);
        isParrying = true;
        //Debug.Log("Parry Window");
    }

    public void EndParryWindow()
    {
        isParrying = false;
        ParryIndicator.SetActive(false);
    }

    public void StartPerfectParryWindow()
    {
        //testParrySprite.color = Color.green;
        //ParryIndicator.SetActive(true);
        isPerfectParrying = true;
        //Debug.Log("Perfect Parry Window");
    }

    public void EndPerfectParryWindow()
    {
        //ParryIndicator.SetActive(false);
        isPerfectParrying = false;
        //testParrySprite.color = Color.yellow;
    }

    #endregion

    public void EnableHitbox()
    {
        if (hbChildScript.alreadyHit == false)
        {
            hitboxChild.SetActive(true);
        }
    }

    public void DisableHitbox()
    {
        hitboxChild.SetActive(false);
        hbChildScript.alreadyHit = false;
    }

    public void ResetHitbox()
    {
        hbChildScript.alreadyHit = false;
        hitboxChild.SetActive(true);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Hitbox" && ( this.tag == "Enemy" || this.tag == "Boss" ) || this.tag == "Player")
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
                        
                        /*//Crit possible from sophie amulet
                         *Moved it to gotDamaged
                        if (leoraChar.sophieAmuletActive)
                        {
                            int critOrNo = Random.Range(1, 21);

                            //critOrNo = 20;

                            if (critOrNo == 20)
                            {
                                incomingDamage = incomingDamage * 2;
                            }

                            Vector3 critPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                            Transform damagePopupTransform = Instantiate(damagePopup, critPosition, Quaternion.identity);
                            DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
                            damPopScript.SetupString("Critical!");
                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 2);
                            TriggerHurtAnim();
                        }
                        else
                        */
                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                        TriggerHurtAnim();
                        

                        /* Parrying moved to Leora
                        if (hitboxChild.isParryable)
                        {
                            if (isPerfectParrying)
                            {
                                //Debug.Log("Perfect Parry");
                                GotDamaged(incomingDamage / 10, otherCharTrigger.gameObject, 0);
                                otherCharTrigger.TriggerHurtAnim();
                                //Debug.Log(otherCharTrigger.gameObject.name);
                                otherCharTrigger.stunTimer.cooldownTime = 2f;
                                otherCharTrigger.stunTimer.StartCooldown();
                                otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            }
                            else if (isParrying)
                            {
                                //Debug.Log("Parry");
                                GotDamaged(incomingDamage / 2, otherCharTrigger.gameObject, 0.5f);
                                otherCharTrigger.TriggerHurtAnim();
                                otherCharTrigger.stunTimer.cooldownTime = 1f;
                                otherCharTrigger.stunTimer.StartCooldown();
                                otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
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
                        }*/
                    }
                }
            }
        }
    }

    public void GotDamaged(int incomingDamage, GameObject otherAttacker, float stMod)
    {

        //Debug.Log(charName + " Health: " + GetHealth());
        if (GetHealth() > 0)
        {
            //Crit possible from sophie amulet
            if (!allied && leoraChar2.sophieAmuletActive)
            {
                int critOrNo = Random.Range(1, 21);

                //critOrNo = 20;

                if (critOrNo == 20)
                {
                    incomingDamage = incomingDamage * 2;

                    Vector3 critPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    Transform critTransform = Instantiate(damagePopup, critPosition, Quaternion.identity);
                    DamagePopUp critPopUpScript = critTransform.GetComponent<DamagePopUp>();
                    critPopUpScript.SetupString("Critical!");
                }

                
            }

            if (incomingDamage < 0)
            {
                incomingDamage = 0;
            }


            SetHealth(GetHealth() - incomingDamage);
            Transform damagePopupTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
            DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
            damPopScript.SetupInt(incomingDamage, "Damage");
            //Debug.Log(charName + " After damage health: " + GetHealth());

            if (this.tag != "Boss")
            {
                StartCoroutine(Knockback(otherAttacker, stMod));
            }

            if (GetHealth() <= 0)
            {
                Death();
            }
        }

    }

    public virtual IEnumerator Knockback(GameObject otherAttacker, float stMod)
    {
        EnemyScript enemyMovement = null;
        CombatPlayerMovement playerMovement = null;

        if (!allied)
        {
            enemyMovement = this.GetComponent<EnemyScript>();
        }
        else
        {
            playerMovement = this.GetComponent<CombatPlayerMovement>();
        }

        knockbackDirection = (transform.position - otherAttacker.transform.position).normalized;

        if (!allied)
        {
            if (enemyMovement != null)
            {
                enemyMovement.canMove = false;
            }
            //Knockback strength is multiplied due to enemies having much more mass
            charRB.AddForce(knockbackDirection * ((stMod * strength) * 10000f), ForceMode2D.Impulse);
            //Debug.Log("Launch enemy");
        }
        else
        {
            playerMovement.canMove = false;
            charRB.AddForce(knockbackDirection * (stMod * strength), ForceMode2D.Impulse);
        }

        //charRB.AddForce(knockbackDirection * strength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f);

        if (GetHealth() > 0)
        {
            if (!allied)
            {
                if (enemyMovement != null)
                {
                    enemyMovement.cooldown.Interupted();
                    enemyMovement.canMove = true;
                }
                charRB.velocity = Vector3.zero;
            }
            else
            {
                playerMovement.canMove = true;
            }
        }
    }

    public virtual void Death()
    {
        //SceneManager.LoadScene("NoCombatAreas");
        dropManager.RandomizedDrops(this.transform.position, this.charName);
        Destroy(this.gameObject);
    }

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        charRB = GetComponent<Rigidbody2D>();
        isParrying = false;
        isPerfectParrying = false;

        leoraChar2 = FindObjectOfType<LeoraChar2>();

        //stunTimer.cooldownTime = 1;
        slowTimer.cooldownTime = 6;

        dropManager = FindObjectOfType<DropManager>();

        //Physics2D.IgnoreCollision();
    }
}
