using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class mainDialogueManager : MonoBehaviour
{
    //pass in values to determine where the story is/what text should be presented
    //values include:
    //int floor === defaults to 0. determines if this is a boss dialogue or not
    //      |
    //       --> keep at 0 if this is not the boss at the end of a floor
    //array? party === who is currently in the party. Defaults to an empty array
    //      |
    //       --> ALWAYS pass in current party members
    //bool isBoss === used to determine if this is a boss dialogue. defaults to false
    //string special === used for special cutscenes, namely the beginning lore drop

    [SerializeField] private Transform bottom;
    [SerializeField] private Transform top;
    [SerializeField] private CanvasGroup fadeOut;
    [SerializeField] private CanvasGroup cutsceneBG;
    [SerializeField] private GameObject[] tweenInPositions;
    [SerializeField] private GameObject[] tweenOutPositions;
    [SerializeField] private Transform dialogueBox;
    [SerializeField] private GameObject[] cutsceneScenes;
    [SerializeField] private GameObject[] cutsceneVariations;
    [SerializeField] private GameObject continueTextPrompt;
    [SerializeField] private HUD_Equipment equipmentMenu;


    [SerializeField] private TextMeshProUGUI condemntxtbox, normaltxtbox, nametxt;
    [SerializeField] private Image nameBox;
    [SerializeField] private Sprite voidSprite;

    [SerializeField] private gainQuest gainQuestMenu;

    //[SerializeField] private pauseMenuManager pauseMenuManager;

    //[SerializeField] private PlayerController playerController;

    // when loading something from resources, you dont specify the file extension
    //[SerializeField] private TextAsset fileName;
    public bool dialogueRunning = false; // Track if dialogue coroutine is running
    //private string fileName = "prologue";

    private string currentlyRunningText = "";
    public static string GLOBALcurrentlyRunningText = "";

    //Used for testing.
    //private bool testingDialogueRan = false;

    private void Start()
    {
        ////dialogueSTART(fileName); // only here for testing
        //if (playerController.BattleTutorialCleared == 1)
        //{
        //    playerController.loadGame();
        //}
        //else // if they have not cleared the tutorial
        //{
        //    dialogueSTART(fileName);
        //}
        //Time.timeScale = 1f;
    }

    //Used for testing. Comment out for the actual game.
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C) && testingDialogueRan == false)
        //{
        //    testingDialogueRan = true;
        //    dialogueSTART("SaveConvo/floor2end_kn");
        //}
    }

    //MUST PASS IN THE 
    public void dialogueSTART(string dialogueFile)
    {
        // Only start the coroutine if it's not already running
        if (!dialogueRunning)
        {
            try
            {
                //Turn off previous dialogue
                nameBox.sprite = voidSprite;
                normaltxtbox.text = "";
                nametxt.text = "";
                
            }
            catch
            {
                Debug.Log("Dialogue bug");
            }


            Time.timeScale = 0f;
            OpenPauseMenu.GLOBALcanOpenPause = true;
            OpenPauseMenu.canOpenPause = false;
            OpenPauseMenu.pauseOpened = false;
            StartCoroutine(completeDialogue("TextFiles/" + dialogueFile));
            dialogueRunning = true; // Set the flag to true when starting the coroutine

            currentlyRunningText = dialogueFile;
            GLOBALcurrentlyRunningText = dialogueFile;
            //playerController.isfrozen = true;

            switch (currentlyRunningText)
            {
                case "IvarQuest/manor_postfight_condemnIvar":
                case "ViinQuest/veinwood_postfight_condemnViin":
                case "LucanQuest/cave_postfight_condemnLucan":
                    //move dialogue box to middle, resize
                    normaltxtbox.fontSize = 18;
                    normaltxtbox.transform.position = condemntxtbox.transform.position;
                    return;
                case "LucanQuest/cave_prefight":
                case "IvarQuest/mansion_prefight":
                case "ViinQuest/veinwood_prefight":
                    audioManager.Instance.playBGM("T8");
                    break;
                case "LucanQuest/cave_postfight_saveLucan":
                case "IvarQuest/mansion_postfight_saveIvar":
                case "ViinQuest/veinwood_postfight_saveViin":
                    FindObjectOfType<killSpareManager>().GetComponent<CanvasGroup>().DOFade(0f, 1).SetUpdate(true);
                    break;
                case "Endings/Condemn/meetingJael":
                case "Endings/Compassion/severin_prefight":
                    audioManager.Instance.playBGM("T9");
                    break;
                case "Endings/Conflicted/beginConflictedFight":
                    audioManager.Instance.playBGM("T11");
                    break;
            }



            top.DOMove(tweenInPositions[0].transform.position, 1.5f).SetUpdate(true).SetEase(Ease.OutCubic);
            bottom.DOMove(tweenInPositions[1].transform.position, 1.5f).SetUpdate(true).SetEase(Ease.OutCubic);
            dialogueBox.DOMove(tweenInPositions[2].transform.position, 1.5f).SetUpdate(true).SetEase(Ease.OutBack);

            equipmentMenu = FindObjectOfType<HUD_Equipment>();
            if (equipmentMenu)
            {
                equipmentMenu.GetComponent<CanvasGroup>().DOFade(0, 0.25f).SetUpdate(true);
            }


            //if (dialogueFile.StartsWith("end"))
            //{
            //    cutsceneBG.DOFade(1, 1f);
            //}
            //else if (dialogueFile == "partySplit")
            //{
            //    pauseMenuManager.partyMemberAbsent("KISA");
            //    pauseMenuManager.partyMemberAbsent("NICOL");
            //    pauseMenuManager.partyMemberAbsent("SOPHIE");
            //    firstSlime.SetActive(false);
            //}
            //else if (dialogueFile.StartsWith("Supports") || dialogueFile.StartsWith("SaveConvo"))
            //{
            //    return; // Don't change the audio for any support dialogue
            //}
            //If we need to do something special with the new dialogue, do it here. check the name of the dialogue file

            /*switch (dialogueFile)
            {
                case "kisaPostFight_k":
                case "nicolEncounter_k":
                case "nicolEncounter_x":
                case "nicolPostFight_kn":
                case "sophiePostFight_kns":
                    am.playBGM("T5");
                    break;
                case "kisaPostFight_x":
                case "nicolPostFight_kx":
                case "nicolPostFight_xx":
                case "sophiePostFight_knx":
                case "sophiePostFight_xnx":
                case "sophiePostFight_kxx":
                    am.playBGM("T6");
                    break;
                case "secondFloor_k":
                case "thirdFloor_kn":
                case "fourthFloor_kns":
                case "level1_slime":
                case "level1_skeleton":
                case "tutorialEncounter":
                case "contemplateKillingf1":
                case "contemplateKillingf2":
                case "contemplateKillingf3":
                case "contemplateKillingf3.5":
                case "contemplateKillingf4":
                    break;
                case "load_genocide":
                    am.changePitch(1, 0.8f, 0.01f);
                    am.playBGM("T2");
                    break;
                case "sophieEncounter_xx":
                case "sophiePostFight_xxx":
                    am.stopBGM(1);
                    break;
                default:
                    am.playBGM("T4");
                    break;
            }*/
        }
    }


    //private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);
    IEnumerator Run(string dialogueFile)
    {
        List<string> lines = FileManager.ReadTextAsset(dialogueFile, false);
        DialogueSystem.instance.Say(lines);
        yield return null;
    }
    IEnumerator completeDialogue(string dialogueFile)
    {
        yield return StartCoroutine(Run(dialogueFile));
    }

    public void dialogueEND(bool isBoss = false)
    {
        // Stop the dialogue coroutine if it's running
        if (dialogueRunning)
        {
            StopCoroutine(completeDialogue(currentlyRunningText));
            dialogueRunning = false; // Set the flag to false when stopping the coroutine
            //if (currentlyRunningText == "prologue")
            //{
            //    pauseMenuManager.partyMemberAdded("KISA");
            //    pauseMenuManager.partyMemberAdded("NICOL");
            //    pauseMenuManager.partyMemberAdded("SOPHIE");
            //    firstSign.enabled = true;
            //}
            //for determining endings
            //switch (currentlyRunningText)
            //{
            //    case "openingCutscene":
            //        Debug.Log("I am sending you to the dialogue scene");
            //        SceneManager.LoadScene("Dialogue");
            //        //fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Dialogue"); });
            //        break;
            //}

            switch (currentlyRunningText)
            {
                case "IvarQuest/manor_postfight":
                    FindObjectOfType<killSpareManager>().EnableKillSpare("Ivar");
                    break;
                case "ViinQuest/veinwood_postfight":
                    FindObjectOfType<killSpareManager>().EnableKillSpare("Viin");
                    break;
                case "LucanQuest/cave_postfight":
                    FindObjectOfType<killSpareManager>().EnableKillSpare("Lucan");
                    break;
                case "LucanQuest/cave_postfight_saveVerita":
                case "IvarQuest/manor_postfight_saveVerita":
                case "ViinQuest/veinwood_postfight_saveVerita":
                    if (BossSaveData.GetNumberOfBossesObtained() == 3)
                    {
                        //ok cool we are triggering the final boss. what dialogue are we playing
                        switch(BossSaveData.GetNumberOfCondemned())
                        {
                            case 0:
                                dialogueSTART("Endings/Compassion/beginCompassion");
                                return;
                            default:
                                dialogueSTART("Endings/Conflicted/withVerita/grabLeora");
                                return;
                        }
                    }
                    break;
                case "LucanQuest/cave_postfight_condemnSpeaker":
                case "IvarQuest/manor_postfight_condemnSpeaker":
                case "ViinQuest/veinwood_postfight_condemnSpeaker":
                    if (BossSaveData.GetNumberOfBossesObtained() == 3)
                    {
                        //ok cool we are triggering the final boss. what dialogue are we playing
                        switch (BossSaveData.GetNumberOfCondemned())
                        {
                            //If they are talking to the speaker, they have condemned *someone*.
                            case 1:
                                dialogueSTART("Endings/Conflicted/withSpeaker/onecondemned");
                                return;
                            case 2:
                                dialogueSTART("Endings/Conflicted/withSpeaker/twocondemned");
                                return;
                            case 3:
                                dialogueSTART("Endings/Condemn/beginCondemn");
                                return;
                        }
                    }
                    break;
                case "Endings/Compassion/beginCompassion":
                    dialogueSTART("Endings/Compassion/speakerCompassion");
                    break;
                case "Endings/Conflicted/withVerita/grabLeora":
                    //ok cool we are triggering the final boss. what dialogue are we playing
                    switch (BossSaveData.GetNumberOfCondemned())
                    {
                        //If they are talking to the speaker, they have condemned *someone*.
                        case 1:
                            dialogueSTART("Endings/Conflicted/withVerita/onecondemned");
                            return;
                        case 2:
                            dialogueSTART("Endings/Conflicted/withVerita/twocondemned");
                            return;
                    }
                    break;
                case "SideQuests/getAlanQuest":
                case "SideQuests/getKisaQuest":
                case "SideQuests/getSophQuest":
                    gainQuestMenu.ShowQuestStart(currentlyRunningText, false);
                    currentlyRunningText = "";
                    GLOBALcurrentlyRunningText = "";
                    continueTextPrompt.SetActive(false);
                    top.DOMove(tweenOutPositions[1].transform.position, 1).SetUpdate(true).SetEase(Ease.OutCubic);
                    bottom.DOMove(tweenOutPositions[1].transform.position, 1).SetUpdate(true).SetEase(Ease.OutCubic);
                    dialogueBox.DOMove(tweenOutPositions[2].transform.position, 2).SetUpdate(true).SetEase(Ease.OutBack);
                    return;
                case "SideQuests/finAlanQuest":
                case "SideQuests/finKisaQuest":
                case "SideQuests/finSophQuest":
                    gainQuestMenu.ShowQuestStart(currentlyRunningText, true);
                    currentlyRunningText = "";
                    GLOBALcurrentlyRunningText = "";
                    continueTextPrompt.SetActive(false);
                    top.DOMove(tweenOutPositions[1].transform.position, 1).SetUpdate(true).SetEase(Ease.OutCubic);
                    //Change bottom
                    bottom.DOMove(tweenOutPositions[1].transform.position, 1).SetUpdate(true).SetEase(Ease.OutCubic);
                    dialogueBox.DOMove(tweenOutPositions[2].transform.position, 2).SetUpdate(true).SetEase(Ease.OutBack);
                    return;
                case "beginTutorial":
                    break;
                default:
                    Time.timeScale = 1f;
                    break;
            }

            //switch (currentlyRunningText) // if this is the end of a route
            //{
            //    case "end_genocide":// 0 = everyone dead
            //        creditsManager.endingID = 0;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_n":// 1 = only nicol alive
            //        creditsManager.endingID = 1;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_ns":// 2 = nicol and sophie alive
            //        creditsManager.endingID = 2;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_k_postFight":// 3 = kisa alive
            //        creditsManager.endingID = 3;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_ks":// 4 = kisa and sophie alive
            //        creditsManager.endingID = 4;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_kn":// 5 = kisa and nicol alive
            //        creditsManager.endingID = 5;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_kns":// 6 = everyone alive!!
            //        creditsManager.endingID = 6;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //    case "end_k": // was going to be Kisa boss fight, removed that because we don't have time
            //        creditsManager.endingID = 3;
            //        fadeOut.DOFade(1, 3).OnComplete(() => { SceneManager.LoadScene("Credits"); });
            //        return;
            //}
            currentlyRunningText = "";
            GLOBALcurrentlyRunningText = "";
            continueTextPrompt.SetActive(false);
            top.DOMove(tweenOutPositions[0].transform.position, 2).SetUpdate(true).SetEase(Ease.OutCubic);
            bottom.DOMove(tweenOutPositions[1].transform.position, 2).SetUpdate(true).SetEase(Ease.OutCubic);
            //CHANGE THIS KATIE YOU ARE STUPID AND NEED TO DIE

            dialogueBox.DOMove(tweenOutPositions[2].transform.position, 2).SetUpdate(true).SetEase(Ease.OutBack);
            if (equipmentMenu)
            {
                equipmentMenu.GetComponent<CanvasGroup>().DOFade(1, 1f).SetUpdate(true);
            }
            OpenPauseMenu.canOpenPause = true;
            //.OnComplete(() => { SceneManager.LoadScene("Overworld"); });
            //if (isBoss)
            //{
            //    battleFade.SetBool("BattleStarting", true);
            //    for (int i = 0; i < cutsceneScenes.Length; i++)
            //    {
            //        cutsceneScenes[i].SetActive(false);
            //    }
            //    return;
            //}
            //am.playBGM("T2");
            //playerController.isfrozen = false;
        }
    }


    //public void menuDialogueEND(bool isSupport = false)
    //{
    //    // Stop the dialogue coroutine if it's running
    //    if (dialogueRunning)
    //    {
    //        StopCoroutine(completeDialogue(currentlyRunningText));
    //        dialogueRunning = false; // Set the flag to false when stopping the coroutine
    //        if (isSupport)
    //        {
    //            pauseMenuManager.gameObject.SetActive(true);
    //            // Step 1: Remove "Supports/" if it exists
    //            if (currentlyRunningText.StartsWith("Supports/DeadAllyConvo/"))
    //            {
    //                currentlyRunningText = currentlyRunningText.Substring(23); // Remove the first 22 characters
    //                //Do more fancy stuff, just mark all scenes as seen idk
    //            }
    //            else if (currentlyRunningText.StartsWith("Supports/"))
    //            {
    //                currentlyRunningText = currentlyRunningText.Substring(9); // Remove the first 9 characters

    //            }

    //            // Step 2: Extract the first 4 letters
    //            string firstPart = currentlyRunningText.Substring(0, 4); // "alan"

    //            // Step 3: Extract the next 4 letters
    //            string secondPart = currentlyRunningText.Substring(4, 4); // "kisa"

    //            // Step 4: Parse the final number and map it to 1, 2, or 3
    //            int finalNumber = int.Parse(currentlyRunningText.Substring(8)); // "3" in this case
    //            int mappedValue = finalNumber switch
    //            {
    //                3 => 1,
    //                8 => 2,
    //                14 => 3,
    //                _ => 4 // Default case for unexpected numbers; this would be our dead ally conversations
    //            };
    //            Debug.Log(mappedValue);
    //            // Call your other function with these values
    //            charSupportData.seenEvent(firstPart, secondPart, mappedValue);
    //        }
    //        else
    //        {
    //            //if this is NOT support, this must be the save menu. open save menu again.
    //            miguelConvo.gameObject.SetActive(true);
    //            //Miguel Convo button was not appearing after the first interact. This makes it so if this is the player's first time interacting with the save data, the button still appears.
    //            if (currentlyRunningText.StartsWith("SaveConvo/firstSaveInteract"))
    //            {
    //                miguelConvo.endConversation(true);
    //            }
    //            else
    //            {
    //                miguelConvo.endConversation(false);
    //            }
    //        }

    //        currentlyRunningText = "";
    //        continueTextPrompt.SetActive(false);
    //        top.DOMove(tweenOutPositions[0].transform.position, 2);
    //        bottom.DOMove(tweenOutPositions[1].transform.position, 2);
    //        dialogueBox.DOMove(tweenOutPositions[2].transform.position, 2);
    //    }
    //}

    public void addCutscene(int whichImage)
    {
        ////do a switch statement here for if the cutscene is one that requires changing based on current party members
        //switch (whichImage)
        //{
        //    case 13:
        //        if (!playerController.hasKisa)
        //        {
        //            cutsceneVariations[0].SetActive(false);
        //        }
        //        if (!playerController.hasNicol)
        //        {
        //            cutsceneVariations[1].SetActive(false);
        //        }
        //        if (!playerController.hasSophie)
        //        {
        //            cutsceneVariations[2].SetActive(false);
        //        }
        //        break;
        //    case 2:
        //        if (!playerController.hasKisa)
        //        {
        //            cutsceneVariations[3].SetActive(false);
        //        }
        //        if (!playerController.hasNicol)
        //        {
        //            cutsceneVariations[4].SetActive(false);
        //        }
        //        if (!playerController.hasSophie)
        //        {
        //            cutsceneVariations[5].SetActive(false);
        //        }
        //        if (playerController.hasKisa && !playerController.hasNicol && !playerController.hasSophie) // if this is a kisa only route
        //        {
        //            cutsceneVariations[3].SetActive(false);
        //            cutsceneVariations[6].SetActive(true);
        //        }
        //        break;
        //    case 4:
        //        cutsceneScenes[0].SetActive(false);
        //        break;
        //    case 9:
        //        cutsceneScenes[4].SetActive(false);
        //        cutsceneScenes[5].SetActive(false);
        //        cutsceneScenes[6].SetActive(false);
        //        cutsceneScenes[7].SetActive(false);
        //        cutsceneScenes[8].SetActive(false);
        //        break;
        //    case 15: //Ending with everyone there staring @ lich
        //        if (!playerController.hasKisa)
        //        {
        //            cutsceneVariations[7].SetActive(false);
        //        }
        //        if (!playerController.hasNicol)
        //        {
        //            cutsceneVariations[8].SetActive(false);
        //        }
        //        if (!playerController.hasSophie)
        //        {
        //            cutsceneVariations[9].SetActive(false);
        //            if (playerController.hasNicol) // If Nicol is here and Sophie is not, change his sprite
        //            {
        //                cutsceneVariations[8].SetActive(false);
        //                cutsceneVariations[10].SetActive(true);
        //            }
        //        }
        //        break;
        //}
        if (whichImage >= 0 && whichImage <= cutsceneScenes.Length)
        {
            cutsceneScenes[whichImage].SetActive(true);
            switch(whichImage)
            {
                case 17:
                    cutsceneScenes[whichImage].transform.DOMove(cutsceneScenes[3].transform.position, 1f).SetUpdate(true);
                    break;
                case 18:
                    cutsceneScenes[whichImage-1].transform.DOMove(cutsceneScenes[4].transform.position, 1f).SetUpdate(true);
                    cutsceneScenes[whichImage].transform.DOMove(cutsceneScenes[3].transform.position, 1f).SetUpdate(true);
                    break;
                case 19:
                    cutsceneScenes[whichImage - 1].transform.DOMove(cutsceneScenes[4].transform.position, 1f).SetUpdate(true);
                    cutsceneScenes[whichImage].transform.DOMove(cutsceneScenes[3].transform.position, 1f).SetUpdate(true);
                    break;
                case 20:
                    cutsceneScenes[whichImage].transform.DOMove(cutsceneScenes[4].transform.position, 1f).SetUpdate(true);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("You need to pass in a number from 0 to " + cutsceneScenes.Length.ToString() + "for cutscenes! See _MainDialogueManager game object for the list of cutscenes.");
        }

    }
}
