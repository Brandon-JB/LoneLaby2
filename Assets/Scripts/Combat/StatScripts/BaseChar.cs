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
        {"Strength", 10},
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

    protected float attackCooldown = 0.5f;
    protected float timeAttacking = 0f;

    protected bool isInAttack = false;

    public virtual void Update()
    {
        if (animator.GetBool("Attacking") == true)
        {
            timeAttacking += Time.deltaTime;
        }

        if (isInCooldown() == false)
        {
            StopAttackAnim();
            timeAttacking = 0;
        }
    }

    public bool isInCooldown()
    {
        if (timeAttacking <= attackCooldown && timeAttacking != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
    }

    protected int GetMana()
    {
        return statsSheet["Mana"];
    }

    protected void SetMana(int mana)
    {
        statsSheet["Mana"] = mana;
    }

    protected void GotDamaged(int incomingDamage)
    {
        //Debug.Log(charName + " Health: " + GetHealth());
        SetHealth(GetHealth() - incomingDamage);
        //Debug.Log(charName + " After damage health: " + GetHealth());

        if (allied)
        {
            healthBar.text = "Health: " + GetHealth() + "/" + statsSheet["MaxHealth"];
        }

        if (GetHealth() <= 0)
        {
            Death();
        }
        
    }

    protected void Death()
    {
        SceneManager.LoadScene("Overworld");
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

    public bool isAttacking()
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

    public void ResetCooldown()
    {
        timeAttacking = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Hitbox")
        {
            BaseChar otherCharTrigger = null;

            HitboxChar hitboxChild = null;

            Debug.Log("Triggered");

            if (collision.tag == "Hitbox")
            {
                otherCharTrigger = collision.GetComponent<BaseChar>();

                Debug.Log("Hitbox triggered");

                if (otherCharTrigger == null)
                {
                    Debug.Log("Other trigger not found");

                    hitboxChild = collision.GetComponent<HitboxChar>();
                    otherCharTrigger = hitboxChild.parentChar;

                    if (otherCharTrigger == null)
                    {
                        Debug.Log("Unable to find parent character of hitbox");
                    }
                }

                if (otherCharTrigger != null)
                {
                    if (otherCharTrigger.allied != this.allied)
                    {
                        GotDamaged(10);
                    }
                }
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
