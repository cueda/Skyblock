using UnityEngine;
using System;
using System.Collections;

public class TouchScreenInputHandler : MonoBehaviour 
{

    public void OnLeftPressed()
    {
        AgnosticInputHandler.Instance.LeftPressed();
    }

    public void OnLeftReleased()
    {
        AgnosticInputHandler.Instance.LeftReleased();
    }

    public void OnRightPressed()
    {
        AgnosticInputHandler.Instance.RightPressed();
    }

    public void OnRightReleased()
    {
        AgnosticInputHandler.Instance.RightReleased();
    }

    public void OnJumpPressed()
    {
        AgnosticInputHandler.Instance.JumpPressed();
    }

    public void OnJumpReleased()
    {
        AgnosticInputHandler.Instance.JumpReleased();
    }

    public void OnActionPressed()
    {
        AgnosticInputHandler.Instance.ActionPressed();
    }

    public void OnActionReleased()
    {
        AgnosticInputHandler.Instance.ActionReleased();
    }
}
