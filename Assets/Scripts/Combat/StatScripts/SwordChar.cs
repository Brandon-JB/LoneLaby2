using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordChar : BaseChar
{
    public SwordScript swordScript;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Sword";
        allied = false;

        ChangeStats(13, 0, 1, 40, 0);
    }

    public override void TriggerHurtAnim()
    {
        base.TriggerHurtAnim();

        swordScript.thrusting = false;
        swordScript.enemyRB.velocity = Vector2.zero;
    }

    public override void StopHurtAnim()
    {
        base.StopHurtAnim();

        swordScript.thrusting = false;
        swordScript.enemyRB.velocity = Vector2.zero;
    }

}
