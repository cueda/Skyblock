using UnityEngine;
using System;
using System.Collections;

public class TestInput : MonoBehaviour 
{
	void Update()
    {
        if (AgnosticInputHandler.IsLeftPressed)
        {
            Debug.Log("Left has been pressed this frame");
        }
        if (AgnosticInputHandler.IsLeftHeld)
        {
            Debug.Log("Left has been held this frame");
        }
        if (AgnosticInputHandler.IsLeftReleased)
        {
            Debug.Log("Left has been released this frame");
        }
	}
}
