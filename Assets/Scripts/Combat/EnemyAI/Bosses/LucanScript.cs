using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucanScript : EnemyScript
{
    [SerializeField] private Vector3 bottomLeftArenaBounds;
    [SerializeField] private Vector3 topRightArenaBounds;

    private bool firstPhase;
    private bool secondPhase;

    public bool isActive;

    [Header("Dashing")]

    [SerializeField] private Cooldown dashCooldown;
    private bool isDashing;
    public BoxCollider2D hurtbox;
    private Vector2 dashTarget;
    [SerializeField] private Cooldown timeBetweenDashes;
    public Cooldown specialStunTimer;

    [SerializeField] private int dashCount;
    [SerializeField] private int dashLimit = 4;

    [SerializeField] private int dashSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isDashing = false;
        dashCount = 0;
        firstPhase = true;
        secondPhase = false;
    }

    //Have the ai walk around normally and do the mace swing while periodically doing the charge attack. As the boss's health gets lower, they do the charge attack more often
    //until they are eventually doing it nearly every attack.

    public void DisableHurtbox()
    {
        hurtbox.enabled = false;
        enemyChar.animator.SetBool("inDash", true);
        enemyChar.animator.SetBool("shieldRush", false);
    }

    public void EnableHurtbox()
    {
        //Debug.Log("Hurtbox active");
        hurtbox.enabled = true;
        enemyChar.animator.SetBool("inDash", false);
    }

    public void TriggerStunAnimation()
    {
        isDashing = false;
        dashCount = 0;
        enemyChar.DisableHitbox();
        enemyChar.SpawnParticle("stunFX", this.transform.position, this.transform, specialStunTimer.cooldownTime);
        EnableHurtbox();
        enemyChar.animator.SetBool("stunned", true);
        enemyChar.StopAttackAnim();
    }
    
    public void StopStunAnimation()
    {
        enemyChar.animator.SetBool("stunned", false);
        timeBetweenDashes.StartCooldown();
    }

    public override void Update()
    {
        if (isActive)
        {

            //Phase change
            if (firstPhase && (enemyChar.GetHealth() <= (enemyChar.GetMaxHealth() / 2)))
            {
                //SecondPhase
                firstPhase = false;
                secondPhase = true;
                dashLimit += 3;
                dashSpeed += 2;
            }
            else if (secondPhase && enemyChar.GetHealth() <= (enemyChar.GetMaxHealth() / 4))
            {
                //Final Phase
                secondPhase = false;
                dashSpeed += 4;
            }

            if (DistanceFromPlayer > followRange || DistanceFromPlayer < attackRange || isDashing)
            {
                enemyChar.animator.SetBool("isMoving", false);
            }

            if (!cooldown.isCoolingDown && enemyChar.stunTimer.isCoolingDown == false && !specialStunTimer.isCoolingDown)
            {
                canMove = true;
                //enemyChar.animator.SetBool("stunned", false);
            }
            else// if (enemyChar.animator.GetBool("Hurt") == true)
            {
                canMove = false;
            }

            //Debug.Log("Enemy is existing");

            if (isDashing && enemyChar.animator.GetBool("inDash"))
            {
                if (!timeBetweenDashes.isCoolingDown)
                {
                    //If Lucan hasn't reached is target yet
                    if (Vector2.Distance(enemyRB.transform.position, dashTarget) > 1)
                    {
                        enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, dashTarget, moveSpeed * dashSpeed * Time.deltaTime);
                    }
                    else
                    {
                        if (firstPhase || secondPhase)
                        {
                            //If lucan has dashed less than 4 times
                            if (dashCount < dashLimit)
                            {
                                //What?

                                dashCount++;
                                timeBetweenDashes.StartCooldown();

                                enemyChar.ResetHitbox();

                                this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, this.transform.position.z);

                                //IIf the enemy is closer to the left side of arena than the right side.
                                if (Vector2.Distance(enemyRB.transform.position, bottomLeftArenaBounds) < Vector3.Distance(enemyRB.transform.position, topRightArenaBounds))
                                {
                                    dashTarget = new Vector2(topRightArenaBounds.x, Player.transform.position.y);
                                    Debug.Log("Change side");
                                    enemyChar.animator.SetFloat("moveX", 1);
                                }
                                else
                                {
                                    dashTarget = new Vector2(bottomLeftArenaBounds.x, Player.transform.position.y);
                                    Debug.Log("Change side");
                                    enemyChar.animator.SetFloat("moveX", -1);
                                }
                            }
                            //Final Dash
                            else if (dashCount == dashLimit)
                            {
                                dashCount++;

                                enemyChar.ResetHitbox();

                                this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, this.transform.position.z);

                                dashTarget = new Vector2((topRightArenaBounds.x + bottomLeftArenaBounds.x) / 2, Player.transform.position.y);
                            }
                            //Reach middle
                            else if (dashCount > dashLimit)
                            {
                                dashCount = 0;

                                isDashing = false;
                                enemyChar.DisableHitbox();
                                EnableHurtbox();
                                enemyChar.StopAttackAnim();
                            }
                        }/* This part is literally useless why did i have it here
                        else
                        {
                            Debug.Log("Reached end");

                            timeBetweenDashes.StartCooldown();

                            enemyChar.ResetHitbox();

                            

                            this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, this.transform.position.z);

                            //IIf the enemy is closer to the left side of arena than the right side.
                            if (Mathf.Abs(enemyRB.transform.position.x - bottomLeftArenaBounds.x) < Mathf.Abs(enemyRB.transform.position.x - topRightArenaBounds.x))
                            {
                                dashTarget = new Vector2(topRightArenaBounds.x, Player.transform.position.y);
                                Debug.Log("Change side");
                                enemyChar.animator.SetFloat("moveX", 1);
                            }
                            else
                            {
                                dashTarget = new Vector2(bottomLeftArenaBounds.x, Player.transform.position.y);
                                Debug.Log("Change side");
                                enemyChar.animator.SetFloat("moveX", -1);
                            }
                        }*/
                    }
                }
                else
                {
                    this.transform.position = new Vector2(this.transform.position.x, Player.transform.position.y);
                    dashTarget.y = Player.transform.position.y;
                }
            }

            if (enemyChar.animator.GetBool("stunned"))
            {
                dashCooldown.StartCooldown();

                if (!specialStunTimer.isCoolingDown)
                {
                    cooldown.StartCooldown();
                    enemyChar.animator.SetBool("stunned", false);
                }
            }


            //Movement
            if (canMove == true && enemyChar.animator.GetBool("Attacking") == false)
            {
                if (!dashCooldown.isCoolingDown && !isDashing)
                {
                    dashCooldown.StartCooldown();

                    //Coin flip on whether lucan will dash
                    if (Random.Range(0, 2) == 0)
                    {
                        isDashing = true;

                        enemyChar.animator.SetBool("shieldRush", true);

                        //Lucan goes to the left side
                        if (Random.Range(0, 2) == 0)
                        {
                            //Setting dash animation and marking the side he dashes to
                            enemyChar.animator.SetFloat("moveX", -1);
                            dashTarget = new Vector2(bottomLeftArenaBounds.x, this.transform.position.y);
                        }
                        //Lucan goes to the right side
                        else
                        {
                            //Setting dash animation and marking the side he dashes to
                            enemyChar.animator.SetFloat("moveX", 1);
                            dashTarget = new Vector2(topRightArenaBounds.x, this.transform.position.y);
                        }
                    }
                    else
                    {
                        Debug.Log("Don't do dash");
                    }
                }

                enemyRB.velocity = Vector2.zero;

                DistanceFromPlayer = Vector3.Distance(this.transform.position, Player.transform.position);
                if ((DistanceFromPlayer <= followRange && DistanceFromPlayer > attackRange) /*&& (PlayerController.isfrozen == false)*/ && !isDashing)
                {
                    enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, PlayerRB.transform.position, moveSpeed * Time.deltaTime);


                    //EnemyRB.transform.position = Vector2.MoveTowards(EnemyRB.transform.position, PlayerRB.transform.position, Speed * Time.deltaTime);

                    //Animations
                    enemyChar.animator.SetBool("isMoving", true);

                    #region Directional Animating
                    /*
                    if (Player.transform.position.x > transform.position.x)
                    {
                        movementInput.x = 1;
                    }
                    else
                    {
                        movementInput.x = -1;
                    }

                    if (Player.transform.position.y > transform.position.y)
                    {
                        movementInput.y = 1;
                    }
                    else
                    {
                        movementInput.y = -1;
                    }


                    if (movementInput != Vector2.zero)
                    {
                        enemyChar.animator.SetFloat("moveX", movementInput.x);
                        enemyChar.animator.SetFloat("moveY", movementInput.y);
                    }

                    enemyChar.animator.SetFloat("moveX", movementInput.x);
                    enemyChar.animator.SetFloat("moveY", movementInput.y);
                    */

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

                    #endregion

                    enemyChar.animator.SetBool("Attacking", true);

                    cooldown.StartCooldown();

                }
            }
        }
    }
}
