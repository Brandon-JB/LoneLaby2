using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViinFightTrigger : MonoBehaviour
{
    public ViinScript viinScript;
    [SerializeField] private mainDialogueManager mdm;
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
            viinScript.attackCooldown.StartCooldown();
            mdm = GameObject.FindObjectOfType<mainDialogueManager>();
            mdm.dialogueSTART("ViinQuest/veinwood_prefight");
            viinScript.isActive = true;
            bossFog.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
