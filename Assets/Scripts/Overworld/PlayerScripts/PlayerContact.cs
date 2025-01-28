using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContact : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
