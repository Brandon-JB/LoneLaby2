using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatPortals : MonoBehaviour
{

    public GameObject[] Portal;
    public GameObject[] Destination;
    public GameObject Player;
    public Animator animator;

    public LeaveingAnimManager leaveAnim;

    public CombatPlayerMovement combatMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportPlayer(string name)
    {
        Debug.Log(name);
        if (name == "Floor1-2")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(0));
        }

        if (name == "Floor2-1")
        {
            leaveAnim.PlayPortalAnimation(animator);
            
            StartCoroutine(Wait(1));
        }

        if (name == "Floor2-3")
        {
            leaveAnim.PlayPortalAnimation(animator);
            
            StartCoroutine(Wait(6));
        }

        if (name == "Floor3-2")
        {
            leaveAnim.PlayPortalAnimation(animator);
            
            StartCoroutine(Wait(7));
        }

        if (name == "Floor2-S")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(2));
        }

        if (name == "FloorS-2")
        {
            leaveAnim.PlayPortalAnimation(animator);
            StartCoroutine(Wait(3));
        }

        if (name == "BossLeave")
        {
            leaveAnim.PlayPortalAnimation(animator);
            
            StartCoroutine(Wait(4));
        }

        if (name == "Exit")
        {
            leaveAnim.LeaveAnimation(animator);
            //SceneManager.LoadScene("Overworld");
        }
        
        if (name == "Town")
        {
            SpawnManager.SpawnNumber = 2;
            SceneManager.LoadScene("NoCombatAreas");
        }

        if (name == "Boss")
        {
            leaveAnim.PlayPortalAnimation(animator);
            
            StartCoroutine(Wait(5));
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
