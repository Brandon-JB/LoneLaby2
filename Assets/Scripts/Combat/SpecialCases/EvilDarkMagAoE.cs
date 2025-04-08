using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilDarkMagAoE : MonoBehaviour
{
    public Animator animator;

    public CircleCollider2D hitbox;

    BaseChar enemyChar;

    [SerializeField] public DarkLeoraChar darkLeora;


    // Start is called before the first frame update
    void Start()
    {
        darkLeora = GameObject.Find("DarkLeora").GetComponent<DarkLeoraChar>();
        //pulseCooldown.StartCooldown();
        animator.SetBool("darkFollowup", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Hitbox" && (collision.tag == "Enemy" || collision.tag == "Boss"))
        {
            enemyChar = collision.GetComponent<BaseChar>();

            enemyChar.GotDamaged(leoraChar.statsSheet["MagAttack"], enemyChar.gameObject, 0);
            enemyChar.TriggerHurtAnim();
        }
    }*/

    public void TriggerHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    public void DeleteThis()
    {
        Destroy(this.gameObject);
    }
}
