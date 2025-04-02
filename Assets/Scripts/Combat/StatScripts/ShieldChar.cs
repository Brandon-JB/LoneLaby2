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

        ChangeStats(7, 0, 9, 35, 0);
    }

}
