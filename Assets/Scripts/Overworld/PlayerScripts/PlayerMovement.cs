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
    public bool CanWalk = true;

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
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, MoveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, SolidObjects ))
                {
                    if(CanWalk)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        animator.SetFloat("x", Input.GetAxisRaw("Horizontal"));
                        animator.SetFloat("y", 0);
                        animator.SetBool("isWalking", true);
                    }
                }
            } 
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, SolidObjects))
                {
                    if(CanWalk)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        animator.SetFloat("x", 0);
                        animator.SetFloat("y", Input.GetAxisRaw("Vertical"));
                        animator.SetBool("isWalking", true);
                    }
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
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