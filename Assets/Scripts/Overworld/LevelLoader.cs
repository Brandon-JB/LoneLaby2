using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class LevelLoader : MonoBehaviour
{
    // 1 = City, 2 = Cave, 3 = Mansion, 4 = Forest, 5 = Church

    public Animator[] animator;
    public string[] SceneNames;
    public int areaNumber;

    public mainDialogueManager mainDialogueManager;

    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if(DemoCheck.getDemo() == false)
        {
            areaNumber = PortalScript.whereGo - 1;
            Debug.Log(SceneNames[areaNumber] + " " + areaNumber);
            StartCoroutine(LoadLevel(SceneNames[areaNumber], areaNumber));
        }
        else
        {
            areaNumber = PortalScript.whereGo - 1;
            Debug.Log(SceneNames[areaNumber] + " " + areaNumber);
            StartCoroutine(LoadDemoLevel(SceneNames[areaNumber], areaNumber));
        }
    }

    IEnumerator LoadLevel(string SceneName, int areaNumber)
    {
        OpenPauseMenu.GLOBALcanOpenPause = false;

        if (areaNumber == 0)
        {
            animator[0].SetBool("IsTown", true);
        }
        else if (areaNumber == 1)
        {
            animator[0].SetBool("IsCave", true);
        }
        else if (areaNumber == 2)
        {
            animator[0].SetBool("IsMansion", true);
        }
        else if (areaNumber == 3)
        {
            animator[0].SetBool("IsForest", true);
        }
        //else if (areaNumber == 4)
        //{
        //    animator[0].SetBool("IsFight", true);
        //}


        animator[0].SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        //OpenPauseMenu.GLOBALcanOpenPause = true;
        PlayerMovement.CanWalk = true;
        SceneManager.LoadScene(SceneName);
        OpenPauseMenu.GLOBALcanOpenPause = false;
        //Debug.Log("called loadScene");
        
    }

    IEnumerator LoadDemoLevel(string SceneName, int areaNumber)
    {
        OpenPauseMenu.GLOBALcanOpenPause = false;

        bool Continue = false;

        if (areaNumber == 0)
        {
            animator[0].SetBool("IsTown", true);
            Continue = true;
        }
        else if (areaNumber == 1)
        {
            animator[0].SetBool("IsCave", true);
            Continue = true;
        }
        else if (areaNumber == 2)
        {
            mainDialogueManager.dialogueSTART("demo_unavailableArea");
            Continue = false;
        }
        else if (areaNumber == 3)
        {
            mainDialogueManager.dialogueSTART("demo_unavailableArea");
            Continue = false;
        }
        //else if (areaNumber == 4)
        //{
        //    animator[0].SetBool("IsFight", true);
        //}

        if(Continue == true)
        {
            animator[0].SetTrigger("Start");

            yield return new WaitForSecondsRealtime(transitionTime);

            //OpenPauseMenu.GLOBALcanOpenPause = true;
            PlayerMovement.CanWalk = true;
            SceneManager.LoadScene(SceneName);
            OpenPauseMenu.GLOBALcanOpenPause = false;
            //Debug.Log("called loadScene");
        }

    }
}

