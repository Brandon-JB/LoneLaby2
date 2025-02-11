using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NonCombatPlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float MoveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private PlayerActions playAction;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string LastH = "LastH";
    private const string LastV = "LastV";


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playAction = GetComponent<PlayerActions>();

        animator = GetComponent<Animator>();

        animator.SetFloat("lastY", -1f);
        animator.SetFloat("lastX", 0f);
    }

    private void Update()
    {
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        rb.velocity = movement * MoveSpeed;

        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat(LastH, movement.x);
            animator.SetFloat(LastV, movement.y);
        }
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        if (animator.GetBool("Attacking") == false)
        {
            //Up
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxis("Vertical") > 0)
            {
                movementInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
                rb.MovePosition(rb.position + movementInput * MoveSpeed * Time.deltaTime);
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
                movementInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
                rb.MovePosition(rb.position + movementInput * MoveSpeed * Time.deltaTime);                //animations
                animator.SetFloat("moveY", -1f);
                animator.SetFloat("moveX", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", -1f);
                animator.SetFloat("lastX", 0f);
            }
            //Right
            else if (Input.GetAxis("Horizontal") > 0)
            {
                movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                rb.MovePosition(rb.position + movementInput * MoveSpeed * Time.deltaTime);
                animator.SetFloat("moveX", 1f);
                animator.SetFloat("moveY", 0f);
                animator.SetBool("isMoving", true);
                animator.SetFloat("lastY", 0f);
                animator.SetFloat("lastX", 1f);
            }
            //Left
            else if (Input.GetAxis("Horizontal") < 0)
            {
                movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                rb.MovePosition(rb.position + movementInput * MoveSpeed * Time.deltaTime);
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
                rb.MovePosition(rb.position + movementInput * MoveSpeed * Time.deltaTime);

                animator.SetBool("isMoving", false);
                animator.SetFloat("moveX", 0f);
                animator.SetFloat("moveY", 0f);
            }
        }

    }*/
}
