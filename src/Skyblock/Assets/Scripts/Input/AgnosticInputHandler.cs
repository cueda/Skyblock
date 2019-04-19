using UnityEngine;
using System;
using System.Collections;

public class AgnosticInputHandler : MonoBehaviour
{
    private bool isLeftPressed, isRightPressed, isDownPressed, isUpPressed, isJumpPressed, isActionPressed;
    private bool isLeftHeld, isRightHeld, isDownHeld, isUpHeld, isJumpHeld, isActionHeld;
    private bool isLeftReleased, isRightReleased, isDownReleased, isUpReleased, isJumpReleased, isActionReleased;

    #region Public (Static) Accessors

    public static bool IsLeftPressed { get { return instance.isLeftPressed; } }
    public static bool IsRightPressed { get { return instance.isRightPressed; } }
    public static bool IsDownPressed { get { return instance.isDownPressed; } }
    public static bool IsUpPressed { get { return instance.isUpPressed; } }
    public static bool IsJumpPressed { get { return instance.isJumpPressed; } }
    public static bool IsActionPressed { get { return instance.isActionPressed; } }

    public static bool IsLeftHeld { get { return instance.isLeftHeld; } }
    public static bool IsRightHeld { get { return instance.isRightHeld; } }
    public static bool IsDownHeld { get { return instance.isDownHeld; } }
    public static bool IsUpHeld { get { return instance.isUpHeld; } }
    public static bool IsJumpHeld { get { return instance.isJumpHeld; } }
    public static bool IsActionHeld { get { return instance.isActionHeld; } }

    public static bool IsLeftReleased { get { return instance.isLeftReleased; } }
    public static bool IsRightReleased { get { return instance.isRightReleased; } }
    public static bool IsDownReleased { get { return instance.isDownReleased; } }
    public static bool IsUpReleased { get { return instance.isUpReleased; } }
    public static bool IsJumpReleased { get { return instance.isJumpReleased; } }
    public static bool IsActionReleased { get { return instance.isActionReleased; } }

    #endregion

    #region Singleton Implementation

    private static AgnosticInputHandler instance = null;
    public static AgnosticInputHandler Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Input variable setters

    public void LeftPressed()
    {
        isLeftPressed = true;
        isLeftHeld = true;
    }

    public void RightPressed()
    {
        isRightPressed = true;
        isRightHeld = true;
    }

    public void DownPressed()
    {
        isDownPressed = true;
        isDownHeld = true;
    }

    public void UpPressed()
    {
        isUpPressed = true;
        isUpHeld = true;
    }

    public void JumpPressed()
    {
        isJumpPressed = true;
        isJumpHeld = true;
    }

    public void ActionPressed()
    {
        isActionPressed = true;
        isActionHeld = true;
    }

    public void LeftReleased()
    {
        isLeftReleased = true;
        isLeftHeld = false;
    }

    public void RightReleased()
    {
        isRightReleased = true;
        isRightHeld = false;
    }

    public void DownReleased()
    {
        isDownReleased = true;
        isDownHeld = false;
    }

    public void UpReleased()
    {
        isUpReleased = true;
        isUpHeld = false;
    }

    public void JumpReleased()
    {
        isJumpReleased = true;
        isJumpHeld = false;
    }

    public void ActionReleased()
    {
        isActionReleased = true;
        isActionHeld = false;
    }

    #endregion

    // Resets isPressed and isReleased in preparation for the next frame.
    void LateUpdate()
    {
        isLeftPressed = isRightPressed = isDownPressed = isUpPressed = isJumpPressed = isActionPressed = false;
        isLeftReleased = isRightReleased = isDownReleased = isUpReleased = isJumpReleased = isActionReleased = false;
    }


}
