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
        StartCoroutine(LoadLevel(SceneNames[areaNumber], areaNumber));
    }

    IEnumerator LoadLevel(string SceneName, int areaNumber)
    {
        if (areaNumber == 0)
        {
            animator[areaNumber].SetBool("IsTown", true);
        }
        else if (areaNumber == 1)
        {
            animator[areaNumber].SetBool("IsCave", true);
        }
        else if (areaNumber == 2)
        {
            animator[areaNumber].SetBool("IsForest", true);
        }
        else if (areaNumber == 3)
        {
            animator[areaNumber].SetBool("IsMansion", true);
        }


        animator[areaNumber].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneName);
    }
}
