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

        ChangeStats(9, 0, 4, 40, 0);
    }

}
