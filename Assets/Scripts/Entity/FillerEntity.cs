using UnityEngine;
using System;
using System.Collections;

// For use with various entities, which may take up more than one space on the Grid
public class FillerEntity : GridEntity 
{
    public void Interact()
    {
        Debug.Log("Trying to interact with a filler entity?");
    }
}
