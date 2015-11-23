using UnityEngine;
using System;
using System.Collections;

public class FileAutoSaver : MonoBehaviour 
{
    // For some reason, the game pauses at the beginning.
    // This causes a faulty save before entities have spawned.
    // This bool prevents saving before the start function has been called for all entities.
    private bool hasAlreadyStarted = false;

    void Start()
    {
        hasAlreadyStarted = true;
    }

	void OnApplicationPause()
    {
        if(hasAlreadyStarted)
        {
            // Only perform this saving behavior for touch screens
            if (TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
                FileSerializer.Save();
            }
        }        
    }

    void OnApplicationQuit()
    {
        // Save for all systems when game is closed
        FileSerializer.Save();
    }

}
