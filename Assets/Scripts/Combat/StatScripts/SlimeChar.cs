using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Slime";
        allied = false;

        ChangeStats(12, 0, 4, 70, 0);
    }

}
