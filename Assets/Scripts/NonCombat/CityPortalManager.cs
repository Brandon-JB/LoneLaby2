using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityPortalManager : MonoBehaviour
{

    public Animator animator;
    //1 = city, 2 = Church
    public int areaNumber;

    public float transitionTime = 1f;

    public GameObject Player;
    public GameObject ChurchGO;
    public GameObject ChurchLEAVE;

    public GameObject StartTutorial;

    // Start is called before the first frame update
    void Start()
    {
        NonCombatPlayerMovement.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCityArea(string name)
    {

        NonCombatPlayerMovement.canMove = false;
        StartCoroutine(Load(areaNumber,name));
    }

    public void teleport(string areaName)
    {
        if (areaName == "Exit")
        {
            OpenPauseMenu.canOpenPause = false;
            OpenPauseMenu.GLOBALcanOpenPause = true;
            SceneManager.LoadScene("Overworld");
        }
        else if (areaName == "Training")
        {
            
            SceneManager.LoadScene("Tutorial");
            OpenPauseMenu.GLOBALcanOpenPause = true;
        }
        else if (areaName == "Church")
        {
            Player.transform.position = ChurchGO.transform.position;
            OpenPauseMenu.GLOBALcanOpenPause = true;
        }
        else if (areaName == "ChurchLeave")
        {
            Player.transform.position = ChurchLEAVE.transform.position;
            OpenPauseMenu.GLOBALcanOpenPause = true;
        }

    }

    IEnumerator Load(int areaNumber, string areaName)
    {
        OpenPauseMenu.GLOBALcanOpenPause = false;

        if (areaName == "ChurchLeave")
        {
            animator.SetTrigger("City");
            yield return new WaitForSecondsRealtime(transitionTime);

            teleport(areaName);
            yield return new WaitForSecondsRealtime(2f);

            NonCombatPlayerMovement.canMove = true;

        }
        else if (areaName == "Church")
        {
            animator.SetTrigger("Church");
            yield return new WaitForSecondsRealtime(transitionTime);

            teleport(areaName);
            yield return new WaitForSecondsRealtime(2f);
            NonCombatPlayerMovement.canMove = true;
        }
        else if (areaName == "Exit")
        {
            animator.SetTrigger("Leaving");
            yield return new WaitForSecondsRealtime(transitionTime);

            teleport(areaName);
            yield return new WaitForSecondsRealtime(2f);
            NonCombatPlayerMovement.canMove = true;
        }
        else if (areaName == "Training")
        {

            StartTutorial.SetActive(true);
            //do something (UI)
            //then once player confirms yes need to do animator.SetTrigger("Training");
        }


    }
}
