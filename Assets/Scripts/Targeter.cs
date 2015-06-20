using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour 
{
    [SerializeField]
    private bool isTouchScreen;
    [SerializeField]
	private Spawner spawner;
    [SerializeField]
    private PlayerGridPosition playerPosition;
    [SerializeField]
    private float rangeLimit = 3;

    private SpriteRenderer spRenderer;
    private GameGridCoords relativeGridPosition;

    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        
        // Enable this object only when in Target state
        EventManager.Game.OnStateSet += OnStateSet;

        gameObject.SetActive(false);
    }
	void OnEnable ()
    {
        // Base visibility based on whether enabling action (menu selection, button) is done through Touch or Controller
        if(!isTouchScreen)
        {
            spRenderer.enabled = true;
        }

        relativeGridPosition = new GameGridCoords(0, 0);
	}

    void OnDisable ()
    {
        spRenderer.enabled = false;
        relativeGridPosition = new GameGridCoords(0, 0);
    }


	// Update is called once per frame
	void Update () 
	{
        if(!isTouchScreen)
        {
            ControllerDirectionInput();
            transform.position = (playerPosition.GetPosition() + relativeGridPosition) * GameGridCoords.WORLD_DIST_PER_UNIT;
        }
        
        // ==============  OLD CODE FOR MOUSE AIMING ==============
        /*
        Vector3 inputPosition = Vector3.zero;
		inputPosition = Camera.main.ScreenToWorldPoint(inputPosition);
		//Debug.Log(v3);
		inputPosition.x = (Mathf.Floor((inputPosition.x + 1) / 2) * 2);
		inputPosition.y = (Mathf.Floor((inputPosition.y + 1) / 2) * 2);
		inputPosition.z = -5.99f; // Making sure those rays are going through the dirt tiles!
		transform.position = inputPosition;
		//Debug.Log(v3);
         */

        GameGridCoords currentGridPosition = new GameGridCoords(transform.position);

		// If left-clicking on a location while in placement mode, place selected block.
		// Check the relevant location to confirm open space.
        if (Input.GetButtonDown("Action") && GameState.IsCurrentState(GameState.State.TARGET)) // && GUIUtility.hotControl == 0)
		{
			// Check that the targeted space is open.
			if(!GameGrid.Instance.IsCoordinatesOccupied(currentGridPosition))
			{
				// For dirt placement (and other floating blocks in the future)
				if(GameData.Instance.currentItem.Equals("dirt"))
				{
					// Check for dirt in inventory.
					if(GameData.Instance.dirtCollected > 0)
					{
						// Add dirt block and use one dirt resource.
						spawner.SpawnDirtBlock(currentGridPosition);
						GameData.Instance.dirtCollected--;
					}
				}

				// For all blocks requiring a floor, check for object in space below.
				else if (GameGrid.Instance.IsCoordinatesOccupied(currentGridPosition + new GameGridCoords(-Vector2.up*2)))
				{
					// Check that this space is a dirt/grass block.
                    string selectedObjTag = GameGrid.Instance.GetObject(currentGridPosition + new GameGridCoords(-Vector2.up * 2)).tag;
					if(selectedObjTag.Equals("GroundDirt") || selectedObjTag.Equals("GroundGrass"))
					{
						// If selected item is flower
						if(GameData.Instance.currentItem.Equals ("flower"))
						{
							// Spawn a flower at targeted location.
							spawner.SpawnFlower(currentGridPosition);
						}

						// If selected item is vase
						if(GameData.Instance.currentItem.Equals ("vase") && GameData.Instance.vasesCollected > 0)
						{
							// Spawn a vase at targeted location and consume one vase resource.
							spawner.SpawnVase(currentGridPosition);
							GameData.Instance.vasesCollected--;
						}

						// If selected item is kitten
						if(GameData.Instance.currentItem.Equals ("kitten") && GameData.Instance.kittensCollected > 0)
						{
							// Spawn a kitten at targeted location and consume one kitten resource.
                            spawner.SpawnKitten(currentGridPosition);
							GameData.Instance.kittensCollected--;
						}
						
						// If selected item is upgrader
						if(GameData.Instance.currentItem.Equals ("upgrader") && GameData.Instance.upgraderCollected > 0)
						{
							// Spawn an upgrader at targeted location and consume one upgrader resource.
                            spawner.SpawnUpgrader(currentGridPosition);
							GameData.Instance.upgraderCollected--;
						}
					}
				}
			}
		}

		// Remove ground block of any type if right-clicked with Targeter on, and setting is Dirt
		// Currently return a dirt block to inventory
        // TODO: leave method to remove dirt block with alternate fire? Or add "remover" tool using Action command?
        //if(Input.GetButtonDown("Fire2") && GameState.Instance.currentItem.Equals("dirt") && GUIUtility.hotControl == 0)
        //{
        //    RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, 1 << LayerMask.NameToLayer("Ground"));

        //    if(hit.collider != null)
        //    {
        //        spawner.RemoveObject(hit.collider.gameObject);
        //        GameState.Instance.dirtCollected++;
        //    }
        //}
	}

    /// <summary>
    /// Takes in controller input (Horizontal, Vertical) to determine if targeter is moved this frame.
    /// </summary>
    private void ControllerDirectionInput()
    {
        // Just a sanity check: make sure movement only happens in Target mode
        if(GameState.IsCurrentState(GameState.State.TARGET))
        {
            GameGridCoords initialPosition = new GameGridCoords(relativeGridPosition.x, relativeGridPosition.y);

            if (AgnosticInputHandler.IsLeftPressed)
            {
                relativeGridPosition.x -= 1;
            }
            if (AgnosticInputHandler.IsRightPressed)
            {
                relativeGridPosition.x += 1;
            }
            if (AgnosticInputHandler.IsDownPressed)
            {
                relativeGridPosition.y -= 1;
            }
            if (AgnosticInputHandler.IsUpPressed)
            {
                relativeGridPosition.y += 1;
            }

            if (Mathf.Abs(relativeGridPosition.x) + Mathf.Abs(relativeGridPosition.y) > rangeLimit)
            {
                relativeGridPosition = initialPosition;
            }
        }
        else
        {
            Debug.LogError("Input attempted in Targeter but GameState not in TARGET mode.");
        }
    }

    void OnStateSet(GameState.State state)
    {
        if(state == GameState.State.TARGET)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
