using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatPlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    [SerializeField] private InputActionReference Movement, Attack;

    [SerializeField] private float MoveSpeed = 2f;

    private PlayerActions playAction;

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playAction = GetComponent<PlayerActions>();

        animator = GetComponent<Animator>();

        animator.SetFloat("lastY", -1f);
        animator.SetFloat("lastX", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Attacking") == false)
        {
            //Up
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") > 0)
            {
                movementInput = new Vector2(0, Input.GetAxis("Vertical"));
                rb.velocity = movementInput * MoveSpeed;
                //animations
                animator.SetFloat("moveY", 1f);
                animator.SetFloat("moveX", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", 1f);
                animator.SetFloat("lastX", 0f);
            }
            //Down
            else if (Input.GetAxis("Vertical") < 0)
            {
                movementInput = new Vector2(0, Input.GetAxis("Vertical"));
                rb.velocity = movementInput * MoveSpeed;
                //animations
                animator.SetFloat("moveY", -1f);
                animator.SetFloat("moveX", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", -1f);
                animator.SetFloat("lastX", 0f);
            }
            //Right
            else if (Input.GetAxis("Horizontal") > 0)
            {
                movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
                rb.velocity = movementInput * MoveSpeed;
                animator.SetFloat("moveX", 1f);
                animator.SetFloat("moveY", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", 0f);
                animator.SetFloat("lastX", 1f);
            }
            //Left
            else if (Input.GetAxis("Horizontal") < 0)
            {
                movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
                rb.velocity = movementInput * MoveSpeed;
                animator.SetFloat("moveX", -1f);
                animator.SetFloat("moveY", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", 0f);
                animator.SetFloat("lastX", -1f);
            }
            else
            {
                Input.ResetInputAxes();
                movementInput = new Vector2(0, 0);
                rb.velocity = movementInput * 0;

                animator.SetBool("isMoving", false);
                animator.SetFloat("moveX", 0f);
                animator.SetFloat("moveY", 0f);
            }
        }

    }
}
