using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject SpawnMain;
    public GameObject SpawnTraining;
    public GameObject SpawnChurch;
    public static int SpawnNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        OpenPauseMenu.canOpenPause = true;
        if (SpawnNumber == 2) // should be 2 for full game
        {
            Player.transform.position = SpawnMain.transform.position;
        }
        else if (SpawnNumber == 1) //Should be 1 for full game
        {
            Player.transform.position = SpawnTraining.transform.position;
        }
        else if (SpawnNumber == 0) //should be 0 for full game
        {
            Player.transform.position = SpawnChurch.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
