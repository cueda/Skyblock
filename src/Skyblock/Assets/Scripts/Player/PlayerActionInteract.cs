using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Handles player's interaction with entities on the GameGrid.
/// Serves as a compartment of PlayerAction, to prevent bloating the code.
/// </summary>
[RequireComponent(typeof(PlayerAction))]
public class PlayerActionInteract : MonoBehaviour
{
    private PlayerGridPosition playerGridPosition;      


	void Awake()
    {
        playerGridPosition = GetComponent<PlayerGridPosition>();
	}
	

	public void InteractWithEntity()
    {
        GridEntity entity = GameGrid.Instance.GetObjectAt(playerGridPosition.GetPosition());

        if(entity != null)
        {
            entity.Interact();
        }
    }
}
