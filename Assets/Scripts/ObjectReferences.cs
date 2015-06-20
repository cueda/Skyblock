using UnityEngine;
using System;
using System.Collections;

public class ObjectReferences : MonoBehaviour 
{
    public static ObjectReferences instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region Player and components
    [Space(10)]
    [SerializeField]
    private Transform _player = null;

    public static Transform player
    {
        get { return instance._player; }
    }
    #endregion

    #region Camera and components
    [Space(10)]
    [SerializeField]
    private Transform _mainCamera = null;

    public static Transform mainCamera
    {
        get { return instance._mainCamera; }
    }
    #endregion
}
