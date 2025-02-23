using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool attackPressed;
    public static bool magicPressed;
    public static bool parryPressed;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction magicAction;
    private InputAction parryAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Movement"];

        attackAction = playerInput.actions["Attack"];

        magicAction = playerInput.actions["Magic"];

        parryAction = playerInput.actions["Parry"];
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();

        if (attackAction.WasPressedThisFrame() )
        {
            attackPressed = true;
        }
        else
        {
            attackPressed = false;
        }

        if (magicAction.WasPressedThisFrame() )
        {
            magicPressed = true;
        }
        else
        {
            magicPressed = false;
        }

        if (parryAction.WasPressedThisFrame() )
        {
            parryPressed = true;
        }
        else
        {
            parryPressed = false;
        }
    }


}
