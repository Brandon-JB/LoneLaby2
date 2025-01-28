using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] BaseChar baseChar = null;

    private void Awake()
    {
        baseChar = GetComponent<LeoraChar>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        baseChar.TriggerAttackAnim();
    }

    public void MagAttack()
    {

    }
}
