using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public BaseChar enemyChar;

    [SerializeField] public GameObject Player;

    [SerializeField] public float attackRange;

    private void Awake()
    {
        enemyChar = GetComponent<BaseChar>();

        Player = GameObject.Find("CombatPlayer");
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= attackRange)
        {
            if (Player.transform.position.y > this.transform.position.y)
            {
                Debug.Log("Below player");
            }
            else
            {
                Debug.Log("Above player");
                enemyChar.animator.SetBool("Attacking", true);
            }

            if (Player.transform.position.x > this.transform.position.x)
            {
                Debug.Log("Left of player");
            }
            else
            {
                Debug.Log("Right of player");
            }
        }
    }
}
