using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossInteraction : MonoBehaviour
{
    [SerializeField] protected float interactRange = 3f;
    [SerializeField] protected float DistanceBetweenObjectAndPlayer;
    [SerializeField] protected GameObject Player;

    public string sceneName;


    // Update is called once per frame
    public virtual void Update()
    {
        DistanceBetweenObjectAndPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (InputManager.interactPressed)
            {

                BossSaveData.bossStates["Ivar"] = Random.Range(1, 3);
                BossSaveData.bossStates["Viin"] = Random.Range(1, 3);
                BossSaveData.bossStates["Lucan"] = Random.Range(1, 3);

                //If they are all the same
                if (BossSaveData.bossStates["Ivar"] == BossSaveData.bossStates["Viin"] && BossSaveData.bossStates["Viin"] == BossSaveData.bossStates["Lucan"])
                {
                    int randoBoss = Random.Range(1, 4);

                    //Switching the value so it's different
                    switch (randoBoss)
                    {
                        case 1:
                            if (BossSaveData.bossStates["Ivar"] == 1)
                            {
                                BossSaveData.bossStates["Ivar"] = 2;
                            }
                            else
                            {
                                BossSaveData.bossStates["Ivar"] = 1;
                            }
                            break;
                        case 2:
                            if (BossSaveData.bossStates["Viin"] == 1)
                            {
                                BossSaveData.bossStates["Viin"] = 2;
                            }
                            else
                            {
                                BossSaveData.bossStates["Viin"] = 1;
                            }
                            break;
                        case 3:
                            if (BossSaveData.bossStates["Lucan"] == 1)
                            {
                                BossSaveData.bossStates["Lucan"] = 2;
                            }
                            else
                            {
                                BossSaveData.bossStates["Lucan"] = 1;
                            }
                            break;
                    }
                }

                foreach (var boss in BossSaveData.bossStates)
                {
                    Debug.Log(boss);
                }

                //Put interactions here
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void Awake()
    {
        Player = FindObjectOfType<NonCombatPlayerMovement>().gameObject;
    }
}
