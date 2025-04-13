using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatPlayerMovement : MonoBehaviour
{

    [SerializeField] private float MoveSpeed = 5f;
    private float defaultMoveSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    [SerializeField] private float attackingMoveSpeed = 1f;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string LastH = "LastH";
    private const string LastV = "LastV";

    public bool canMove;
    public LeoraChar2 leoraChar;
    public Sprite hurtSprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        canMove = true;

        defaultMoveSpeed = MoveSpeed;
    }

    private void Update()
    {
        if (leoraChar.stunned && leoraChar.stunTimer.isCoolingDown)
        {
            canMove = false;
            //animator.enabled = false;
            
        }
        else
        {
            if (leoraChar.stunned)
            {
                canMove = true;
                //animator.enabled = true;
            }

            leoraChar.stunned = false;
        }


        if (canMove == true)
        {
            //Movement
            if (animator.GetBool("Magicing") == false && animator.GetBool("Parrying") == false)
            {
                movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            }
            else
            {
                movement.Set(0, 0);

            }

            animator.SetFloat(horizontal, movement.x);
            animator.SetFloat(vertical, movement.y);

            if (leoraChar.comboTimer.isCoolingDown)//animator.GetBool("Attacking") || animator.GetBool("isInCombo"))
            {
                MoveSpeed = attackingMoveSpeed;
            }
            else
            {
                MoveSpeed = defaultMoveSpeed;
            }

            rb.velocity = movement * MoveSpeed;          

            if (movement != Vector2.zero)
            {
                animator.SetFloat(LastH, movement.x);
                animator.SetFloat(LastV, movement.y);
            }
        }
         
    }

}
