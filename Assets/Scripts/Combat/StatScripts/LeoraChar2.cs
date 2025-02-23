using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeoraChar2 : BaseChar
{

    //Variable for the combo window.
    //If the combo timer is "cooling down" then that just means it's in the combo window.
    [SerializeField] public Cooldown comboTimer;

    //Variable for the cooldown time.
    [SerializeField] public Cooldown attackCooldown;

    [SerializeField] public Cooldown magicCooldown;

    [SerializeField] private GameOver gameOverManager;

    [SerializeField] private CombatPlayerMovement playerMovement;

    
    //public bool isInFirstCombo = false;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Leora";
        allied = true;

        healthBar.text = GetHealth() + "/" + statsSheet["MaxHealth"];
        manaBar.text = GetMana() + "/" + statsSheet["MaxMana"];

        animator.SetFloat("LastH", 0);
        animator.SetFloat("LastV", -1);
    }


    public override void Update()
    {
        //Once the combo timer ends, then set the combo bool in the animator to false;
        if (!comboTimer.isCoolingDown)
        {
            animator.SetBool("isInCombo", false);
        }

        
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

        SetMana(GetMana() - 1);
        manaBar.text = GetMana() + "/" + statsSheet["MaxMana"];
        mpSlider.value = ((float)GetMana()) / statsSheet["MaxMana"];
    }

    public void EndMagick()
    {
        animator.SetBool("Magicing", false);
    }
}
