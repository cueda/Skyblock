using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDisplay : MonoBehaviour
{
    private Text textDisplay;

    void Awake ()
    {
        textDisplay = GetComponent<Text>();

#if UNITY_ANDROID || UNITY_IOS
        textDisplay.text = "Move: Touch left/right\n" + 
                           "Jump: Touch bottom\n" +
                           "Use Item: Tap center\n" +
                           "Interact: Hold center";

#elif UNITY_STANDALONE || UNITY_EDITOR
        if (IsControllerConnected())
        {
            textDisplay.text = "Move: Left Stick\n" +
                              "Jump: Bottom BTN\n" +
                              "Use Item: Left BTN (tap)\n" +
                              "Interact: Left BTN (hold)\n" +
                              "Menu: Top BTN";
        }
        else
        {
            textDisplay.text = "Move: Left/Right\n" +
                              "Jump: X\n" +
                              "Use Item: Z (tap)\n" +
                              "Interact: Z (hold)\n" +
                              "Menu: C";
        }
#endif
    }
	

	void Update () 
	{
		
	}


    bool IsControllerConnected ()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Check if the string is empty or not
            if (!string.IsNullOrEmpty(temp[0]))
            {
                //Not empty, controller temp[i] is connected
                Debug.Log("Controller is connected.");
                return true;
            }
            else
            {
                //If it is empty, controller i is disconnected
                //where i indicates the controller number
                Debug.Log("Controller disconnected.");
                return false;
            }
        }
        else
        {
            Debug.Log("No controllers found.");
            return false;
        }
    }
}
