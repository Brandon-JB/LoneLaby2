using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Shield";
        allied = false;

        ChangeStats(6, 0, 9, 35, 0);
    }

}
