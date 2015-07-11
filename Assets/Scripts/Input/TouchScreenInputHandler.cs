using UnityEngine;
using System;
using System.Collections;

public class TouchScreenInputHandler : MonoBehaviour 
{
    #if UNITY_ANDROID || UNITY_IPHONE
    public const bool IS_TOUCH_SCREEN = true;
    #else
    public const bool IS_TOUCH_SCREEN = false;
    #endif

    [SerializeField]
    private GameObject TouchInputPanel;

    void Awake()
    {
        if(!IS_TOUCH_SCREEN)
        {
            TouchInputPanel.SetActive(false);
        }
        else
        {
            EventManager.Game.OnStateSet += OnStateSet;
        }
    }

    
    void OnStateSet(GameState.State state)
    {
        if(state != GameState.State.MOVE)
        {
            TouchInputPanel.SetActive(false);
        }
        else
        {
            TouchInputPanel.SetActive(true);
        }
    }


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
