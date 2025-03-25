using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViinFightTrigger : MonoBehaviour
{
    public ViinScript viinScript;

    public GameObject bossFog;

    // Start is called before the first frame update
    void Start()
    {
        
        bossFog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            viinScript.isActive = true;
            bossFog.SetActive(true);
        }
    }
}
