using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverinChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Severin";
        allied = false;

        ChangeStats(16, 0, 8, 300, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
