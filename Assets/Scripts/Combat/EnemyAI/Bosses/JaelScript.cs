using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JaelScript : MonoBehaviour
{
    [SerializeField] private Vector2 bottomLeftBounds;
    [SerializeField] private Vector2 topRightBounds;

    [SerializeField] public JaelChar jaelChar;

    [SerializeField] private int maxAxeThrowCount;
    private int originalMaxAxeCount;

    private int axesThrown;
    [SerializeField] private Cooldown throwingCooldown;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private GameObject axeSpawnPoint;

    [SerializeField] private List<GameObject> fakeJaelList = new List<GameObject>();

    [SerializeField] private float offsetFromPlayer;
    [SerializeField] private GameObject Player;

    [SerializeField] private Animator fogAnimator;

    private bool teleporting;

    [SerializeField] private bool specialAttacking;

    private bool firstPhase;
    private bool secondPhase;
    private bool thirdPhase;


    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<LeoraChar2>().gameObject;
        throwingCooldown.StartCooldown();
        specialAttacking = false;
        teleporting = false;

        firstPhase = false;
        secondPhase = false;

        originalMaxAxeCount = maxAxeThrowCount;
    }


    // Update is called once per frame
    void Update()
    {
        //If Jael is available to throw an axe
        if (!jaelChar.animator.GetBool("stunned") && !throwingCooldown.isCoolingDown && !jaelChar.animator.GetBool("Attacking") && teleporting == false)
        {
            if (!firstPhase &&  jaelChar.GetHealth() <= jaelChar.GetMaxHealth() - jaelChar.GetMaxHealth() / 4)
            {
                firstPhase = true;
                maxAxeThrowCount = originalMaxAxeCount + 2;
            }

            if (!secondPhase && jaelChar.GetMaxHealth() <= jaelChar.GetMaxHealth() / 2)
            {
                secondPhase = true;
                maxAxeThrowCount = originalMaxAxeCount + 4;
            }

            if (!specialAttacking && jaelChar.GetMaxHealth() <= jaelChar.GetMaxHealth() / 4)
            {
                specialAttacking = true;
                throwingCooldown.cooldownTime = 0.5f;
            }

            if (axesThrown <= maxAxeThrowCount)
            {
                teleporting = true;
                fogAnimator.SetBool("tpOut", true);
                //Debug.Log("Teleport out");

                if (specialAttacking == true)
                {
                    foreach (var fakeJael in fakeJaelList)
                    {
                        fakeJaelScript tempFakeJael = fakeJael.GetComponent<fakeJaelScript>();

                        tempFakeJael.fogAnimator.SetBool("tpOut", true);
                    }
                }
                /*jaelChar.animator.SetFloat("xDir", 0);
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

                if (tpPosition.x < bottomLeftBounds.x)
                {
                    tpPosition.x = bottomLeftBounds.x;
                }
                else if (tpPosition.x > topRightBounds.x)
                {
                    tpPosition.x = topRightBounds.x;
                }

                if (tpPosition.y < bottomLeftBounds.y)
                {
                    tpPosition.y = bottomLeftBounds.y;
                }
                else if (tpPosition.y > topRightBounds.y)
                {
                    tpPosition.y = topRightBounds.y;
                }

                this.gameObject.transform.position = tpPosition;
                jaelChar.TriggerAttackAnim();

                takenPositions.Add(jaelPosition);

                //tpList.RemoveAt(jaelPosition);

                if (specialAttacking)
                {
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
                }
                throwingCooldown.StartCooldown();
                axesThrown++;
                */
            }
            //Axe limit reached
            else
            {
                /*int jaelPosition = Random.Range(0, 4);

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

                if (tpPosition.x < bottomLeftBounds.x)
                {
                    tpPosition.x = bottomLeftBounds.x;
                }
                else if (tpPosition.x > topRightBounds.x)
                {
                    tpPosition.x = topRightBounds.x;
                }

                if (tpPosition.y < bottomLeftBounds.y)
                {
                    tpPosition.y = bottomLeftBounds.y;
                }
                else if (tpPosition.y > topRightBounds.y)
                {
                    tpPosition.y = topRightBounds.y;
                }

                this.gameObject.transform.position = tpPosition;*/

                if (!specialAttacking)
                {
                    jaelChar.animator.SetBool("stunned", true);
                    jaelChar.stunTimer.cooldownTime = 3;
                    jaelChar.stunTimer.StartCooldown();
                    //jaelChar.SpawnParticle("stunFX", jaelChar.transform.position, jaelChar.transform, jaelChar.stunTimer.cooldownTime);
                    axesThrown = 0;
                }
                else
                {
                    jaelChar.animator.SetBool("stunned", true);
                    jaelChar.stunTimer.cooldownTime = 6;
                    jaelChar.stunTimer.StartCooldown();
                    jaelChar.SpawnParticle("stunFX", jaelChar.transform.position, jaelChar.transform, jaelChar.stunTimer.cooldownTime);
                    axesThrown = 0;
                }
            }
        }
    }

    public void ThrowAxe()
    {
        GameObject Axe = Instantiate(axePrefab, axeSpawnPoint.transform.position, Quaternion.identity);
        JaelAxe axeScript = Axe.GetComponent<JaelAxe>();
        axeScript.parentChar = this.jaelChar;
    }

    public void TPandAttack()
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

        if (tpPosition.x < bottomLeftBounds.x)
        {
            tpPosition.x = bottomLeftBounds.x;
        }
        else if (tpPosition.x > topRightBounds.x)
        {
            tpPosition.x = topRightBounds.x;
        }

        if (tpPosition.y < bottomLeftBounds.y)
        {
            tpPosition.y = bottomLeftBounds.y;
        }
        else if (tpPosition.y > topRightBounds.y)
        {
            tpPosition.y = topRightBounds.y;
        }

        fogAnimator.SetBool("tpIn", true);

        this.gameObject.transform.position = tpPosition;
        //jaelChar.TriggerAttackAnim();

        takenPositions.Add(jaelPosition);

        //tpList.RemoveAt(jaelPosition);

        /*if (!specialAttacking && firstPhase)
        {
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
                        //tempFakeJael.TriggerAttackAnim();
                        tempFakeJael.fogAnimator.SetBool("tpIn", true);
                        takenPositions.Add(i);
                        break;
                    }

                }
            }
        }*/

        if (specialAttacking)
        {
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
                        //tempFakeJael.TriggerAttackAnim();
                        tempFakeJael.fogAnimator.SetBool("tpIn", true);
                        takenPositions.Add(i);
                        break;
                    }

                }
            }
        }
        throwingCooldown.StartCooldown();

        teleporting = false;
        axesThrown++;
    }
}
