using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        allied = false;
        charName = "Spirit";

        ChangeStats(9, 0, 2, 60, 0);
    }

    public override void TriggerHurtAnim()
    {
        QuestManager.AddProgress("Sophie", 1);

        base.TriggerHurtAnim();

        animator.SetBool("StartWalk", false);
    }
}
