using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpParticleScript : MonoBehaviour
{
    public void KillSelf()
    {
        Destroy(this.gameObject);
    }
}
