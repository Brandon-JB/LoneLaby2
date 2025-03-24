using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IvarScript : MonoBehaviour
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
    //Bounds and Phase change need to be assigned in the editor

    [SerializeField] private IvarChar ivarChar;

    [SerializeField] private GameObject Player;
    private LeoraChar2 leoraChar;

    public bool isActive;

    [Header("Vectors")]
    [SerializeField] private Vector2 bottomLeftArenaBounds;
    [SerializeField] private Vector2 topRightArenaBounds;
    [SerializeField] private GameObject enemySpawnPosition;
    private Vector2 moveTargetPosition;

    [Header("Casting")]
    [SerializeField] private GameObject[] summonList;
    private List<GameObject> enemyList =  new List<GameObject>();
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Cooldown castingCooldown;
    public Animator castParticleAnimator;

    private int whichMoveToCast;

    [Header("Moving")]
    private bool isMoving;
    [SerializeField] private Rigidbody2D ivarRB;
    [SerializeField] private float moveSpeed;

    [Header("Phase Change")]
    public GameObject darknessEffect;
    private bool firstTeleportHappened;
    public GameObject firstTPObject;
    public GameObject ivarFirstTPObject;
    private bool secondTeleportHappened;
    public GameObject secondTPObject;
    public GameObject ivarSecondTPObject;

    [Header("Massive Damage Cast")]
    [SerializeField] private Cooldown timeUntilBigAttack;
    public int damageThreshold;
    public int damageTaken;
    public bool bigCasting;

    //1 for the first teleport, 2 for the second teleport
    private int teleportNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        bigCasting = false;
        isActive = false;

        Player = GameObject.Find("CombatPlayer");
        leoraChar = Player.GetComponent<LeoraChar2>();

        firstTeleportHappened = false;
        secondTeleportHappened = false;

        moveTargetPosition = new Vector2(Random.Range(bottomLeftArenaBounds.x, topRightArenaBounds.x), Random.Range(bottomLeftArenaBounds.y, topRightArenaBounds.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //Moving
            if (ivarChar.animator.GetBool("stunned") == false && ivarChar.animator.GetBool("Casting") == false && ivarChar.animator.GetBool("tpCast") == false && ivarChar.animator.GetBool("bigAttack") == false)
            {
                //if ivar is close to the target point
                if (Vector2.Distance(this.transform.position, moveTargetPosition) < 1)
                {
                    //Chooses a random position from within the arena bounds
                    moveTargetPosition = new Vector2(Random.Range(bottomLeftArenaBounds.x, topRightArenaBounds.x), Random.Range(bottomLeftArenaBounds.y, topRightArenaBounds.y));
                }
                else
                {
                    ivarRB.transform.position = Vector2.MoveTowards(ivarRB.transform.position, moveTargetPosition, moveSpeed * Time.deltaTime);
                }
            }

            //Casting
            if (castingCooldown.isCoolingDown == false && ivarChar.animator.GetBool("Casting") == false && ivarChar.animator.GetBool("tpCast") == false && ivarChar.animator.GetBool("bigAttack") == false && ivarChar.animator.GetBool("stunned") == false)
            {
                whichMoveToCast = 0;

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i] == null)
                    {
                        enemyList.RemoveAt(i);
                    }
                }

                //Limits the amount of enemies
                if (enemyList == null || enemyList.Count() <= 4)
                {
                    whichMoveToCast = Random.Range(0, 2);
                }


                TriggerCastAnim();
            }

            //Teleporting to the fire mazes
            if (firstTeleportHappened == false && ivarChar.GetHealth() <= ivarChar.GetMaxHealth() / 2)
            {
                teleportNum = 1;
                TriggerTPCast();
            }
            else if (secondTeleportHappened == false && ivarChar.GetHealth() <= ivarChar.GetMaxHealth() / 4)
            {
                teleportNum = 2;
                TriggerTPCast();
            }

            //Massive attack during the maze
            if (bigCasting)
            {
                if (damageTaken >= damageThreshold)
                {
                    TriggerStunAnim();
                    bigCasting = false;
                    ivarChar.animator.SetBool("bigAttack", false);
                    timeUntilBigAttack.Interupted();
                    darknessEffect.SetActive(false);

                    //Sending Ivar and player back to normal area
                    Player.transform.position = new Vector2((bottomLeftArenaBounds.x + topRightArenaBounds.x) / 2, ((bottomLeftArenaBounds.y + topRightArenaBounds.y) / 2) - 2);
                    this.transform.position = new Vector2((bottomLeftArenaBounds.x + topRightArenaBounds.x) / 2, (bottomLeftArenaBounds.y + topRightArenaBounds.y) / 2);
                }
                else if (!timeUntilBigAttack.isCoolingDown)
                {
                    bigCasting = false;
                    ivarChar.animator.SetBool("bigAttack", false);
                    leoraChar.GotDamaged(50, this.gameObject, 0);
                    leoraChar.TriggerHurtAnim();
                    darknessEffect.SetActive(false);

                    //Sending Ivar and player back to normal arena
                    Player.transform.position = new Vector2((bottomLeftArenaBounds.x + topRightArenaBounds.x) / 2, ((bottomLeftArenaBounds.y + topRightArenaBounds.y) / 2) - 2);
                    this.transform.position = new Vector2((bottomLeftArenaBounds.x + topRightArenaBounds.x) / 2, (bottomLeftArenaBounds.y + topRightArenaBounds.y) / 2);
                }
                

            }
        }
    }

    private void TriggerTPCast()
    {
        ivarChar.animator.SetBool("tpCast", true);
        ivarChar.animator.SetBool("bigAttack", true);
    }

    public void StopTPCast()
    {
        ivarChar.animator.SetBool("tpCast", false);
    }

    public void Teleport()
    {
        damageTaken = 0;

        switch (teleportNum)
        {
            case 1:
                Player.transform.position = firstTPObject.transform.position;
                this.transform.position = ivarFirstTPObject.transform.position;
                darknessEffect.SetActive(true);
                firstTeleportHappened = true;
                break;

            case 2:
                Player.transform.position = secondTPObject.transform.position;
                this.transform.position = ivarSecondTPObject.transform.position;
                darknessEffect.SetActive(true);
                secondTeleportHappened = true;
                break;
            default:
                Debug.Log("Wrong teleport number");
                break;
        }

        timeUntilBigAttack.StartCooldown();
        bigCasting = true; 
    }

    private void TriggerCastAnim()
    {
        ivarChar.animator.SetBool("Casting", true);

        switch (whichMoveToCast)
        {
            //Projectile
            case 0:
                castParticleAnimator.SetBool("projectile", true);
                break;
            //Summoning
            case 1:
                castParticleAnimator.SetBool("summon", true);
                break;
        }
    }

    public void TriggerStunAnim()
    {
        ivarChar.SpawnParticle("stunFX", ivarChar.transform.position, ivarChar.transform, ivarChar.stunTimer.cooldownTime);
        ivarChar.stunTimer.StartCooldown();
        ivarChar.animator.SetBool("stunned", true);
    }

    public void StopCastAnim()
    {
        castingCooldown.StartCooldown();
        ivarChar.animator.SetBool("Casting", false);
    }

    public void StopStunAnim()
    {
        ivarChar.animator.SetBool("stunned", false);
    }

    public void CastSpell()
    {
        castingCooldown.StartCooldown();

        castParticleAnimator.SetBool("summon", false);
        castParticleAnimator.SetBool("projectile", false);

        switch (whichMoveToCast)
        {
            //Projectile
            case 0:
                GameObject projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
                IvarProjectile projScript = projectile.GetComponent<IvarProjectile>();
                projScript.parentChar = this.ivarChar;
                break;
            //Summoning
            case 1:
                    enemyList.Add(Instantiate(summonList[Random.Range(0, summonList.Length)], enemySpawnPosition.transform.position, Quaternion.identity));
                break;
        }
    }
}
