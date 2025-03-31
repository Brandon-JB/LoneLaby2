using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class KeybindManager : MonoBehaviour
{
    public InputActionAsset controls;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private Dictionary<string, string> defaultBindings = new Dictionary<string, string>();
    public static bool isRebinding = false;

    public TMP_Text attackText, magicText, parryText, interactText; // UI Text for display

    private void Start()
    {
        // Store default key bindings for each action (modify as needed)
        defaultBindings["Attack"] = "<Keyboard>/enter";
        defaultBindings["Magic"] = "<Keyboard>/m";
        defaultBindings["Parry"] = "<Keyboard>/p";
        defaultBindings["Interact"] = "<Keyboard>/e";

        LoadKeybinds(); // Load saved keybinds
    }

    public void ResetKeybind(string actionName)
    {
        InputAction action = controls.FindAction(actionName);
        if (action == null || !defaultBindings.ContainsKey(actionName)) return;

        string defaultBinding = defaultBindings[actionName];

        // Check if any other action is using this default binding
        foreach (var otherAction in controls)
        {
            if (otherAction != action && otherAction.bindings[0].effectivePath == defaultBinding)
            {
                // Recursively reset the conflicting action
                ResetKeybind(otherAction.name);
            }
        }

        // Apply the default binding
        action.ApplyBindingOverride(0, defaultBinding);

        // Update UI
        UpdateButtonLabels();
        SaveKeybinds();
    }

    public void StartRebinding(string actionName, TMP_Text buttonText)
    {
        if (isRebinding) return; // Prevent multiple rebindings
        isRebinding = true; // Mark that we are rebinding

        InputAction action = controls.FindAction(actionName);
        if (action == null) return;

        action.Disable();
        string originalKeyText = buttonText.text;
        buttonText.text = "<size=75%>Press a key</size>";

        rebindOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                string newBinding = action.bindings[0].effectivePath;

                // Restricted keys (WASD)
                string[] restrictedKeys = { "<Keyboard>/w", "<Keyboard>/a", "<Keyboard>/s", "<Keyboard>/d", "<Keyboard>/escape" };
                if (restrictedKeys.Contains(newBinding))
                {
                    StartCoroutine(ShowTemporaryMessage(buttonText, "<size=75%>Invalid Key</size>", originalKeyText));
                    action.Enable();
                    rebindOperation.Dispose();
                    return;
                }

                // Check for duplicate key
                foreach (var existingAction in controls)
                {
                    if (existingAction != action && existingAction.bindings[0].effectivePath == newBinding)
                    {
                        StartCoroutine(ShowTemporaryMessage(buttonText, "<size=75%>Key is in use!</size>", originalKeyText));
                        action.Enable();
                        rebindOperation.Dispose();
                        return;
                    }
                }


                Debug.Log("Applying");
                // Apply new binding
                action.ApplyBindingOverride(0, newBinding);
                buttonText.text = action.bindings[0].ToDisplayString();

                action.Enable();
                rebindOperation.Dispose();
                isRebinding = false; // Reset flag
            })
            .Start();
    }


    private IEnumerator ShowTemporaryMessage(TMP_Text buttonText, string message, string originalKeyText)
    {
        // Save the original button text
        string originalText = buttonText.text;
        // Display the temporary error message
        buttonText.text = message;
        yield return new WaitForSecondsRealtime(1f); // Wait for 1 second
                                             // Revert back to the original key text
        buttonText.text = originalKeyText;
        isRebinding = false; // Reset flag
    }


    private void UpdateButtonLabels()
    {
        attackText.text = controls.FindAction("Attack").bindings[0].ToDisplayString();
        magicText.text = controls.FindAction("Magic").bindings[0].ToDisplayString();
        parryText.text = controls.FindAction("Parry").bindings[0].ToDisplayString();
        interactText.text = controls.FindAction("Interact").bindings[0].ToDisplayString();
    }

    public void ResetAttack() => ResetKeybind("Attack");
    public void ResetMagic() => ResetKeybind("Magic");
    public void ResetParry() => ResetKeybind("Parry");
    public void ResetInteract() => ResetKeybind("Interact");
    public void RebindAttack() => StartRebinding("Attack", attackText);
    public void RebindMagic() => StartRebinding("Magic", magicText);
    public void RebindParry() => StartRebinding("Parry", parryText);
    public void RebindInteract() => StartRebinding("Interact", interactText);

    //Called on every change
    public void SaveKeybinds()
    {
        string rebinds = controls.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("Keybinds", rebinds);
        PlayerPrefs.Save();
    }

    public void LoadKeybinds()
    {
        if (PlayerPrefs.HasKey("Keybinds"))
        {
            string rebinds = PlayerPrefs.GetString("Keybinds");
            controls.LoadBindingOverridesFromJson(rebinds);
            UpdateButtonLabels(); // Refresh UI
        }
    }

    public void CancelRebinding()
    {
        if (isRebinding)
        {
            rebindOperation?.Cancel(); // Cancel the operation if it's running
            rebindOperation?.Dispose();
            isRebinding = false;
            OpenPauseMenu.GLOBALcanOpenPause = true;
        }
    }


}

