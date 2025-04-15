using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSpawnManager : MonoBehaviour
{

    public static int CutsceneSpawnpoint; // 0 upstairs, 1 downstairs.
    public GameObject upstairs;
    public GameObject downstairs;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        if (CutsceneSpawnpoint == 0)
        {
            Player.transform.position = upstairs.transform.position;
        }
        if (CutsceneSpawnpoint == 1)
        {
            Player.transform.position = downstairs.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
