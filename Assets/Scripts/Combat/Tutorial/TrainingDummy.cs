using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    public Transform damagePopup;

    public MagicParticles particleManager;

    public Tutorial tutorialScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            Transform damagePopupTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
            DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
            damPopScript.SetupInt(0, "Damage");

            if (tutorialScript.tutorialCounter == 2)
            {
                tutorialScript.progressTutorial();
            }
        }
        else if (collision.tag == "Magick")
        {
            SpawnParticle("stunFX", this.transform.position, this.transform, 1.5f);

            Transform damagePopupTransform = Instantiate(damagePopup, transform.position, Quaternion.identity);
            DamagePopUp damPopScript = damagePopupTransform.GetComponent<DamagePopUp>();
            damPopScript.SetupInt(0, "Damage");

            if (tutorialScript.tutorialCounter == 4)
            {
                tutorialScript.progressTutorial();
            }
        }
    }

    public void SpawnParticle(string particleName, Vector3 position, Transform parentTransform, float lifetime)
    {
        MagicParticles particle = Instantiate(particleManager, position, Quaternion.identity, parentTransform);
        particle.lifetime.cooldownTime = lifetime;
        particle.lifetime.StartCooldown();
        particle.animator.SetBool(particleName, true);
        particle.startedAnimating = true;
    }
}
