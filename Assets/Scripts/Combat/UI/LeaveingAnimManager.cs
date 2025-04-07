using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class LeaveingAnimManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveAnimation(Animator animator)
    {
        StartCoroutine(Leave(animator));
    }

    IEnumerator Leave(Animator animator)
    {
        OpenPauseMenu.GLOBALcanOpenPause = false;

        animator.SetTrigger("Leaving");
        
        yield return new WaitForSeconds(1f);

        OpenPauseMenu.GLOBALcanOpenPause = true;
        SceneManager.LoadScene("Overworld");
    }
}
