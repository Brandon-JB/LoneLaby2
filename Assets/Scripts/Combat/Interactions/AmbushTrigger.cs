using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrigger : CombatInteraction
{
    public List<EnemyScript> ambushingScripts = new List<EnemyScript>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (InputManager.interactPressed)
            {
                Debug.Log("Die");
                foreach (var enemy in ambushingScripts)
                {
                    enemy.followRange = 10000f;
                }
            }
        }
    }
}
