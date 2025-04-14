using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject Player;
    public saveUI saveui;
    private float maxDistance = 1.5f;
    private float DistanceBetweenObjects;
    //private float InteractionLength = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DistanceBetweenObjects = Vector3.Distance(transform.position, Player.transform.position);
        if (DistanceBetweenObjects <= maxDistance)
        {
            if (InputManager.interactPressed == true)
            {
                Debug.Log("");
                saveui.OPENSAVEMENU();
            }
        }
    }
}
