using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvarChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        allied = false;
        charName = "Ivar";

        ChangeStats(10, 0, 4, 100, 0);
    }
}
