using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public BaseChar enemyChar;

    [SerializeField] public Rigidbody2D PlayerRB;
    public GameObject Player;


    [SerializeField] public float attackRange;
    [SerializeField] public float followRange = 10f;
    float DistanceFromPlayer;

    private Vector2 movementInput;
    [SerializeField] private float moveSpeed = 4f;

    private Rigidbody2D enemyRB;

    [SerializeField] public Cooldown cooldown;


    public bool canMove = true;

    private void Awake()
    {
        enemyChar = GetComponent<BaseChar>();

        enemyRB = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("CombatPlayer");

        PlayerRB = Player.GetComponent<Rigidbody2D>();

        canMove = true;
    }

    private void Update()
    {
        if (DistanceFromPlayer > followRange)
        {
            enemyChar.animator.SetBool("isMoving", false);
        }

        if (!cooldown.isCoolingDown)
        {
            canMove = true;
        }

        //Debug.Log("Enemy is existing");

        //Movement
        DistanceFromPlayer = Vector3.Distance(this.transform.position, Player.transform.position);

        if (canMove == true)
        {
            if ((DistanceFromPlayer <= followRange && DistanceFromPlayer > attackRange) /*&& (PlayerController.isfrozen == false)*/)
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

                if (movementInput.x > .5) movementInput.y = 0;
                if (movementInput.y > .5) movementInput.x = 0;

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
                //Getting the distances between the x and y coordinates
                float xDistance = Mathf.Abs(this.transform.position.x) - Mathf.Abs(Player.transform.position.x);
                float yDistance = Mathf.Abs(this.transform.position.y) - Mathf.Abs(Player.transform.position.y); ;

                //Seeing whether the enemy is closer on the x or y coordinate
                //Need to figure out a better way of doing this
                if (xDistance < yDistance)
                {
                    enemyChar.animator.SetFloat("moveY", 0);
                }
                else if (xDistance > yDistance) 
                {
                    enemyChar.animator.SetFloat("moveX", 0);
                }
                else //if the distances are the same
                {
                    Debug.Log("X and y distances are the same");
                }

                enemyChar.animator.SetBool("Attacking", true);
                
                cooldown.StartCooldown();

            }
        }
    }

    public void AllowMovement()
    {
        canMove = true;
    }

    public void OnDisable()
    {
        canMove = false;
    }
}
