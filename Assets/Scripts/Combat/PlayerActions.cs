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
        if (Input.GetMouseButtonDown(0))
        {
            if (leoraChar.isInCooldown() == false && leoraChar.isInCombo() == false)
            {
                leoraChar.inCombo = true;
                Attack();
            }
            else if (leoraChar.isInCooldown() == false && leoraChar.inCombo == true) 
            {
                leoraChar.timeInCombo = 0;
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
