using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] LeoraChar2 leoraChar = null;

    

    private void Awake()
    {
        leoraChar = GetComponent<LeoraChar2>();

        
    }


    //Update for LeoraChar
    //IGNORE THIS STUPID STINKY CODE
    /*
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
            else if (leoraChar.inCombo == true && leoraChar.animator.GetBool("ThirdCombo") == false) 
            {
                leoraChar.ResetCombo();
                leoraChar.DoNextCombo();
            }
        }
    }
    */

    //Update for LeoraChar2
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            //if (leoraChar.attackCooldown.isCoolingDown) return;

            Debug.Log("Reading G Input");

            //Sets the Attacking bool in the animator to true
            leoraChar.DoNextCombo();
        }
    }

    public void MagAttack()
    {

    }
}
