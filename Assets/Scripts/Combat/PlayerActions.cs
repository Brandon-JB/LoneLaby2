using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] LeoraChar2 leoraChar = null;

    [SerializeField] private GameObject magicParticle;

    [SerializeField] private GameObject tempMagPart;

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
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            //if (leoraChar.attackCooldown.isCoolingDown) return;

            Debug.Log("Reading G Input");

            //Sets the Attacking bool in the animator to true
            if (!leoraChar.attackCooldown.isCoolingDown)
            {
                leoraChar.DoNextCombo();
            }
            else if (leoraChar.comboTimer.isCoolingDown)
            {
                leoraChar.DoNextCombo();
            }
        }*/


        //Attacking
        if (!leoraChar.stunTimer.isCoolingDown)
        {
            if (InputManager.attackPressed)
            {
                //Debug.Log("Reading G Input");

                //Sets the Attacking bool in the animator to true
                if (!leoraChar.attackCooldown.isCoolingDown)
                {
                    leoraChar.DoNextCombo();
                }
                else if (leoraChar.comboTimer.isCoolingDown)
                {
                    leoraChar.DoNextCombo();
                }
            }

            if (tempMagPart != null && leoraChar.animator.GetBool("Hurt"))
            {
                Destroy(tempMagPart);
            }

            //Magicking
            if (InputManager.magicPressed && leoraChar.magicCooldown.isCoolingDown == false && leoraChar.GetMana() > 0)
            {
                //Debug.Log("M Pressed");

                leoraChar.magicCooldown.StartCooldown();

                leoraChar.MagAttack();

                //GameObject tempMagPart;

                //Spawn the magic particles in the animator using the functions below

            }

            //Parrying
            if (InputManager.parryPressed && leoraChar.parryCooldown.isCoolingDown == false && leoraChar.animator.GetBool("Hurt") == false)
            {
                leoraChar.TriggerParryAnim();
            }
        }
    }

    #region Magic Animator Particles
    public void SpawnLightParticle()
    {
        if (leoraChar.magicType == "lightMag")
        {

            tempMagPart = Instantiate(magicParticle, this.transform.position, Quaternion.identity, this.transform);

            MagicParticles tempMagManager = tempMagPart.GetComponent<MagicParticles>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool(leoraChar.magicType, true);
        }
    }

    public void SpawnBloodParticle()
    {
        if (leoraChar.magicType == "bloodMag")
        {
            tempMagPart = Instantiate(magicParticle, this.transform.position, Quaternion.identity, this.transform);

            MagicParticles tempMagManager = tempMagPart.GetComponent<MagicParticles>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool(leoraChar.magicType, true);
        }
    }

    public void SpawnMindParticle()
    {
        if (leoraChar.magicType == "mindMag")
        {
            tempMagPart = Instantiate(magicParticle, this.transform.position, Quaternion.identity, this.transform);

            MagicParticles tempMagManager = tempMagPart.GetComponent<MagicParticles>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool(leoraChar.magicType, true);
        }
    }

    public void SpawnDarkParticle()
    { 
        if (leoraChar.magicType == "darkMag")
        {
            tempMagPart = Instantiate(magicParticle, this.transform.position, Quaternion.identity, this.transform);

            MagicParticles tempMagManager = tempMagPart.GetComponent<MagicParticles>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool(leoraChar.magicType, true);
        }
    }

    #endregion
}
