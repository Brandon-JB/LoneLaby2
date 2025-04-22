using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JaelChar : BaseChar
{
    [SerializeField] private JaelScript jaelScript;

    // Start is called before the first frame update
    void Start()
    {
        charName = "Jael";
        allied = false;

        ChangeStats(11, 0, 5, 4, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (animator.GetBool("stunned") && !stunTimer.isCoolingDown)
        {
            animator.SetBool("stunned", false);

        }
    }

    public override void TriggerHurtAnim()
    {
        //This is here to stop the hurt animation from trying to trigger
    }

    public override void Death()
    {
        if (jaelScript.specialAttacking)
        {
            audioManager.Instance.playSFX(18);

            mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("Endings/Condemn/finishCondemn");
            //I don't think I have to do anything else for this, but I can modify this.

            //killSpareMenu.SetActive(true);
            //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
            //killSpare.bossName = "Lucan";
            Destroy(this.gameObject);
        }
        else
        {
            //Prevents boss from dying if they haven't done their final move yet
            SetHealth(Mathf.Clamp(GetHealth(), 1, GetMaxHealth()));
        }
    }

    public override void GotDamaged(int incomingDamage, GameObject otherAttacker, float stMod)
    {
        base.GotDamaged(incomingDamage, otherAttacker, stMod);

        if (!jaelScript.firstPhase)
        {
            SetHealth(Mathf.Clamp(GetHealth(), GetMaxHealth() - (GetMaxHealth() / 4), GetMaxHealth()));
        }

        if (!jaelScript.secondPhase)
        {
            SetHealth(Mathf.Clamp(GetHealth(), GetMaxHealth() / 2, GetMaxHealth()));
        }
    }
}
