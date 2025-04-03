using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseCanvas;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name);
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (isPaused == false)
            {
                PauseCanvas.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
            else if (isPaused == true)
            {
                PauseCanvas.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
            
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
