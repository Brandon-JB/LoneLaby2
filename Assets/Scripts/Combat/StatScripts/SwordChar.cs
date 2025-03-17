using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Sword";
        allied = false;

        ChangeStats(13, 0, 1, 40, 0);
    }
}
