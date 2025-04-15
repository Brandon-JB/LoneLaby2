using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaelChar : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        charName = "Jael";
        allied = false;

        ChangeStats(11, 0, 5, 380, 0);
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

    public override void Death()
    {
        mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();
        mdm.dialogueSTART("Endings/Condemn/finishCondemn");
        //I don't think I have to do anything else for this, but I can modify this.

        //killSpareMenu.SetActive(true);
        //killSpareManager killSpare = killSpareMenu.GetComponent<killSpareManager>();
        //killSpare.bossName = "Lucan";
        Destroy(this.gameObject);
    }
}
