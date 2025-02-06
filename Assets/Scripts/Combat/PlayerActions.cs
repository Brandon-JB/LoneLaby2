using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] LeoraChar leoraChar = null;

    

    private void Awake()
    {
        leoraChar = GetComponent<LeoraChar>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G pressed");
            if (leoraChar.isInCooldown() == false && leoraChar.isInCombo() == false)
            {
                Debug.Log("Attack started");
                leoraChar.inCombo = true;
                //leoraChar.comboTimer = 1.5f;
                Attack();
            }
            else if (/*leoraChar.isInCooldown() == false && */leoraChar.inCombo == true && leoraChar.animator.GetBool("ThirdCombo") == false) 
            {
                leoraChar.ResetCombo();
                leoraChar.DoNextCombo();
            }
        }

        
    }

    private void Attack()
    {
        leoraChar.TriggerAttackAnim();
    }

    public void MagAttack()
    {

    }
}
