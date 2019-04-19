using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerActionInteract))]
[RequireComponent(typeof(PlayerActionItem))]
public class PlayerAction : MonoBehaviour 
{
    [SerializeField]
    private float buttonHoldTime = .5f;

    private PlayerGridPosition playerGridPosition;
    private PlayerActionInteract playerInteract;
    private PlayerActionItem playerItem;

    private bool performInteractNextFrame;
    private bool useItemNextFrame;


	void Awake()
    {
        playerGridPosition = GetComponent<PlayerGridPosition>();
        playerInteract = GetComponent<PlayerActionInteract>();
        playerItem = GetComponent<PlayerActionItem>();

        EventManager.Game.OnStateSet += OnStateSet;
	}
	

	void OnEnable()
	{
        performInteractNextFrame = false;
        useItemNextFrame = false;
	}
	
	
	void Update() 
	{
        // Begin coroutine for determining function for Action button press
        if(AgnosticInputHandler.IsActionPressed && GameState.IsCurrentState(GameState.State.MOVE))
        {
            StartCoroutine(RegisterInteractInput());
        }

        // Begin interaction (held Action button from coroutine) with game object nearest player coordinates
        if(performInteractNextFrame)
        {
            playerInteract.InteractWithEntity();
            performInteractNextFrame = false;
        }
        // Begin use of inventory item (tapped Action button) at existing coordinates
        else if (useItemNextFrame)
        {
            playerItem.UseCurrentItem();
            useItemNextFrame = false;
        }
	}


    // Timer to record an Action button touch, to read holds.
    // Prevents pressing in one location and releasing at a different location as a valid input.
    IEnumerator RegisterInteractInput()
    {
        GameGridCoords initialGridPosition = playerGridPosition.GetPosition();
        bool hasButtonBeenReleased = false;

        for (float timer = 0f; timer < buttonHoldTime; timer += Time.deltaTime )
        {
            if(AgnosticInputHandler.IsActionReleased)
            {
                hasButtonBeenReleased = true;
                break;
            }
            yield return null;
        }

        if(!hasButtonBeenReleased && playerGridPosition.GetPosition().Equals(initialGridPosition))
        {
            performInteractNextFrame = true;
        }
        else
        {
            useItemNextFrame = true;
        }
    }

    void OnStateSet(GameState.State newState)
    {
        this.enabled = (newState == GameState.State.MOVE);
    }
}
