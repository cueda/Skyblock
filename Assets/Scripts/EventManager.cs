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
        public static Action<EntityType> OnItemSelected = delegate { };
    }

    public static class Player
    {

    }

    public static class Values
    {
        public static Action<int, int> OnLifetimeFlowersCollectedChanged = delegate { };
        public static Action<int, int> OnFlowersCollectedChanged = delegate { };
        public static Action<int, int> OnDirtCollectedChanged = delegate { };
        public static Action<int, int> OnKittensCollectedChanged = delegate { };
        public static Action<int, int> OnVasesCollectedChanged = delegate { };
        public static Action<int, int> OnUpgradersCollectedChanged = delegate { };

        public static Action<int, int> OnDirtCostChanged = delegate { };
        public static Action<int, int> OnKittenCostChanged = delegate { };

        public static Action<int, int> OnFlowerValueLevelChanged = delegate { };
        public static Action<int, int> OnKittenGenerateLevelChanged = delegate { };
        public static Action<int, int> OnKittenStorageLevelChanged = delegate { };
    }

    #endregion
}
