using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInteraction : MonoBehaviour
{
    [SerializeField] protected float interactRange = 3f;
    [SerializeField] protected float DistanceBetweenObjectAndPlayer;
    public LeoraChar2 leoraChar;
    [SerializeField] protected GameObject Player;


    // Update is called once per frame
    public virtual void Update()
    {
        DistanceBetweenObjectAndPlayer = Vector2.Distance(transform.position, Player.transform.position);

        /*if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (InputManager.interactPressed)
            {
                Put interactions here
            }
        }*/
    }

    private void Awake()
    {
        leoraChar = FindObjectOfType<LeoraChar2>();
        Player = leoraChar.gameObject;
    }
}
