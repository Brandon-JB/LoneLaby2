using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 DO NOT USE THIS CODE
THIS IS OLD CODE THAT WAS DUMB AND STUPID AND BAD
IGNORE THIS
GO TO LEORACHAR2
 */ 
 




public class LeoraChar : BaseChar
{
    public float comboTimer = 1.5f;
    public float timeInCombo = 0f;

    public bool inCombo = false;

    private float attackCooldown = 0.5f;
    private float timeAttacking = 0f;

    private bool isInAttack = false;


    //public bool isInFirstCombo = false;

    // Start is called before the first frame update
    void Start()
    {

        charName = "Leora";
        inCombo = false;
        attackCooldown = 0.7f;
    }


    public override void Update()
    {
        if (inCombo == true)
        {
            animator.SetBool("isComboing", true);
            timeInCombo += Time.deltaTime;
        }

        if (timeInCombo >= comboTimer)
        {
            endCombo();
            timeInCombo = 0;
        }

        if (isAttacking() )
        {
            timeAttacking += Time.deltaTime;
        }

        if (isInCooldown() == false)
        {
            timeAttacking = 0;

            StopAttackAnim();
        }

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

    public void endCombo()
    {
        inCombo = false;
        timeInCombo = 0;
        animator.SetBool("isComboing", false);
        animator.SetBool("SecondCombo", false);
        animator.SetBool("ThirdCombo", false);
    }

    public bool isInCombo()
    {
        if (timeInCombo < comboTimer && timeInCombo != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetCombo()
    {
        timeInCombo = 0;
    }

    public void DoNextCombo()
    {
        if (inCombo && animator.GetBool("SecondCombo") == false)
        {
            animator.SetBool("SecondCombo", true);
            //comboTimer = 0.7f;
        }
        else if (animator.GetBool("SecondCombo"))
        {
            animator.SetBool("ThirdCombo", true);
            //timeInCombo = 0.7f * comboTimer;
            
            timeAttacking = 0;
        }
    }

    public void ResetCooldown()
    {
        timeAttacking = 0;
    }
}
