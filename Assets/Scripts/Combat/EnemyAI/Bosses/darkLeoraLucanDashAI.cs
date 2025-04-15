using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darkLeoraLucanDashAI : BaseChar
{
    [SerializeField] private Vector2 bottomLeftArenaBounds;
    [SerializeField] private Vector2 topRightArenaBounds;

    [SerializeField] private Cooldown timeBetweenDashes;

    [SerializeField] public GameObject leftDashArrow;
    [SerializeField] public GameObject rightDashArrow;

    [SerializeField] private Image leftArrowSprite;
    [SerializeField] private Image rightArrowSprite;

    [SerializeField] private Rigidbody2D enemyRB;

    private Vector2 dashTarget;
    [SerializeField] private int dashSpeed;

    private GameObject Player;




    // Start is called before the first frame update
    void Start()
    {
        leftDashArrow = GameObject.Find("LeftArrow");
        rightDashArrow = GameObject.Find("RightArrow");

        leftArrowSprite = leftDashArrow.GetComponent<Image>();
        rightArrowSprite = rightDashArrow.GetComponent<Image>();

        leftArrowSprite.enabled = false;
        rightArrowSprite.enabled = false;

        animator.SetBool("lucanDash", true);

        Player = GameObject.Find("CombatPlayer");

        charName = "Lucora";

        allied = false;

        ChangeStats(15, 0, 6, 1, 0);

        dashTarget = bottomLeftArenaBounds;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!timeBetweenDashes.isCoolingDown)
        {
            //If Lucan hasn't reached is target yet
            if (Vector2.Distance(enemyRB.transform.position, dashTarget) > 1 && animator.GetBool("stunned") == false) 
            {
                leftArrowSprite.enabled = false;
                rightArrowSprite.enabled = false;
                enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, dashTarget, dashSpeed * Time.deltaTime);
            }
            else
            {

                timeBetweenDashes.StartCooldown();
                //Debug.Log("Reached end");

                ResetHitbox();

                this.transform.position = new Vector2(this.transform.position.x, Player.transform.position.y);

                //IIf the enemy is closer to the left side of arena than the right side.
                if (Vector2.Distance(enemyRB.transform.position, bottomLeftArenaBounds) < Vector2.Distance(enemyRB.transform.position, topRightArenaBounds))
                {
                    dashTarget = new Vector2(topRightArenaBounds.x, Player.transform.position.y);
                    //leftArrowSprite.enabled = true;
                    //Debug.Log("Change side");
                    animator.SetFloat("moveX", 1);
                }
                else
                {
                    dashTarget = new Vector2(bottomLeftArenaBounds.x, Player.transform.position.y);
                    //rightArrowSprite.enabled = true;
                    //Debug.Log("Change side");
                    animator.SetFloat("moveX", -1);

                }/* This part is literally useless why did i have it here
                    else
                    {
                        Debug.Log("Reached end");

                        timeBetweenDashes.StartCooldown();

                        ResetHitbox();

                            

                        this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, this.transform.position.z);

                        //IIf the enemy is closer to the left side of arena than the right side.
                        if (Mathf.Abs(enemyRB.transform.position.x - bottomLeftArenaBounds.x) < Mathf.Abs(enemyRB.transform.position.x - topRightArenaBounds.x))
                        {
                            dashTarget = new Vector2(topRightArenaBounds.x, Player.transform.position.y);
                            Debug.Log("Change side");
                            animator.SetFloat("moveX", 1);
                        }
                        else
                        {
                            dashTarget = new Vector2(bottomLeftArenaBounds.x, Player.transform.position.y);
                            Debug.Log("Change side");
                            animator.SetFloat("moveX", -1);
                        }
                    }*/
            }
        }
        else
        {
            //if (Vector2.Distance(enemyRB.transform.position, bottomLeftArenaBounds) < Vector2.Distance(enemyRB.transform.position, topRightArenaBounds))
            if (!animator.GetBool("stunned"))
            {
                if (Mathf.Abs(Mathf.Abs(enemyRB.transform.position.x) - Mathf.Abs(bottomLeftArenaBounds.x)) < Mathf.Abs(Mathf.Abs(enemyRB.transform.position.x) - Mathf.Abs(topRightArenaBounds.x)))
                {
                    //leftDashArrow.SetActive(true);
                    leftArrowSprite.enabled = true;
                    rightArrowSprite.enabled = false;
                }
                else
                {
                    //rightDashArrow.SetActive(true);
                    rightArrowSprite.enabled = true;
                    leftArrowSprite.enabled = false;
                }

                this.transform.position = new Vector2(this.transform.position.x, Player.transform.position.y);
                dashTarget.y = Player.transform.position.y;
            }
        }
    }

    public override IEnumerator deathAnim()
    {

        Color c = charSprite.material.color;

        for (float f = 1; f >= 0; f -= 0.05f)
        {
            if (charSprite == null)
            {
                continue;
            }

            c.a = f;
            c.r = Random.Range(0, 1f);
            c.g = Random.Range(0, 1f);
            c.b = Random.Range(0, 1f);


            charSprite.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        dropManager.SpecificDrop(this.transform.position, "Small HP");
        ReachDestination();
        animator.enabled = true;
        charRB.constraints = RigidbodyConstraints2D.None;
        EnableHitbox();
        c.a = 1;
        c.r = 1;
        c.g = 1;
        c.b = 1;
        charSprite.color = c;
        animator.SetBool("stunned", false);
    }

    private void ReachDestination()
    {
        this.transform.position = dashTarget;
    }
}
