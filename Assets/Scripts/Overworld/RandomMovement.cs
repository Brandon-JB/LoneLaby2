using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Adjust speed
    public float lifetime = 5f;  // Time before deletion

    private Rigidbody2D rb;
    private Vector2 randomDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start changing direction every second
        InvokeRepeating(nameof(ChangeDirection), 0f, 1f);

        // Destroy after 5 seconds
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.velocity = randomDirection * moveSpeed;
    }

    void ChangeDirection()
    {
        // Pick a random direction
        randomDirection = Random.insideUnitCircle.normalized;
    }
}
