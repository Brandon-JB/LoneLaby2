using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IvarScript : MonoBehaviour
{
    [SerializeField] private IvarChar ivarChar;

    [SerializeField] private GameObject Player;

    [Header("Vectors")]
    [SerializeField] private Vector2 bottomLeftArenaBounds;
    [SerializeField] private Vector2 topRightArenaBounds;
    [SerializeField] private Vector2 enemySpawnPosition;
    private Vector2 moveTargetPosition;

    [Header("Casting")]
    [SerializeField] private GameObject[] summonList;
    private GameObject[] enemyList = { };
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Cooldown castingCooldown;

    [Header("Moving")]
    private bool isMoving;
    [SerializeField] private Rigidbody2D ivarRB;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("CombatPlayer");

        moveTargetPosition = new Vector2(Random.Range(bottomLeftArenaBounds.x, topRightArenaBounds.x), Random.Range(bottomLeftArenaBounds.y, topRightArenaBounds.y));
    }

    // Update is called once per frame
    void Update()
    {
        //Moving
        if (ivarChar.animator.GetBool("stunned") == false && ivarChar.animator.GetBool("Casting") == false)
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
        if (castingCooldown.isCoolingDown == false && ivarChar.animator.GetBool("Casting") == false)
        {
            TriggerCastAnim();
        }
    }

    private void TriggerCastAnim()
    {
        ivarChar.animator.SetBool("Casting", true);
    }

    public void TriggerStunAnim()
    {
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

        int whichMove = 0;

        //Limits the amount of enemies
        if (enemyList == null || enemyList.Count() <= 4)
        {
            whichMove = Random.Range(0, 2);
        }

        switch (whichMove)
        {
            //Projectile
            case 0:
                GameObject projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
                IvarProjectile projScript = projectile.GetComponent<IvarProjectile>();
                projScript.parentChar = this.ivarChar;
                break;
            //Summoning
            case 1:
                    enemyList.Append(Instantiate(summonList[Random.Range(0, summonList.Length)], enemySpawnPosition, Quaternion.identity));
                break;
        }
    }
}
