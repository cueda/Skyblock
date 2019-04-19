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

    #region Player transform and components
    [Space(10)]
    [SerializeField]
    private Transform _player = null;

    public static Transform player
    {
        get { return instance._player; }
    }
    #endregion

    #region Camera transform and components
    [Space(10)]
    [SerializeField]
    private Transform _mainCamera = null;

    public static Transform mainCamera
    {
        get { return instance._mainCamera; }
    }
    #endregion

    #region Spawner
    [Space(10)]
    [SerializeField]
    private Spawner _spawner = null;

    public static Spawner spawner
    {
        get { return instance._spawner; }
    }
    #endregion
}
