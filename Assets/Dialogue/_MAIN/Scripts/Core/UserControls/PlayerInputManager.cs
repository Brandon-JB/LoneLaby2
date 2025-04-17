using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        //look for open pause menu
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(audioStatics.keycodeInterractButton)) && !OpenPauseMenu.pauseOpened)
        {
            PromptAdvance();
            continueButton.SetActive(false);
        }
    }
    public void PromptAdvance()
    {
        DialogueSystem.instance.OnUserPrompt_Next();
    }
}
