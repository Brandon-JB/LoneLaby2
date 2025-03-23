using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvarFireWall : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        allied = false;
        charName = "Fire";
        ChangeStats(20, 0, 9999999, 9999999, 9999999);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
