using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseChar : MonoBehaviour
{
    public Dictionary<string, int> statsSheet = new Dictionary<string, int>()
    {
        {"Strength", 10},
        {"Defense", 4},
        {"Health", 50},
        {"MaxHealth", 50},
        {"Mana", 4},
        {"MaxMana", 4}
    };

    protected string charName = "";

    protected bool allied = true;

    public Animator animator = null;

    //Only for use with Leora. Kept here so there's only one damage script
    [SerializeField] public TMP_Text healthBar;
    [SerializeField] public TMP_Text manaBar;
    [SerializeField] Slider hpSlider;
    [SerializeField] public Slider mpSlider;

    public Rigidbody2D charRB;

    [SerializeField] private Vector2 knockbackDirection = Vector2.zero;

    private float strength = 25f;

    [SerializeField] private GameObject hitboxChild = null;

    [Header("Parrying")]
    [SerializeField] private bool isParrying;
    [SerializeField] private bool isPerfectParrying;

    [SerializeField] private GameObject testParrySignal;
    [SerializeField] private SpriteRenderer testParrySprite;
    [SerializeField] public Cooldown parryCooldown;


    public virtual void Update()
    {
       
    }


    #region Stats

    protected void SetMaxHealth()
    {
        statsSheet["Health"] = statsSheet["MaxHealth"];
    }

    protected void SetMaxMana()
    {
        statsSheet["Mana"] = statsSheet["MaxMana"];
    }

    protected int GetHealth()
    {
        return statsSheet["Health"];
    }

    protected void SetHealth(int health)
    {
        statsSheet["Health"] = health;

        if (GetHealth() < 0)
        {
            statsSheet["Health"] = 0;
        }
    }

    public int GetMana()
    {
        return statsSheet["Mana"];
    }

    protected void SetMana(int mana)
    {
        statsSheet["Mana"] = mana;

        if (GetMana() < 0)
        {
            statsSheet["Mana"] = 0;
        }
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

    public void TriggerHurtAnim()
    {
        animator.SetBool("Hurt", true);
        animator.SetBool("Attacking", false);
        DisableHitbox();

        if (allied)
        {
            animator.SetBool("Magicing", false);
            animator.SetBool("isInCombo", false);
        }
    }

    public void StopHurtAnim()
    {
        animator.SetBool("Hurt", false);
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

    #endregion

    #region Getting hit

    #region Parrying

    public void StartParryWindow()
    {
        testParrySprite.color = Color.yellow;
        testParrySignal.SetActive(true);
        isParrying = true;
        //Debug.Log("Parry Window");
    }

    public void EndParryWindow()
    {
        isParrying = false;
        testParrySignal.SetActive(false);
    }

    public void StartPerfectParryWindow()
    {
        testParrySprite.color = Color.green;
        isPerfectParrying = true;
        //Debug.Log("Perfect Parry Window");
    }

    public void EndPerfectParryWindow()
    {
        isPerfectParrying = false;
        testParrySprite.color = Color.yellow;
    }

    #endregion

    public void EnableHitbox()
    {
        hitboxChild.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitboxChild.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Hitbox")
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
                        collision.gameObject.SetActive(false);

                        if (isPerfectParrying)
                        {
                            Debug.Log("Perfect Parry");
                            GotDamaged(otherCharTrigger.statsSheet["Strength"] / 10, otherCharTrigger.gameObject, 0);
                        }
                        else if (isParrying)
                        {
                            Debug.Log("Parry");
                            GotDamaged(otherCharTrigger.statsSheet["Strength"] / 2, otherCharTrigger.gameObject, 0.5f);
                        }
                        else
                        {
                            GotDamaged(otherCharTrigger.statsSheet["Strength"], otherCharTrigger.gameObject, 1);
                            TriggerHurtAnim();
                        }
                    }
                }
            }
        }
    }

    protected void GotDamaged(int incomingDamage, GameObject otherAttacker, float stMod)
    {

        //Debug.Log(charName + " Health: " + GetHealth());
        SetHealth(GetHealth() - incomingDamage);
        //Debug.Log(charName + " After damage health: " + GetHealth());

        StartCoroutine(Knockback(otherAttacker, stMod));

        if (allied)
        {
            healthBar.text = GetHealth() + "/" + statsSheet["MaxHealth"];
            hpSlider.value = ((float)GetHealth()) / statsSheet["MaxHealth"];
        }

        if (GetHealth() <= 0)
        {
            Death();
        }

    }

    private IEnumerator Knockback(GameObject otherAttacker, float stMod)
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
            enemyMovement.canMove = false;
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
                enemyMovement.canMove = true;
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
        Destroy(this.gameObject);
    }

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        charRB = GetComponent<Rigidbody2D>();
        isParrying = false;
        isPerfectParrying = false;
    }
}
