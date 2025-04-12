using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvarFightTrigger : MonoBehaviour
{
    public IvarScript ivarScript;
    public mainDialogueManager mdm;

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
            mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("IvarQuest/manor_prefight");
            ivarScript.isActive = true;
            bossFog.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
