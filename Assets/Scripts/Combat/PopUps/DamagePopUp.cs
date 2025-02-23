using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;

    private float disappearTimer;

    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void SetupInt(int damageAmount, string textPurpose)
    {
        textMesh.text = (damageAmount.ToString());

        if (textPurpose == "Damage")
        {
            textColor = Color.red;
        }
        else if (textPurpose == "Health")
        {
            textColor = Color.green;
        }
        else if (textPurpose == "Mana")
        {
            textColor = Color.blue;
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
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

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
