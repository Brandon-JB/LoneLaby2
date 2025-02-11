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

    public InputAction attackAction;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string LastH = "LastH";
    private const string LastV = "LastV";


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Movement
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        rb.velocity = movement * MoveSpeed;

        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat(LastH, movement.x);
            animator.SetFloat(LastV, movement.y);
        }

        //Attacking
        if (InputManager.attackPressed)
        {
            //do whatever for attacking
        }
         
    }

}
