using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // 1 = City, 2 = Cave, 3 = Mansion, 4 = Forest, 5 = Church

    public Animator[] animator;
    public string[] SceneNames;
    public int areaNumber;

    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        areaNumber = PortalScript.whereGo - 1;
        Debug.Log(SceneNames[areaNumber] + " " + areaNumber);
        StartCoroutine(LoadLevel(SceneNames[areaNumber], areaNumber));
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

        yield return new WaitForSeconds(transitionTime);

        OpenPauseMenu.GLOBALcanOpenPause = true;
        SceneManager.LoadScene(SceneName);
        PlayerMovement.CanWalk = true;
    }
}
