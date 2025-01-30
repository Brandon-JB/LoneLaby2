using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoraChar : BaseChar
{
    private float comboTimer = 2f;
    public float timeInCombo = 0f;

    public bool inCombo = false;

    //public bool isInFirstCombo = false;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Leora";
        inCombo = false;
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
    }

    public void endCombo()
    {
        inCombo = false;
        animator.SetBool("isComboing", false);
        animator.SetBool("SecondCombo", false);
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

    public void DoNextCombo()
    {
        if (inCombo)
        {
            animator.SetBool("SecondCombo", true);
        }
    }
}
