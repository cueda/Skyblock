using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Controller joystick / button / keyboard input handling
/// </summary>
public class ControllerInputHandler : MonoBehaviour 
{
    [SerializeField]
    private float controllerDeadZone = .15f;
    
    // Controller/keyboard specific bool conditions for IsHeld
    // otherwise touch input is overwritten by lack of controller input
    private bool IsControllerLeftHeld, IsControllerRightHeld, IsControllerUpHeld, IsControllerDownHeld;
    private bool IsControllerActionHeld, IsControllerJumpHeld;

	void Update()
    {
        HandleHorizontalInput();
        HandleVerticalInput();
        HandleButtons();
	}

    private void HandleHorizontalInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // If input is left (i.e. negative input)        
        if (horizontalInput < -1f * controllerDeadZone && !IsControllerLeftHeld)
        {
            IsControllerLeftHeld = true;
            AgnosticInputHandler.Instance.LeftPressed();
        }
        if (horizontalInput > -1f * controllerDeadZone && IsControllerLeftHeld)
        {
            IsControllerLeftHeld = false;
            AgnosticInputHandler.Instance.LeftReleased();
        }

        // If input is right (i.e. positive input)
        if (horizontalInput > controllerDeadZone && !IsControllerRightHeld)
        {
            IsControllerRightHeld = true;
            AgnosticInputHandler.Instance.RightPressed();
        }
        if (horizontalInput < controllerDeadZone && IsControllerRightHeld)
        {
            IsControllerRightHeld = false;
            AgnosticInputHandler.Instance.RightReleased();
        }
    }

    private void HandleVerticalInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        // If input is up (i.e. positive input)
        if (verticalInput > controllerDeadZone && IsControllerUpHeld)
        {
            IsControllerUpHeld = false;
            AgnosticInputHandler.Instance.UpPressed();
        }
        if (verticalInput < controllerDeadZone && !IsControllerUpHeld)
        {
            IsControllerUpHeld = true;
            AgnosticInputHandler.Instance.UpReleased();
        }

        // If input is down (i.e. negative input)
        if (verticalInput < -1f * controllerDeadZone && IsControllerDownHeld)
        {
            IsControllerDownHeld = false;
            AgnosticInputHandler.Instance.DownPressed();
        }
        if (verticalInput > -1f * controllerDeadZone && !IsControllerDownHeld)
        {
            IsControllerDownHeld = true;
            AgnosticInputHandler.Instance.DownReleased();
        }
    }

    // Handles Jump and Interact button states
    private void HandleButtons()
    {
        // Action button states
        if(Input.GetButtonDown("Action") && !IsControllerActionHeld)
        {
            IsControllerActionHeld = true;
            AgnosticInputHandler.Instance.ActionPressed();
        }
        if(Input.GetButtonUp("Action") && IsControllerActionHeld)
        {
            IsControllerActionHeld = false;
            AgnosticInputHandler.Instance.ActionReleased();
        }

        // Jump button states
        if(Input.GetButtonDown("Jump") && !IsControllerJumpHeld)
        {
            IsControllerJumpHeld = true;
            AgnosticInputHandler.Instance.JumpPressed();
        }
        if(Input.GetButtonUp("Jump") && IsControllerJumpHeld)
        {
            IsControllerJumpHeld = false;
            AgnosticInputHandler.Instance.JumpReleased();
        }
    }
}
