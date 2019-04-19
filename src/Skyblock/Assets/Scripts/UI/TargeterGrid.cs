using UnityEngine;
using System;
using System.Collections;

public class TargeterGrid : MonoBehaviour 
{
    [SerializeField]
    private PlayerGridPosition playerPosition;

	void Awake()
    {
        // Needs to be added to delegate, even before first enable
        EventManager.Game.OnStateSet += OnStateSet;

        gameObject.SetActive(false);
	}

    void OnEnable()
    {
        // Enable this object only when in Target state
        // Remove any additional subscriptions to delegate before adding
        EventManager.Game.OnStateSet -= OnStateSet;
        EventManager.Game.OnStateSet += OnStateSet;
    }


    void OnDisable()
    {
        // Enable this object only when in Target state
        EventManager.Game.OnStateSet += OnStateSet;
    }

    void Update()
    {
        transform.position = playerPosition.GetPosition() * GameGridCoords.WORLD_DIST_PER_UNIT;
    }

    void OnStateSet(GameState.State state)
    {
        if (state == GameState.State.TARGET)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
