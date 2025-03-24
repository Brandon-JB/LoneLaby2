using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : EnemyScript
{
    [SerializeField] private BoxCollider2D hurtbox;
    private bool ableToMove = false;

    [SerializeField] private GameObject warningAoE;

    public void EnableWarning()
    {
        warningAoE.SetActive(true);
    }

    public void DisableWarning()
    {
        warningAoE.SetActive(false);
    }

    public void EnableHurtbox()
    {
        hurtbox.enabled = true;
    }

    public void DisableHurtbox()
    {
        hurtbox.enabled = false;
    }

    public void DisableMovement()
    {
        canMove = false;
        ableToMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
        ableToMove = true;
    }

    public void StartWalk()
    {
        enemyChar.animator.SetBool("StartWalk", true);
    }

    public void EndWalk()
    {
        enemyChar.animator.SetBool("StartWalk", false);
        DisableMovement();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
    }

    public override void Update()
    {
        if (DistanceFromPlayer > followRange || canMove == false || enemyChar.animator.GetBool("Hurt"))
        {
            enemyChar.animator.SetBool("isMoving", false);
        }

        if (!cooldown.isCoolingDown && enemyChar.stunTimer.isCoolingDown == false && enemyChar.animator.GetBool("Hurt") == false && ableToMove == true)
        {
            canMove = true;
        }
        else// if (enemyChar.animator.GetBool("Hurt") == true)
        {
            canMove = false;
        }

        //Debug.Log("Enemy is existing");

        if (enemyChar.animator.GetBool("StartWalk") == false)
        {
            DistanceFromPlayer = Vector3.Distance(this.transform.position, Player.transform.position);

            if (DistanceFromPlayer <= followRange && !cooldown.isCoolingDown && enemyChar.animator.GetBool("Hurt") == false && !enemyChar.stunTimer.isCoolingDown) //&& DistanceFromPlayer > attackRange))
            {
                StartWalk();
            }
            else
            {
                EndWalk();
            }
        }

        //Movement
        if (canMove == true && enemyChar.animator.GetBool("Attacking") == false)
        {
            enemyRB.velocity = Vector2.zero;

            DistanceFromPlayer = Vector3.Distance(this.transform.position, Player.transform.position);
            if ((DistanceFromPlayer <= followRange && (DistanceFromPlayer > attackRange)) /*&& (PlayerController.isfrozen == false)*/)
            {

                enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, PlayerRB.transform.position, moveSpeed * Time.deltaTime);


                //EnemyRB.transform.position = Vector2.MoveTowards(EnemyRB.transform.position, PlayerRB.transform.position, Speed * Time.deltaTime);

                //Animations
                enemyChar.animator.SetBool("isMoving", true);

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

                /*if (movementInput.x > .5) movementInput.y = 0;
                if (movementInput.y > .5) movementInput.x = 0;
                if (movementInput.x < -.5) movementInput.y = 0;
                if (movementInput.y < -.5) movementInput.x = 0;*/

                if (movementInput != Vector2.zero)
                {
                    enemyChar.animator.SetFloat("moveX", movementInput.x);
                    enemyChar.animator.SetFloat("moveY", movementInput.y);
                }

                enemyChar.animator.SetFloat("moveX", movementInput.x);
                enemyChar.animator.SetFloat("moveY", movementInput.y);
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

                /*
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
                }*/

                #endregion

                enemyChar.animator.SetBool("Attacking", true);

                cooldown.StartCooldown();

            }
        }
    }
}
