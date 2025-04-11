using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JaelScript : MonoBehaviour
{
    [SerializeField] private Vector2 bottomLeftBounds;
    [SerializeField] private Vector2 topRightBounds;

    [SerializeField] private JaelChar jaelChar;

    [SerializeField] private int maxAxeThrowCount;
    private int axesThrown;
    [SerializeField] private Cooldown throwingCooldown;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private GameObject axeSpawnPoint;

    [SerializeField] private List<GameObject> fakeJaelList = new List<GameObject>();

    [SerializeField] private float offsetFromPlayer;
    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<LeoraChar2>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //If Jael is available to throw an axe
        if (!jaelChar.animator.GetBool("stunned") && !throwingCooldown.isCoolingDown && !jaelChar.animator.GetBool("Attacking"))
        {
            if (axesThrown <= maxAxeThrowCount)
            {
                jaelChar.animator.SetFloat("xDir", 0);
                jaelChar.animator.SetFloat("yDir", 0);

                //Real jael position
                int jaelPosition = Random.Range(0, 4);

                Vector2 tpPosition = Player.transform.position;

                List<Vector2> tpList = new List<Vector2>();// = {
                tpList.Add(new Vector2(tpPosition.x + offsetFromPlayer, tpPosition.y));
                tpList.Add(new Vector2(tpPosition.x, tpPosition.y + offsetFromPlayer));
                tpList.Add(new Vector2(tpPosition.x - offsetFromPlayer, tpPosition.y));
                tpList.Add(new Vector2(tpPosition.x, tpPosition.y - offsetFromPlayer));

                List<int> takenPositions = new List<int>();


                switch (jaelPosition)
                {
                    case 0:
                        tpPosition = tpList[jaelPosition];//new Vector2(tpList[jaelPosition].x + offsetFromPlayer, tpList[jaelPosition].y);
                        jaelChar.animator.SetFloat("xDir", -1);
                        break;
                    case 1:
                        tpPosition = tpList[jaelPosition];//new Vector2(tpList[jaelPosition].x, tpList[jaelPosition].y + offsetFromPlayer);
                        jaelChar.animator.SetFloat("yDir", -1);
                        break;
                    case 2:
                        tpPosition = tpList[jaelPosition]; //new Vector2(tpList[jaelPosition].x - offsetFromPlayer, tpList[jaelPosition].y);
                        jaelChar.animator.SetFloat("xDir", 1);
                        break;
                    case 3:
                        tpPosition = tpList[jaelPosition]; //new Vector2(tpList[jaelPosition].x, tpList[jaelPosition].y - offsetFromPlayer);
                        jaelChar.animator.SetFloat("yDir", 1);
                        break;
                }

                this.gameObject.transform.position = tpPosition;
                jaelChar.TriggerAttackAnim();

                takenPositions.Add(jaelPosition);

                //tpList.RemoveAt(jaelPosition);

                foreach (var fakeJ in fakeJaelList)
                { 
                    for (int i = 0; i < tpList.Count; i++)
                    {
                        if (!takenPositions.Contains(i))
                        {
                            fakeJaelScript tempFakeJael = fakeJ.GetComponent<fakeJaelScript>();
                            //Debug.Log("Does not contain the tp in index: " + i);

                            tempFakeJael.animator.SetFloat("xDir", 0);
                            tempFakeJael.animator.SetFloat("yDir", 0);

                            switch (i)
                            {
                                case 0:
                                    tempFakeJael.animator.SetFloat("xDir", -1);
                                    //tpPosition = new Vector2(tpPosition.x + offsetFromPlayer, tpPosition.y);
                                    break;
                                case 1:
                                    tempFakeJael.animator.SetFloat("yDir", -1);
                                    //tpPosition = new Vector2(tpPosition.x, tpPosition.y + offsetFromPlayer);
                                    break;
                                case 2:
                                    tempFakeJael.animator.SetFloat("xDir", 1);
                                    //tpPosition = new Vector2(tpPosition.x - offsetFromPlayer, tpPosition.y);
                                    break;
                                case 3:
                                    tempFakeJael.animator.SetFloat("yDir", 1);
                                    //tpPosition = new Vector2(tpPosition.x, tpPosition.y - offsetFromPlayer);
                                    break;
                            }

                            fakeJ.transform.position = tpList[i];
                            tempFakeJael.TriggerAttackAnim();
                            takenPositions.Add(i);
                            break;
                        }
                        
                    }
                }

                /*for (int i = 0; i < tpList.Count; i++)
                {
                    fakeJaelScript tempFakeJael = fakeJaelList[i].GetComponent<fakeJaelScript>();

                    switch (i)
                    {
                        case 0:
                            tempFakeJael.animator.SetFloat("xDir", -1);
                            //tpPosition = new Vector2(tpPosition.x + offsetFromPlayer, tpPosition.y);
                            break;
                        case 1:
                            tempFakeJael.animator.SetFloat("yDir", -1);
                            //tpPosition = new Vector2(tpPosition.x, tpPosition.y + offsetFromPlayer);
                            break;
                        case 2:
                            tempFakeJael.animator.SetFloat("xDir", 1);
                            //tpPosition = new Vector2(tpPosition.x - offsetFromPlayer, tpPosition.y);
                            break;
                        case 3:
                            tempFakeJael.animator.SetFloat("yDir", 1);
                            //tpPosition = new Vector2(tpPosition.x, tpPosition.y - offsetFromPlayer);
                            break;
                    }

                    fakeJaelList[i].transform.position = tpList[i];
                    tempFakeJael.TriggerAttackAnim();
                }*/
                throwingCooldown.StartCooldown();
                axesThrown++;
            }
            //Axe limit reached
            else
            {

            }
        }
    }

    public void ThrowAxe()
    {
        GameObject Axe = Instantiate(axePrefab, axeSpawnPoint.transform.position, Quaternion.identity);
        JaelAxe axeScript = Axe.GetComponent<JaelAxe>();
        axeScript.parentChar = this.jaelChar;
    }
}
