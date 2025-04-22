using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;

    private float disappearTimer;

    private Color textColor;

    [SerializeField] private Color damageColor, healthColor, manaColor;

    private bool isDamage = false;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        textMesh = transform.GetComponentInChildren<TextMeshPro>();
    }

    public void SetupInt(int damageAmount, string textPurpose)
    {
        textMesh.text = (damageAmount.ToString());

        if (textPurpose == "Damage")
        {
            isDamage = true;
            animator.enabled = true;
            
            textColor = damageColor;
        }
        else if (textPurpose == "Health")
        {
            isDamage = false;
            animator.enabled = false;

            textColor = healthColor;
        }
        else if (textPurpose == "Mana")
        {
            isDamage = false;
            animator.enabled = false;

            textColor = manaColor;
        }
        
        disappearTimer = 0.7f;
    }

    public void SetupString(string Words)
    {
        textMesh.text = Words;
        textColor = textMesh.color;
        disappearTimer = 0.7f;
    }

    private void Update()
    {
        textMesh.color = textColor;

        float moveYSpeed = 2;
        if (isDamage == false)
        {
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
