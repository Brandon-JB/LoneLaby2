using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaelChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Jael";
        allied = false;

        ChangeStats(11, 0, 5, 300, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (animator.GetBool("stunned") && !stunTimer.isCoolingDown)
        {
            animator.SetBool("stunned", false);

        }
    }
}
