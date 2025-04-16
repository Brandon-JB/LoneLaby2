using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossInteraction : MonoBehaviour
{
    [SerializeField] protected float interactRange = 3f;
    [SerializeField] protected float DistanceBetweenObjectAndPlayer;
    [SerializeField] protected GameObject Player;

    public string sceneName;


    // Update is called once per frame
    public virtual void Update()
    {
        DistanceBetweenObjectAndPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (InputManager.interactPressed)
            {
                //Put interactions here
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void Awake()
    {
        Player = FindObjectOfType<NonCombatPlayerMovement>().gameObject;
    }
}
