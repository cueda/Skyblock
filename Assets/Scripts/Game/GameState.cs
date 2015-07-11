using UnityEngine;
using System;
using System.Collections;

public class GameState : MonoBehaviour 
{
    private static State currentState;

    public enum State
    {
        MOVE,
        TARGET,
        INVENTORY,
        BALLOON,
        MENU
    }

    void Awake()
    {
        EventManager.Game.OnStateSet += OnStateSet;
    }

    void Start()
    {
        // Temporarily, the game will default to Move state
        EventManager.Game.OnStateSet(State.MOVE);
    }

    void OnStateSet(State newState)
    {
        currentState = newState;
    }

    // Use this function to quickly check if game state is currently of a given type
    public static bool IsCurrentState(State state)
    {
        return state == currentState;
    }
}
