using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject SpawnMain;
    public GameObject SpawnTraining;
    public static int SpawnNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SpawnNumber == 0)
        {
            Player.transform.position = SpawnMain.transform.position;
        }
        else if (SpawnNumber == 1) 
        {
            Player.transform.position = SpawnTraining.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
