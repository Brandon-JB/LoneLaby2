using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : EnemyScript
{
    public bool thrusting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Exact same as normal attack script, but similar to tree where the enemy can only attack left or right
    public override void Update()
    {
        if (DistanceFromPlayer > followRange)
        {
            //enemyChar.animator.SetBool("isMoving", false);
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

            Vector2 movePosition;

            float VerticalDistance = Mathf.Abs(this.transform.position.y - Player.transform.position.y);

            DistanceFromPlayer = Vector2.Distance(this.transform.position, Player.transform.position);

            if ((DistanceFromPlayer <= followRange && (DistanceFromPlayer > attackRange || VerticalDistance > 1)) /*&& (PlayerController.isfrozen == false)*/)
            {
                if (Player.transform.position.x > transform.position.x)
                {
                    movePosition = new Vector2(PlayerRB.transform.position.x - 2, PlayerRB.transform.position.y);
                }
                else
                {
                    movePosition = new Vector2(PlayerRB.transform.position.x + 2, PlayerRB.transform.position.y);
                }

                enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, movePosition, moveSpeed * Time.deltaTime);

                //EnemyRB.transform.position = Vector2.MoveTowards(EnemyRB.transform.position, PlayerRB.transform.position, Speed * Time.deltaTime);

                //Animations
                //enemyChar.animator.SetBool("isMoving", true);

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
                    //enemyChar.animator.SetFloat("moveY", movementInput.y);
                }

                enemyChar.animator.SetFloat("moveX", movementInput.x);
                //enemyChar.animator.SetFloat("moveY", movementInput.y);
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

        if (thrusting)
        {
            Vector2 slidePosition = new Vector2(enemyRB.transform.position.x + (1 * enemyChar.animator.GetFloat("moveX")), enemyRB.transform.position.y);

            if (enemyChar.animator.GetFloat("moveX") < 0)
            {
                enemyRB.velocity = 15 * -slidePosition.normalized;
            }
            else if (enemyChar.animator.GetFloat("moveX") > 0)
            {
                enemyRB.velocity = 15 * slidePosition.normalized;
            }

            //enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, slidePosition, moveSpeed / 2 * Time.deltaTime);
        }
        /*else if (enemyChar.animator.GetBool("Attacking") && !thrusting)
        {
            enemyRB.velocity = Vector2.zero;
        }*/
    }

    public void ToggleThrust()
    {
        if (thrusting == true)
        {
            enemyRB.velocity = Vector2.zero;
        }
        thrusting = !thrusting;
    }
}
