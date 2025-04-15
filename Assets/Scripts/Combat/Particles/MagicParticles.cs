using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicParticles : MonoBehaviour
{
    public Animator animator;

    public Cooldown lifetime = new Cooldown();

    public bool startedAnimating = false;

    private void Start()
    {
        //Debug.Log("Spawned");
        //startedAnimating = false;

        /*if (!lifetime.isCoolingDown)
        {
            lifetime.StartCooldown();
        }*/
    }

    private void Update()
    {
        if (!lifetime.isCoolingDown && startedAnimating == true)
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

    public void PlayMagSFX(int id)
    {
        audioManager.Instance.playSFX(id);
    }
}
