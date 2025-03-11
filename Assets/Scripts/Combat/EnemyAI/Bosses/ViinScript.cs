using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViinScript : MonoBehaviour
{
    [SerializeField] private Vector3 bottomLeftArenaBounds;
    [SerializeField] private Vector3 topRightArenaBounds;

    [SerializeField] private ViinChar viinChar;

    [SerializeField] private Rigidbody2D viinRB;

    [SerializeField] private BoxCollider2D viinCollider;

    [SerializeField] private GameObject Player;

    [SerializeField] private Rigidbody2D PlayerRB;

    public bool isActive = false;

    [SerializeField] private Cooldown attackCooldown;

    private bool isAttacking = false;

    [Header("AoE Effects")]

    [SerializeField] private float AoESize;

    [SerializeField] private GameObject WarningObject;
    [SerializeField] private Transform WarningTransform;

    [SerializeField] private Transform AttackAoETransform;
    
    [Header("First Phase")]

    private int AttackCount;

    private int attackLimit = 3;

    private bool firstPhase = true;

    [Header("Second Phase")]

    private int bloodOrbCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        bloodOrbCount = 0;
        firstPhase = true;
        isAttacking = false;

        WarningObject.SetActive(false);
        viinChar.DisableHitbox();

        attackLimit = 3;

        viinChar = GetComponent<ViinChar>();

        viinRB = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("CombatPlayer");

        PlayerRB = Player.GetComponent<Rigidbody2D>();
    }

    public void SpawnBloodOrbs()
    {

    }

    public void StopStunAnim()
    {
        viinChar.animator.SetBool("stunned", false);
    }

    public void TriggerStunAnim()
    {
        viinChar.animator.SetBool("stunned", true);
    }

    public void EnableHurtbox()
    {
        viinCollider.enabled = true;
    }

    public void DisableHurtbox()
    {
        viinCollider.enabled = false;
    }

    private void Update()
    {
        if (isActive && !viinChar.animator.GetBool("stunned") && !isAttacking)
        {
            if (!attackCooldown.isCoolingDown)
            {
                if (AttackCount < attackLimit)
                {
                    isAttacking = true;

                    StartCoroutine(WarningForAttack());
                }
                else
                {
                    this.transform.position = new Vector2((bottomLeftArenaBounds.x + topRightArenaBounds.x) / 2, (bottomLeftArenaBounds.y + topRightArenaBounds.y) / 2);

                    attackCooldown.StartCooldown();

                    viinChar.animator.SetBool("stunned", true);

                    AttackCount = 0;
                }
            }
        }
    }

    private IEnumerator WarningForAttack()
    {
        this.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y);

        WarningTransform.localScale = new Vector2(Vector2.one.x * AoESize, Vector2.one.y * AoESize);
        AttackAoETransform.localScale = new Vector2(Vector2.one.x * AoESize, Vector2.one.y * AoESize);

        WarningObject.SetActive(true);

        yield return new WaitForSeconds(1);

        WarningObject.SetActive(false);

        viinChar.animator.SetBool("Attacking", true);

        attackCooldown.StartCooldown();

        isAttacking = false;

        AttackCount++;
    }
}

