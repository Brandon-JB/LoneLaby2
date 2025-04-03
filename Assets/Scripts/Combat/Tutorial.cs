using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    //i have the text to display, maybe we could freeze the game, display it, wait 3 seconds,
    //then have the "try it now!!" text pop up, unfreeze the player, and only advance after they do that input or something

    [SerializeField] private string[] textToDisplay;
    [SerializeField] private TextMeshProUGUI[] tutorialtext;
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform[] tutorialUI;
    private int tutorialCounter;
    [SerializeField] private mainDialogueManager mainDialogueManager;

    public static Tutorial Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Freeze the game
        Time.timeScale = 0f;
    }

    public void progressTutorial()
    {
        
        
        tutorialtext[tutorialCounter].text = textToDisplay[tutorialCounter];

        tutorialUI[tutorialCounter].DOMove(locations[tutorialCounter % 2].position, 0.5f).SetUpdate(true).OnComplete(() => {

            tutorialCounter++;
            if (tutorialCounter % 2 == 1)
            {
                StartCoroutine(progressTutorialAfterDelay());

            } else
            {
                Time.timeScale = 1f;
                //Wait for player input
                switch (tutorialCounter)
                {
                    case 2:
                        //Player must move and attack
                        break;
                    case 4:
                        //Player must use magic
                        break;
                    case 6:
                        //Player must successfully parry

                        //This ends the tutorial
                        Time.timeScale = 0f;
                        mainDialogueManager.dialogueSTART("endTutorial");
                        break;
                }
            }
        });
    }
    private IEnumerator progressTutorialAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3f); // Wait for 3 seconds
        progressTutorial();
        StopCoroutine(progressTutorialAfterDelay());
    }



    //The text to display:
    /*
        When in dangerous situations, sometimes combat is necessary. Leora can move using W, A, S, and D, but she can also attack using (key).
        Try attacking the training dummy now using (key).
        Additionally, Leora can use magic to deal damage to multiple enemies at once. Each type of magic also has an additional effect, with Light magic stunning enemies.
        Try casting Light magic using (key). Leora can gain different types of magic at a later date.
        Finally, Leora can also parry attacks, stunning the attacker and diminishing the damage dealt to her. This can be done using (key).
        Try parrying Severin now using (key). Time if perfectly to get a perfect parry! 
     */
}
