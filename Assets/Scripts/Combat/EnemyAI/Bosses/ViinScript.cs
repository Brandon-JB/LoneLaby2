using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViinScript : MonoBehaviour
{
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //IMPORTANT
    //LOOK AT THIS 
    //Each Blood crystal needs to be assigned manually in the editor


    [SerializeField] public Vector3 bottomLeftArenaBounds;
    [SerializeField] public Vector3 topRightArenaBounds;

    [SerializeField] public ViinChar viinChar;

    [SerializeField] private Rigidbody2D viinRB;

    [SerializeField] private BoxCollider2D viinCollider;

    [SerializeField] private GameObject Player;

    [SerializeField] private Rigidbody2D PlayerRB;

    public bool isActive = false;

    [SerializeField] public Cooldown attackCooldown;

    private bool isAttacking = false;

    [Header("AoE Effects")]

    [SerializeField] private float AoESize;

    [SerializeField] public GameObject WarningObject;
    [SerializeField] private Transform WarningTransform;

    [SerializeField] private Transform AttackAoETransform;

    public float attackVariation = 1;
    
    [Header("First Phase")]

    public int AttackCount;

    public int attackLimit = 3;

    [Header("Second Phase")]

    //crystal buffs layout
    /*
     * Start of fight: 4 second attack cooldown speed, 3 attack limit
     * First wave of crystal spawning: 3 second attack cooldown speed, + 3 attack dive limit
     * Each crystal buff: -0.5 second attack cooldown speed, + 5 attack dive limit
     */

    private bool firstCrystalsSpawned = false;
    private bool secondCrystalsSpawned = false;
    private bool thirdCrystalsSpawned = false;

    public Dictionary<int, bool> CrystalDestroyed = new Dictionary<int, bool>()
    {
        { 0, false },
        { 1, false },
        { 2, false },
    };

    public BloodCrystalScript[] bloodCrystals; 

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isAttacking = false;

        firstCrystalsSpawned = false;
        secondCrystalsSpawned = false;
        thirdCrystalsSpawned = false;

        WarningObject.SetActive(false);
        viinChar.DisableHitbox();

        attackCooldown.cooldownTime = 4;
        attackLimit = 3;
        AttackCount = 0;

        viinChar = GetComponent<ViinChar>();

        viinRB = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("CombatPlayer");

        PlayerRB = Player.GetComponent<Rigidbody2D>();
    }

    public void SpawnBloodOrbs()
    {
        foreach(var crystal in CrystalDestroyed)
        {
            //for each crystal not destroyed
            if (crystal.Value == false) 
            {
                viinChar.AddToSpecificStat("Strength", 2);
                bloodCrystals[crystal.Key].StartSpawnAnimation();
                attackCooldown.cooldownTime -= 0.5f;
                attackLimit += 5;
            }
        }
    }

    public void MarkAsDestroyed()
    {
        for (int i = 0; i < bloodCrystals.Length; i++)
        {
            if (bloodCrystals[i].animator.GetBool("Death"))
            {
                CrystalDestroyed[i] = true;
            }
        }
    }

    public void DespawnBloodOrbs()
    {
        foreach (var crystal in CrystalDestroyed)
        {
            //for each crystal not destroyed
            if (crystal.Value == false)
            {
                //Debug.Log(bloodCrystals[crystal.Key].name);
                bloodCrystals[crystal.Key].TriggerDespawnAnimation();
            }
        }
    }

    public void StopStunAnim()
    {
        viinChar.animator.SetBool("stunned", false);
    }

    public void TriggerStunAnim()
    {
        viinChar.animator.SetBool("stunned", true);
    }

    public void TriggerShortStun()
    {
        viinChar.animator.SetBool("shortStun", true);
    }

    public void EndShortStun()
    {
        viinChar.animator.SetBool("shortStun", false);
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
        if (isActive && !viinChar.animator.GetBool("stunned") && !viinChar.animator.GetBool("shortStun") && !isAttacking)
        {
            //small opening after few attacks
            //big opening after crystal break
            //attack off cooldown
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

                    TriggerShortStun();

                    AttackCount = 0;
                }
            }

            //Spawning the first wave of crystals at 75% health
            if (!firstCrystalsSpawned && viinChar.GetHealth() < (viinChar.GetMaxHealth() - (viinChar.GetMaxHealth() / 4 )))
            {
                firstCrystalsSpawned = true;
                attackLimit += 3;
                attackCooldown.cooldownTime = 3;
                SpawnBloodOrbs();
            }

            //Spawning the second wave of crystals at 50% health
            if (!secondCrystalsSpawned && viinChar.GetHealth() < viinChar.GetMaxHealth() / 2)
            {
                secondCrystalsSpawned = true;
                SpawnBloodOrbs();
            }

            //Spawning the third wave of crystals at 25% health
            if (!thirdCrystalsSpawned && viinChar.GetHealth() < viinChar.GetMaxHealth() / 4)
            {
                thirdCrystalsSpawned = true;
                SpawnBloodOrbs();
            }
            
        }
    }

    private IEnumerator WarningForAttack()
    {
        this.transform.position = new Vector2(Player.transform.position.x + Random.Range(-attackVariation, attackVariation), Player.transform.position.y + Random.Range(-attackVariation, attackVariation));

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

