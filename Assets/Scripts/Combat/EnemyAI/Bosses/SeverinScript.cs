using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinScript : EnemyScript
{
    private bool firstBigHitDone;
    private bool secondBigHitDone;

    public bool fstBigMoveOver;
    public bool sndBigMoveOver;

    [SerializeField] public GameObject screenFlash;
    [SerializeField] private GameObject massiveHitbox;

    [SerializeField] private float defaultCooldown = 2;

    public bool parrying;
    [SerializeField] private GameObject parryIndicator;

    [SerializeField] private List<GameObject> tpPositions;

    [SerializeField] private GameObject topRightBound;
    [SerializeField] private GameObject bottomLeftBound;

    // Start is called before the first frame update
    void Start()
    {
        firstBigHitDone = false;
        secondBigHitDone = false;
        fstBigMoveOver = false;
        sndBigMoveOver = false;
        screenFlash.SetActive(false);
        parryIndicator.SetActive(false);

        cooldown.StartCooldown();
    }

    public void TriggerParry()
    {
        parrying = true;
        parryIndicator.SetActive(true);
    }

    /*Severin Combat loop:
     * 1. Follow player around like a normal enemy and attack
     * 2. At 50/25%, start the big attack
     *      a. Have a list of possible dash positions spread throughout the arena
     *      b. Have Severin choose the farthest one from Leora to dash to
     *      c. Dash to that position (with hurtbox turned off to prevent pushing)
     *      d. start windup
     * 3. If the player hits Severin during the windup, then she does the charged attack early and it's unparriable
     * 4. After attack charges up, severin dashes towards the player
     * 5. After severin reaches the player, she does a massive attack that covers the whole screen and needs to be parried
     * 6. After this, severin gets stunned for several seconds
    */

    public void FarthestTP()
    {
        GameObject farthestPoint = null;
        float farthestDistance = 0;

        foreach (var pos in tpPositions)
        {
            float distance = Vector2.Distance(pos.transform.position, this.transform.position);

            if (distance > farthestDistance)
            {
                farthestPoint = pos;
                farthestDistance = distance;
            }
        }

        this.transform.position = farthestPoint.transform.position;
    }

    public void TPNextToLeora()
    {
        Vector2 tpPos = new Vector2(Player.transform.position.x, Player.transform.position.y + 2.5f);

        if (tpPos.y > topRightBound.transform.position.y)
        {
            tpPos.y = Player.transform.position.y - 2.5f;
        }

        this.transform.position = tpPos;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (DistanceFromPlayer > followRange)
        {
            enemyChar.animator.SetBool("isMoving", false);
        }

        if (!cooldown.isCoolingDown && enemyChar.stunTimer.isCoolingDown == false && !enemyChar.animator.GetBool("charging"))
        {
            canMove = true;
        }
        else// if (enemyChar.animator.GetBool("Hurt") == true)
        {
            canMove = false;
            path.destination = this.transform.position;
            enemyChar.animator.SetBool("isMoving", false);
        }

        //Beginning charged attack
        if (!firstBigHitDone && enemyChar.GetHealth() <= enemyChar.GetMaxHealth() / 2)
        {
            firstBigHitDone = true;
            enemyChar.animator.SetTrigger("dash");
            enemyChar.animator.SetBool("charging", true);
            enemyChar.animator.SetBool("Attacking", false);
        }

        //Beginning charged attack again
        if (!secondBigHitDone && enemyChar.GetHealth() <= enemyChar.GetMaxHealth() / 4)
        {
            secondBigHitDone = true;
            enemyChar.animator.SetTrigger("dash");
            enemyChar.animator.SetBool("charging", true);
            enemyChar.animator.SetBool("Attacking", false);
        }

        //Movement
        if (canMove == true && enemyChar.animator.GetBool("Attacking") == false)
        {
            enemyRB.velocity = Vector2.zero;

            DistanceFromPlayer = Vector2.Distance(this.transform.position, Player.transform.position);
            if ((DistanceFromPlayer <= followRange && DistanceFromPlayer > attackRange) /*&& (PlayerController.isfrozen == false)*/)
            {
                path.maxSpeed = moveSpeed;

                path.destination = PlayerRB.transform.position;

                //enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, PlayerRB.transform.position, moveSpeed * Time.deltaTime);


                //EnemyRB.transform.position = Vector2.MoveTowards(EnemyRB.transform.position, PlayerRB.transform.position, Speed * Time.deltaTime);

                //Animations
                enemyChar.animator.SetBool("isMoving", true);

                #region Directional Animating
                    

                float xDistance = Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(Player.transform.position.x));
                float yDistance = Mathf.Abs(Mathf.Abs(this.transform.position.y) - Mathf.Abs(Player.transform.position.y));

                //if the enemy is to the right of the player.
                if (this.transform.position.x > Player.transform.position.x)
                {
                    enemyChar.animator.SetFloat("moveX", -1);
                }
                else //enemy is to the left of the player
                {
                    enemyChar.animator.SetFloat("moveX", 1);
                }

                //if the enemy is above the player.
                if (this.transform.position.y > Player.transform.position.y)
                {
                    enemyChar.animator.SetFloat("moveY", -1);
                }
                else //enemy is below the player
                {
                    enemyChar.animator.SetFloat("moveY", 1);
                }

                //Seeing whether the enemy is closer on the x or y coordinate
                //Need to figure out a better way of doing this
                if (xDistance > yDistance)
                {
                    enemyChar.animator.SetFloat("moveY", 0);
                }
                else if (xDistance < yDistance)
                {
                    enemyChar.animator.SetFloat("moveX", 0);
                }
                else //if the distances are the same
                {
                    Debug.Log("X and y distances are the same");
                }
                #endregion
            }
            //Attacking
            else if (DistanceFromPlayer <= attackRange)
            {
                if (cooldown.isCoolingDown) return;



                canMove = false;

                #region Somehow works
                //Getting the distances between the x and y coordinates
                float xDistance = Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(Player.transform.position.x));
                float yDistance = Mathf.Abs(Mathf.Abs(this.transform.position.y) - Mathf.Abs(Player.transform.position.y));

                //if the enemy is to the right of the player.
                if (this.transform.position.x > Player.transform.position.x)
                {
                    enemyChar.animator.SetFloat("moveX", -1);
                }
                else //enemy is to the left of the player
                {
                    enemyChar.animator.SetFloat("moveX", 1);
                }

                //if the enemy is above the player.
                if (this.transform.position.y > Player.transform.position.y)
                {
                    enemyChar.animator.SetFloat("moveY", -1);
                }
                else //enemy is below the player
                {
                    enemyChar.animator.SetFloat("moveY", 1);
                }

                //Seeing whether the enemy is closer on the x or y coordinate
                //Need to figure out a better way of doing this
                if (xDistance > yDistance)
                {
                    enemyChar.animator.SetFloat("moveY", 0);
                }
                else if (xDistance < yDistance)
                {
                    enemyChar.animator.SetFloat("moveX", 0);
                }
                else //if the distances are the same
                {
                    Debug.Log("X and y distances are the same");
                }

                //if the player is horizontal to dark leora
                if (enemyChar.animator.GetFloat("moveX") != 0)
                {
                    if (yDistance > 1)
                    {
                        Vector2 movePostion = new Vector2(this.transform.position.x, PlayerRB.transform.position.y);

                        path.maxSpeed = moveSpeed;

                        path.destination = movePostion;


                        //enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, movePostion, moveSpeed * Time.deltaTime);

                        return;
                    }
                }
                //if the player is vertical to dark leora
                else if (enemyChar.animator.GetFloat("moveY") != 0)
                {
                    if (xDistance > 1)
                    {
                        Vector2 movePostion = new Vector2(PlayerRB.transform.position.x, this.transform.position.y);

                        path.maxSpeed = moveSpeed;

                        path.destination = movePostion;

                        //enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, movePostion, moveSpeed * Time.deltaTime);

                        return;
                    }
                }

                #endregion

                enemyRB.velocity = Vector2.zero;
                path.maxSpeed = 0;

                path.destination = this.transform.position;

                enemyChar.animator.SetBool("Attacking", true);

                cooldown.cooldownTime = defaultCooldown;
                cooldown.StartCooldown();

            }
        }
    }

    public void enableScreenFlash()
    {
        screenFlash.SetActive(true);

        if (firstBigHitDone && !secondBigHitDone) 
        {
            fstBigMoveOver = true;
        }
        else
        {
            sndBigMoveOver = true;
        }
    }

    public void DisabledChargeAnim()
    {
        enemyChar.animator.SetBool("charging", false);
        enemyChar.animator.SetBool("endCharge", false);
        
    }

    public void DisableScreenFlash()
    {
        parrying = false;
        parryIndicator.SetActive(false);
        screenFlash.SetActive(false);
    }

    public void TriggerMassiveAttack()
    {
        massiveHitbox.SetActive(true);
    }

    public void DisableMassiveHitbox()
    {
        massiveHitbox.SetActive(false);
    }
}
