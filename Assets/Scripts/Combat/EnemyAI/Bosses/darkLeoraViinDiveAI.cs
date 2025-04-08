using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkLeoraViinDiveAI : BaseChar
{
    [SerializeField] private Cooldown diveCooldown;
    private bool diving;
    private GameObject Player;

    public Transform WarningTransform;
    public Transform AttackAoETransform;
    [SerializeField] private float AoESize;

    [SerializeField] private float attackVariation;
    [SerializeField] private GameObject WarningObject;

    // Start is called before the first frame update
    void Start()
    {
        charName = "darkVeora";
        allied = false;

        Player = GameObject.Find("CombatPlayer");

        animator.SetBool("viinDive", true);

        ChangeStats(14, 0, 1, 1, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!diving && !diveCooldown.isCoolingDown)
        {
            diving = true;

            StartCoroutine(Diving());
        }
    }

    IEnumerator Diving()
    {
        this.transform.position = new Vector2(Player.transform.position.x + Random.Range(-attackVariation, attackVariation), Player.transform.position.y + Random.Range(-attackVariation, attackVariation));

        WarningTransform.localScale = new Vector2(Vector2.one.x * AoESize, Vector2.one.y * AoESize);
        AttackAoETransform.localScale = new Vector2(Vector2.one.x * AoESize, Vector2.one.y * AoESize);

        WarningObject.SetActive(true);

        yield return new WaitForSeconds(0.85f);

        WarningObject.SetActive(false);

        animator.SetBool("Attacking", true);

        diving = false;
        diveCooldown.StartCooldown();

    }
}
