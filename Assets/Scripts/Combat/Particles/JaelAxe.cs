using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaelAxe : HitboxChar
{
    [SerializeField] Rigidbody2D rb;

    private GameObject Player;

    [SerializeField] private GameObject spriteObject;

    private bool launched = false;

    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Cooldown lifespan;

    [SerializeField] private Animator animator;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lifespan.StartCooldown();

        isParryable = false;
        Player = GameObject.Find("CombatPlayer");

        launched = false;

        Vector2 velVec = (Player.transform.position - this.transform.position).normalized;

        if (Mathf.Abs(velVec.x) >= Mathf.Abs(velVec.y))
        {
            animator.SetBool("horizontal", true);

            if (velVec.x > 0)
            {
                sr.flipX = true;
            }
        }
        else
        {
            animator.SetBool("vertical", true);

            if (velVec.y > 0)
            {
                sr.flipY = true;
            }
        }

        

        rb.velocity = velVec * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!lifespan.isCoolingDown)
        {
            Destroy(this.gameObject);
        }
    }
}
