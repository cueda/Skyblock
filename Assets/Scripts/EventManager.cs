using UnityEngine;
using System;
using System.Collections;

public class EventManager : MonoBehaviour
{
    #region Singleton Methods
    private static EventManager instance = null;
    public static EventManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Events

    /// <summary>
    /// Example class for how to set up Actions
    /// Please follow this convention
    /// </summary>
    public static class Example
    {
        public static Action Fu = delegate { };
    }

    public static class Game
    {
        public static Action<GameState.State> OnStateSet = delegate { };
        public static Action OnItemSelectionMoved = delegate { };
        public static Action<EntityType> OnItemSelected = delegate { };
    }

    public static class Player
    {

    }

    #endregion
}
