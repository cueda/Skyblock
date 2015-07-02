using UnityEngine;
using System;
using System.Collections;

public class TargeterGrid : MonoBehaviour 
{
    [SerializeField]
    private PlayerGridPosition playerPosition;

	void Awake()
    {
        // Enable this object only when in Target state
        EventManager.Game.OnStateSet += OnStateSet;

        gameObject.SetActive(false);
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
