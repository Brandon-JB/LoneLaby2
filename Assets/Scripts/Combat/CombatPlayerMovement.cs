using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatPlayerMovement : MonoBehaviour
{

    [SerializeField] private float MoveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string LastH = "LastH";
    private const string LastV = "LastV";

    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        canMove = true;
    }

    private void Update()
    {
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

            if (animator.GetBool("Attacking") || animator.GetBool("isInCombo"))
            {
                MoveSpeed = 1f;
            }
            else
            {
                MoveSpeed = 5f;
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
