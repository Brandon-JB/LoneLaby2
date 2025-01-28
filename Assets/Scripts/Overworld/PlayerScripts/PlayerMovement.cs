using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    [SerializeField] private InputActionReference Movement, Attack;

    [SerializeField] private float MoveSpeed = 5f;

    private PlayerActions playAction;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playAction = GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement...looks so bad
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0)
        {
            movementInput = new Vector2(0, Input.GetAxis("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            movementInput = new Vector2(0, Input.GetAxis("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("x", 0);
            animator.SetFloat("y", -1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("y", 0);
            animator.SetFloat("x", 1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("y", 0);
            animator.SetFloat("x", -1);
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = movementInput * 0;
            animator.SetBool("isWalking", false);

        }

    }
}
