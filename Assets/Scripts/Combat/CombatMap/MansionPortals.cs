using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionPortals : MonoBehaviour
{
    public GameObject Floor1;
    public GameObject Floor2;
    public GameObject Floor3Enter;
    public GameObject Floor3Exit;
    public GameObject Player;

    public GameObject[] Destination;

    public CombatPlayerMovement combatMovement;
    public LeaveingAnimManager leaveAnim;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {

    }

    public void MansionStairs(string name)
    {
        if (name == "Floor1-2")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(1));
        }
        else if (name == "Floor2-1")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(0));
        }
        else if (name == "Floor2-3")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(3));
        }
        else if (name == "Floor3")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(2));
        }
    }


    IEnumerator Wait(int DestNum)
    {
        combatMovement.canMove = false;
        yield return new WaitForSeconds(0.2f);
        Player.transform.position = Destination[DestNum].transform.position;
        yield return new WaitForSeconds(0.1f);
        combatMovement.canMove = true;
    }
}

