using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucanFightTrigger : MonoBehaviour
{
    public LucanScript lucanScript;
    [SerializeField] private mainDialogueManager mdm;
    [SerializeField] private GameObject fogWall;

    //public GameObject bossFog;

    // Start is called before the first frame update
    void Start()
    {
        fogWall.SetActive(false);

        if (BossSaveData.bossStates["Lucan"] != 0)
        {
            Destroy(lucanScript.gameObject);
            Destroy(this.gameObject);
        }
        //bossFog.SetActive(false);
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
            mdm.dialogueSTART("LucanQuest/cave_prefight");
            lucanScript.isActive = true;
            //bossFog.SetActive(true);
            this.gameObject.SetActive(false);
            fogWall.SetActive(true);
        }
    }
}
