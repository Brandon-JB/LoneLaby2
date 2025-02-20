using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseChar : MonoBehaviour
{
    public Dictionary<string, int> statsSheet = new Dictionary<string, int>()
    {
        {"Strength", 5},
        {"Defense", 4},
        {"Health", 30},
        {"MaxHealth", 30},
        {"Mana", 4},
        {"MaxMana", 4}
    };

    protected string charName = "";

    protected bool allied = true;

    public Animator animator = null;

    [SerializeField] TMP_Text healthBar;

    public Rigidbody2D charRB;

    [SerializeField] private Vector2 knockbackDirection = Vector2.zero;

    [SerializeField] private float strength = 20f;

    [SerializeField] private GameObject hitboxChild = null;

    [SerializeField] private bool isParrying;
    [SerializeField] private bool isPerfectParrying;


    public virtual void Update()
    {
       
    }

    

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

    protected void GotDamaged(int incomingDamage, GameObject otherAttacker)
    {

        //Debug.Log(charName + " Health: " + GetHealth());
        SetHealth(GetHealth() - incomingDamage);
        //Debug.Log(charName + " After damage health: " + GetHealth());

        StartCoroutine(Knockback(otherAttacker));
       
        if (allied)
        {
            healthBar.text = "Health: " + GetHealth() + "/" + statsSheet["MaxHealth"];
        }

        if (GetHealth() <= 0)
        {
            Death();
        }

    }

    private IEnumerator Knockback(GameObject otherAttacker)
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
            charRB.AddForce(knockbackDirection * (strength * 15000f), ForceMode2D.Impulse);
            Debug.Log("Launch enemy");
        }
        else
        {
            playerMovement.canMove = false;
            charRB.AddForce(knockbackDirection * strength, ForceMode2D.Impulse);
        }

        charRB.AddForce(knockbackDirection * strength, ForceMode2D.Impulse);

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
        DisableHitbox();
        animator.SetBool("Hurt", true);
        animator.SetBool("Attacking", false);

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

    /*public void AttackStart()
    {
        isInAttack = true;
    }

    public void AttackOver()
    {
        isInAttack = false;
    }*/

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
                        GotDamaged(otherCharTrigger.statsSheet["Strength"], collision.gameObject);
                        TriggerHurtAnim();
                    }
                }
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        charRB = GetComponent<Rigidbody2D>();
        isParrying = false;
        isPerfectParrying = false;
    }
}
