using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "TreeMimic";
        allied = false;

        ChangeStats(11, 0, 7, 30, 0);
    }
}
