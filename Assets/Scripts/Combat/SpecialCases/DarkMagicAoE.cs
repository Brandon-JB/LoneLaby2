using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DarkMagicAoE : MonoBehaviour
{

    public Animator animator;

    public CircleCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("darkFollowup", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}
