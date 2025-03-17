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

        ChangeStats(7, 0, 2, 60, 0);
    }
}
