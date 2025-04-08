using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkLeoraScript : EnemyScript
{
    [SerializeField] LeoraChar2 leoraChar;

    [SerializeField] private GameObject magicParticle;

    private GameObject tempMagPart;

    // Start is called before the first frame update
    void Start()
    {
        leoraChar = FindObjectOfType<LeoraChar2>();
    }

    public void SpawnDarkParticle()
    {
        tempMagPart = Instantiate(magicParticle, this.transform.position, Quaternion.identity, this.transform);

        MagicParticles tempMagManager = tempMagPart.GetComponent<MagicParticles>();

        //Animator for particles would go here
        tempMagManager.animator.SetBool("darkMag", true);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (DistanceFromPlayer > followRange || canMove == false)
        {
            enemyChar.animator.SetBool("isMoving", false);
        }

        if (!cooldown.isCoolingDown && enemyChar.animator.GetBool("Hurt") == false && enemyChar.stunTimer.isCoolingDown == false)
        {
            canMove = true;
        }
        else// if (enemyChar.animator.GetBool("Hurt") == true)
        {
            canMove = false;
        }

        //Debug.Log("Enemy is existing");

        //Movement
        if (canMove == true && enemyChar.animator.GetBool("Attacking") == false)
        {
            enemyRB.velocity = Vector2.zero;

            DistanceFromPlayer = Vector2.Distance(this.transform.position, Player.transform.position);
            if ((DistanceFromPlayer <= followRange && DistanceFromPlayer > attackRange) /*&& (PlayerController.isfrozen == false)*/)
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
                    enemyChar.animator.SetFloat("Horizontal", movementInput.x);
                    enemyChar.animator.SetFloat("Vertical", movementInput.y);
                }

                enemyChar.animator.SetFloat("Horizontal", movementInput.x);
                enemyChar.animator.SetFloat("Vertical", movementInput.y);
                */

                float xDistance = Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(Player.transform.position.x));
                float yDistance = Mathf.Abs(Mathf.Abs(this.transform.position.y) - Mathf.Abs(Player.transform.position.y));

                //if the enemy is to the right of the player.
                if (this.transform.position.x > Player.transform.position.x)
                {
                    enemyChar.animator.SetFloat("Horizontal", -1);
                }
                else //enemy is to the left of the player
                {
                    enemyChar.animator.SetFloat("Horizontal", 1);
                }

                //if the enemy is above the player.
                if (this.transform.position.y > Player.transform.position.y)
                {
                    enemyChar.animator.SetFloat("Vertical", -1);
                }
                else //enemy is below the player
                {
                    enemyChar.animator.SetFloat("Vertical", 1);
                }

                //Seeing whether the enemy is closer on the x or y coordinate
                //Need to figure out a better way of doing this
                if (xDistance > yDistance)
                {
                    enemyChar.animator.SetFloat("Vertical", 0);
                }
                else if (xDistance < yDistance)
                {
                    enemyChar.animator.SetFloat("Horizontal", 0);
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
                    enemyChar.animator.SetFloat("Horizontal", -1);
                }
                else //enemy is to the left of the player
                {
                    enemyChar.animator.SetFloat("Horizontal", 1);
                }

                //if the enemy is above the player.
                if (this.transform.position.y > Player.transform.position.y)
                {
                    enemyChar.animator.SetFloat("Vertical", -1);
                }
                else //enemy is below the player
                {
                    enemyChar.animator.SetFloat("Vertical", 1);
                }

                //Seeing whether the enemy is closer on the x or y coordinate
                //Need to figure out a better way of doing this
                if (xDistance > yDistance)
                {
                    enemyChar.animator.SetFloat("Vertical", 0);
                }
                else if (xDistance < yDistance)
                {
                    enemyChar.animator.SetFloat("Horizontal", 0);
                }
                else //if the distances are the same
                {
                    Debug.Log("X and y distances are the same");
                }

                //if the player is horizontal to dark leora
                if (enemyChar.animator.GetFloat("Horizontal") != 0)
                {
                    if (yDistance > 1)
                    {
                        Vector2 movePostion = new Vector2 (this.transform.position.x, PlayerRB.transform.position.y);

                        enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, movePostion, moveSpeed * Time.deltaTime);

                        return;
                    }
                }
                //if the player is vertical to dark leora
                else if (enemyChar.animator.GetFloat("Vertical") != 0)
                {
                    if (xDistance > 1)
                    {
                        Vector2 movePostion = new Vector2(PlayerRB.transform.position.x, this.transform.position.y);

                        enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, movePostion, moveSpeed * Time.deltaTime);

                        return;
                    }
                }

                #endregion

                int actionChoice = 0;

                if (leoraChar.animator.GetBool("Attacking"))
                {
                    actionChoice = 1;
                }
                else if (leoraChar.animator.GetBool("Magicing"))
                {
                    actionChoice = 2;
                }

                switch (actionChoice)
                {
                    case 0:
                        enemyChar.animator.SetBool("Attacking", true);
                        cooldown.cooldownTime = 2;
                        break;
                    case 1:
                        enemyChar.animator.SetBool("Parrying", true);
                        cooldown.cooldownTime = 0.5f;
                        break;
                    case 2:
                        enemyChar.animator.SetBool("Magicing", true);
                        cooldown.cooldownTime = 3;
                        break;
                }
                cooldown.StartCooldown();

            }
        }
    }
}
