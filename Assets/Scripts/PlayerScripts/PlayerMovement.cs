using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    [SerializeField] private InputActionReference Movement, Attack;

    [SerializeField] private float MoveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0)
        {
            movementInput = new Vector2(0, Input.GetAxis("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            movementInput = new Vector2(0, Input.GetAxis("Vertical"));
            rb.velocity = movementInput * MoveSpeed;
            //animations
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movementInput * MoveSpeed;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movementInput * MoveSpeed;
        }
        else
        {
            rb.velocity = movementInput * 0;

        }

    }
}
