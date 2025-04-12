using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvarProjectile : HitboxChar
{
    [SerializeField] Rigidbody2D rb;

    private GameObject Player;

    [SerializeField] private GameObject spriteObject;

    private Vector2 lastPlayerPosition;
    private bool launched = false;

    [SerializeField] private Cooldown timeTracking;

    [SerializeField] private Cooldown lifespan;

    public float launchSpeed;
    public float trackSpeed;

    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lifespan.StartCooldown();
        timeTracking.StartCooldown();

        isParryable = false;
        Player = GameObject.Find("CombatPlayer");

        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Tracks the player
        if (timeTracking.isCoolingDown)
        { 
            rb.transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, trackSpeed * Time.deltaTime);
            Vector3 playerPos = Player.transform.position - transform.position;
            spriteObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.normalized);

            if (trackSpeed < maxSpeed)
            {
                trackSpeed += 0.005f;
            }
        }
        //Launches in a line
        else if (!timeTracking.isCoolingDown)
        {
            if (launched == false)
            {
                lastPlayerPosition = (Player.transform.position - transform.position).normalized;
                launched = true;
            }

            rb.AddForce(lastPlayerPosition * launchSpeed * 200, ForceMode2D.Force);
        }

        if (!lifespan.isCoolingDown)
        {
            Destroy(this.gameObject);
        }
    }
}
