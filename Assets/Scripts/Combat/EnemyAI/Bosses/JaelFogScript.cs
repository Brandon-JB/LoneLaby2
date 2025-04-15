using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JaelFogScript : MonoBehaviour
{
    [SerializeField] private GameObject jaelSprite;
    [SerializeField] private JaelChar jaelChar;
    [SerializeField] private fakeJaelScript fakeJael;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D hurtBox;

    private void Start()
    {
        jaelChar = GetComponentInParent<JaelChar>();
        fakeJael = GetComponentInParent<fakeJaelScript>();
    }

    public void disappearJael()
    {
        jaelSprite.SetActive(false);
        if (hurtBox != null)
        {
            hurtBox.enabled = false;
        }
    }

    public void appearJael()
    {
        jaelSprite.SetActive(true);
        if (hurtBox != null)
        {
            hurtBox.enabled = true;
        }
    }

    public void ResetFog()
    {
        animator.SetBool("tpIn", false);
        animator.SetBool("tpOut", false);
    }

    public void JaelAttackAnimTrigger()
    {
        if (jaelChar != null)
        {
            jaelChar.TriggerAttackAnim();
        }
        else
        {
            fakeJael.TriggerAttackAnim();
        }
    }

    public void TpAndAttack()
    {
        if (jaelChar != null)
        {
            JaelScript jaelScript = jaelChar.gameObject.GetComponent<JaelScript>();

            jaelScript.TPandAttack();
        }
    }
}
