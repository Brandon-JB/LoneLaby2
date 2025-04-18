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
            Player.transform.position = Destination[0].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "Floor2-1")
        {
            Player.transform.position = Destination[1].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "Floor2-3")
        {
            Player.transform.position = Destination[6].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "Floor3-2")
        {
            Player.transform.position = Destination[7].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "Floor2-S")
        {
            Player.transform.position = Destination[2].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "FloorS-2")
        {
            Player.transform.position = Destination[3].transform.position;
            StartCoroutine(Wait());
        }

        if (name == "BossLeave")
        {
            Player.transform.position = Destination[4].transform.position;
            StartCoroutine(Wait());
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
            Player.transform.position = Destination[5].transform.position;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        combatMovement.canMove = false;
        yield return new WaitForSeconds(0.3f);
        combatMovement.canMove = true;
    }
}
