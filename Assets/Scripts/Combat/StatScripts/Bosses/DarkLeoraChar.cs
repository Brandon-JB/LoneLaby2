using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLeoraChar : BaseChar
{

    //Variable for the combo window.
    //If the combo timer is "cooling down" then that just means it's in the combo window.
    [SerializeField] public Cooldown comboTimer = new Cooldown();

    //Variable for the cooldown time.
    [SerializeField] public Cooldown attackCooldown = new Cooldown();

    [SerializeField] public Cooldown magicCooldown = new Cooldown();

    [SerializeField] private GameOverAndUI gameOverManager;
    [SerializeField] private DarkLeoraScript dLeoraScript;

    [SerializeField] private CombatPlayerMovement playerMovement;

    [SerializeField] private GameObject magHitbox;

    [SerializeField] private GameObject darkMagFollowup;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private float projectileSpawnOffset;

    public bool secondPhaseActive;

    public string magicType = "";

    //[SerializeField] private BoxCollider2D hurtbox;

    private bool hyperArmor;

    //For Darkness
    private DarknessManager darknessManager;

    private string outlierBossDialogue;
    private int outlierNum;

    //public bool isInFirstCombo = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing Leora
        charName = "DarkLeora";
        allied = false;
        magicType = "darkMag";

        ChangeStats(10, 10, 4, 350, 10);

        animator.SetFloat("LastH", 0);
        animator.SetFloat("LastV", -1);

        magHitbox.SetActive(false);
        darknessManager = GameObject.FindObjectOfType<DarknessManager>();

        outlierBossDialogue = FindOutlierBoss();
        if (outlierBossDialogue == "You shouldn't be here.")
        {
            Debug.Log(outlierBossDialogue);
            Application.Quit();
        }
    }

    private string FindOutlierBoss()
    {
        int sum = 0;

        foreach(var boss in BossSaveData.bossStates)
        {
            Debug.Log(boss.Key + ": " + boss.Value);
            sum += boss.Value;
        }

        Debug.Log(sum);

        switch (sum)
        {
            case 4:
                foreach(var boss in BossSaveData.bossStates)
                {
                    if (boss.Value == 2)
                    {
                        outlierNum = boss.Value;
                        return boss.Key;
                    }
                }
                break;
            case 5:
                foreach (var boss in BossSaveData.bossStates)
                {
                    if (boss.Value == 1)
                    {
                        outlierNum = boss.Value;
                        return boss.Key;
                    }
                }
                break;
            default:
                return "You shouldn't be here.";
        }

        return "You shouldn't be here.";
    }

    public override void TriggerHurtAnim()
    {
        if (!hyperArmor)
        {
            base.TriggerHurtAnim();
            isParrying = false;
            isPerfectParrying = false;
            EnableHurtbox();

            DisableMagHitbox();
        }
    }

    public void ToggleHyperArmor()
    {
        hyperArmor = !hyperArmor;
    }

    public override void StopHurtAnim()
    {
        base.StopHurtAnim();

        if (hyperArmor)
        {
            ToggleHyperArmor();
        }

        isParrying = false;
        isPerfectParrying = false;

        DisableMagHitbox();
    }

    public override void Update()
    {
        base.Update();

        //Once the combo timer ends, then set the combo bool in the animator to false;
        if (!comboTimer.isCoolingDown)
        {
            animator.SetBool("isInCombo", false);
        }

        if (Time.timeScale == 0)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        BaseChar otherCharTrigger = null;

        HitboxChar hitboxChild = null;

        //Debug.Log(collision.gameObject.name + " Triggered " + this.gameObject.name);

        if (collision.tag == "Hitbox")
        {
            otherCharTrigger = collision.GetComponent<BaseChar>();

            //Debug.Log("Hitbox triggered");

            if (otherCharTrigger == null)
            {
                hitboxChild = collision.GetComponent<HitboxChar>();

                if (hitboxChild == null)
                {
                    hitboxChild = collision.GetComponentInParent<HitboxChar>();
                    Debug.Log("Child not found");
                }

                otherCharTrigger = hitboxChild.parentChar;

                if (otherCharTrigger == null)
                {
                    Debug.Log("Unable to find parent character of hitbox");
                }
            }

            if (otherCharTrigger != null)
            {
                hitboxChild = collision.GetComponent<HitboxChar>();
                LeoraChar2 leoraChar = otherCharTrigger.GetComponent<LeoraChar2>();

                if (otherCharTrigger.allied != this.allied)
                {
                    if (hitboxChild == null)
                    {
                        hitboxChild = collision.GetComponentInParent<HitboxChar>();
                        //Debug.Log("Child not found");
                    }

                    hitboxChild.alreadyHit = true;
                    collision.gameObject.SetActive(false);

                    int incomingDamage = otherCharTrigger.statsSheet["Strength"] - statsSheet["Defense"];

                    if (hitboxChild.isParryable)
                    {
                        if (isPerfectParrying)
                        {
                            //Debug.Log("Perfect Parry");
                            GotDamaged(incomingDamage / 10, otherCharTrigger.gameObject, 0);
                            leoraChar.TriggerHurtAnim();
                            //Debug.Log(otherCharTrigger.gameObject.name);
                            otherCharTrigger.stunTimer.cooldownTime = 1.5f;
                            otherCharTrigger.stunTimer.StartCooldown();
                            otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            leoraChar.stunned = true;
                            if (otherCharTrigger.tag != "Boss")
                            {
                                otherCharTrigger.StartCoroutine(otherCharTrigger.Knockback(this.gameObject, 0));
                            }
                        }
                        else if (isParrying)
                        {
                            //Debug.Log("Parry");
                            GotDamaged(incomingDamage / 2, otherCharTrigger.gameObject, 0.5f);
                            leoraChar.TriggerHurtAnim();
                            otherCharTrigger.stunTimer.cooldownTime = 1f;
                            otherCharTrigger.stunTimer.StartCooldown();
                            otherCharTrigger.SpawnParticle("stunFX", otherCharTrigger.transform.position, otherCharTrigger.transform, otherCharTrigger.stunTimer.cooldownTime);
                            leoraChar.stunned = true;
                            if (otherCharTrigger.tag != "Boss")
                            {
                                //Debug.Log("Parry");
                                otherCharTrigger.StartCoroutine(otherCharTrigger.Knockback(this.gameObject, 0));
                            }
                        }
                        else
                        {
                            GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                            TriggerHurtAnim();
                        }
                        
                    }
                    else
                    {
                        GotDamaged(incomingDamage, otherCharTrigger.gameObject, 1);
                        TriggerHurtAnim();
                    }
                }
            }
        }
    }

    public void MidFightDialogue()
    {
        mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();

        switch (outlierNum)
        {
            //If the outlier boss was killed
            case 1:
                switch (outlierBossDialogue)
                {
                    case "Ivar":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_ivarkilled");
                        break;
                    case "Lucan":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_lucakilled");
                        break;
                    case "Viin":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_viinkilled");
                        break;
                }
                break;
            //If the outlier boss was spared
            case 2:
                switch (outlierBossDialogue)
                {
                    case "Ivar":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_ivarspared");
                        break;
                    case "Lucan":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_lucaspared");
                        break;
                    case "Viin":
                        mdm.dialogueSTART("Endings/Conflicted/midfight_viinspared");
                        break;
                }
                break;
        }

    }



    public override IEnumerator Knockback(GameObject otherAttacker, float stMod)
    {
        if (hyperArmor)
        {
            stMod = 0;
        }

        return base.Knockback(otherAttacker, stMod);
    }


    //A event to be called in the animator that starts both cooldownsa and preps the animator for whether the user continues the combo or not.
    public void StartComboTimer()
    {
        animator.SetBool("isInCombo", true);
        animator.SetBool("Attacking", false);
        comboTimer.StartCooldown();
        attackCooldown.StartCooldown();
    }

    public override void Death()
    {
        audioManager.Instance.playSFX(17);

        DisableHurtbox();
        animator.SetBool("Death", true);
        darkLeoraLucanDashAI tempDLucanAI = dLeoraScript.activeLucanCopy.GetComponent<darkLeoraLucanDashAI>();
        tempDLucanAI.leftDashArrow.SetActive(false);
        tempDLucanAI.rightDashArrow.SetActive(false);
        Destroy(dLeoraScript.activeLucanCopy);
        Destroy(dLeoraScript.activeViinCopy);
        if (dLeoraScript.activeMagicParticle != null)
        {
            Destroy(dLeoraScript.activeMagicParticle);
        }

        if (dLeoraScript.firstProjectile != null)
        {
            Destroy(dLeoraScript.firstProjectile);
        }

        if (dLeoraScript.secondProjectile != null)
        {
            Destroy(dLeoraScript.secondProjectile);
        }
        charRB.constraints = RigidbodyConstraints2D.FreezeAll;
        charRB.velocity = Vector2.zero;
        ParryIndicator.SetActive(false);
        isParrying = false;
        isPerfectParrying = false;
    }

    public void DeathDialogue()
    {
        mainDialogueManager mdm = GameObject.FindObjectOfType<mainDialogueManager>();

        switch (outlierNum)
        {
            //If the outlier boss was killed
            case 1:
                switch (outlierBossDialogue)
                {
                    case "Ivar":
                        mdm.dialogueSTART("Endings/Conflicted/end_ivarkilled");
                        break;
                    case "Lucan":
                        mdm.dialogueSTART("Endings/Conflicted/end_lucakilled");
                        break;
                    case "Viin":
                        mdm.dialogueSTART("Endings/Conflicted/end_viinkilled");
                        break;
                }
                break;
            //If the outlier boss was spared
            case 2:
                switch (outlierBossDialogue)
                {
                    case "Ivar":
                        mdm.dialogueSTART("Endings/Conflicted/end_ivarspared");
                        break;
                    case "Lucan":
                        mdm.dialogueSTART("Endings/Conflicted/end_lucaspared");
                        break;
                    case "Viin":
                        mdm.dialogueSTART("Endings/Conflicted/end_viinspared");
                        break;
                }
                break;
        }

        Destroy(this.gameObject);
    }

    //used for invincibility
    public void DisableHurtbox()
    {
        hurtbox.enabled = false;
    }

    public void EnableHurtbox()
    {
        hurtbox.enabled = true;
    }

    //An event to be called in the animator that goes at the end of the combo
    public void endCombo()
    {
        attackCooldown.StartCooldown();
        animator.SetBool("Attacking", false);
    }
    public void DoNextCombo()
    {
        animator.SetBool("Attacking", true);
    }

    public void MagAttack()
    {
        animator.SetBool("Magicing", true);

        MagicEffects magEffects = magHitbox.GetComponent<MagicEffects>();

        magEffects.magicType = magicType;

        SetMana(GetMana() - 2);
    }

    public void EnableMagHitbox()
    {
        magHitbox.SetActive(true);
        if (darknessManager != null && magicType == "lightMag")
        {
            darknessManager.turnoffDarkness();
        }
    }

    public void DisableMagHitbox()
    {
        magHitbox.SetActive(false);
        //Debug.Log("Disable mag hitbox");
    }

    public void EndMagick()
    {
        animator.SetBool("Magicing", false);

        ToggleHyperArmor();

        if (magicType == "darkMag")
        {
            dLeoraScript.activeMagicParticle = Instantiate(darkMagFollowup, this.transform.position, Quaternion.identity);

            EvilDarkMagAoE tempMagManager = dLeoraScript.activeMagicParticle.GetComponent<EvilDarkMagAoE>();

            //Animator for particles would go here
            tempMagManager.animator.SetBool("darkFollowup", true);

            if (secondPhaseActive)
            {
                Vector2 firstProjSpawn = new Vector2(projectileSpawnPoint.transform.position.x + projectileSpawnOffset, projectileSpawnPoint.transform.position.y);
                Vector2 secondProjSpawn = new Vector2(projectileSpawnPoint.transform.position.x - projectileSpawnOffset, projectileSpawnPoint.transform.position.y);

                dLeoraScript.firstProjectile = Instantiate(projectilePrefab, firstProjSpawn, Quaternion.identity);
                IvarProjectile projScript = dLeoraScript.firstProjectile.GetComponent<IvarProjectile>();
                projScript.parentChar = this;

                dLeoraScript.secondProjectile = Instantiate(projectilePrefab, secondProjSpawn, Quaternion.identity);
                IvarProjectile secondProjScript = dLeoraScript.secondProjectile.GetComponent<IvarProjectile>();
                secondProjScript.parentChar = this;
            }
        }
    }
}
