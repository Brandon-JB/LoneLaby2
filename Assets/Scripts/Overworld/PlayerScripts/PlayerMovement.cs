using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    public float MoveSpeed = 5f;


    public Animator animator;

    //grid based movement
    public Transform movePoint;
    public LayerMask SolidObjects;
    public float Timer = 1;
    public static bool CanWalk = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            movementInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            movementInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("x", 0);
            animator.SetFloat("y", -1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            rb.velocity = movementInput * MoveSpeed;
            //animations
            animator.SetFloat("y", 0);
            animator.SetFloat("x", 1);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
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



/*//Movement...looks so bad
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

        }*/