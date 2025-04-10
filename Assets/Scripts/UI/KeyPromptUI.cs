using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class KeyPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private InputActionReference interactAction;

    private static readonly Dictionary<string, string> keyShortener = new Dictionary<string, string>()
    {
        // Common keys
        { "Space", "Space" },
        { "Enter", "Enter" },
        { "Escape", "Esc" },
        { "Backspace", "Bksp" },
        { "Tab", "Tab" },
        { "Shift", "Shift" },
        { "Left Shift", "LShift" },
        { "Right Shift", "RShift" },
        { "Control", "Ctrl" },
        { "Left Control", "LCtrl" },
        { "Right Control", "RCtrl" },
        { "Alt", "Alt" },
        { "Left Alt", "LAlt" },
        { "Right Alt", "RAlt" },
        { "Caps Lock", "Caps" },
        { "Command", "Cmd" },
        { "Left Command", "LCmd" },
        { "Right Command", "RCmd" },

        // Arrow keys
        { "Up Arrow", "Up" },
        { "Down Arrow", "Down" },
        { "Left Arrow", "Left" },
        { "Right Arrow", "Right" },

        // Function keys
        { "F1", "F1" }, { "F2", "F2" }, { "F3", "F3" }, { "F4", "F4" },
        { "F5", "F5" }, { "F6", "F6" }, { "F7", "F7" }, { "F8", "F8" },
        { "F9", "F9" }, { "F10", "F10" }, { "F11", "F11" }, { "F12", "F12" },

        // Mouse
        { "Mouse Left", "LMB" },
        { "Mouse Right", "RMB" },
        { "Mouse Middle", "MMB" },
        { "Mouse ScrollWheel", "Scroll" },

        // Symbols / punctuation
        { "Backquote", "`" },
        { "Slash", "/" },
        { "Backslash", "\\" },
        { "Semicolon", ";" },
        { "Quote", "'" },
        { "Period", "." },
        { "Comma", "," },
        { "Minus", "-" },
        { "Equals", "=" },

        // Numpad
        { "Numpad Enter", "NEnter" },
        { "Numpad Plus", "N+" },
        { "Numpad Minus", "N-" },
        { "Numpad Multiply", "N*" },
        { "Numpad Divide", "N/" },

        // Gamepad (optional)
        { "Button South", "A" },
        { "Button East", "B" },
        { "Button West", "X" },
        { "Button North", "Y" }
    };

    private void OnEnable()
    {
        if (promptText == null || interactAction == null)
        {
            Debug.LogWarning("KeyPromptUI: Missing references.");
            return;
        }

        string controlName = GetShortenedKeyName();
        promptText.text = controlName;
    }

    private string GetShortenedKeyName()
    {
        InputBinding binding = interactAction.action.bindings[0];

        string displayName = InputControlPath.ToHumanReadableString(
            binding.effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );

        if (keyShortener.TryGetValue(displayName, out string shortName))
        {
            return shortName;
        }

        return displayName.Length > 10 ? displayName.Substring(0, 10) + "…" : displayName;
    }
}
