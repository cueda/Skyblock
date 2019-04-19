using UnityEngine;
using System;
using System.Collections;

public class FileButtonTest : MonoBehaviour 
{
    public void BeginSaving()
    {
        Debug.Log("Save process started");
        FileSerializer.Save();
        Debug.Log("Save complete! File is at " + Application.persistentDataPath);
    }


    public void BeginDelete()
    {
        Debug.Log("Delete process started");
        FileSerializer.Delete();
        Debug.Log("Delete complete!");
    }
}
