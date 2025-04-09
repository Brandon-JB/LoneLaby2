using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinScript : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*Severin Combat loop:
     * 1. Follow player around like a normal enemy and attack
     * 2. At 50/25%, start the big attack
     * 3. If the player hits Severin during the windup, then she does the charged attack early and it's unparriable
     * 4. After attack charges up, severin does the big attack which needs to be parried
     * 5. After this, severin gets stunned for several seconds
    */

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
