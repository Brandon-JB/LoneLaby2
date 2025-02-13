using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticles : MonoBehaviour
{
    public Animator animator;

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
