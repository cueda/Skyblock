using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour 
{
    [SerializeField]
	private Spawner spawner;
    [SerializeField]
    private PlayerGridPosition playerPosition;
    [SerializeField]
    private float rangeLimit = 3;

    private SpriteRenderer spRenderer;
    private GameGridCoords relativeGridPosition;    // Used for Controller targeting system only

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
        if(!TouchScreenInputHandler.IS_TOUCH_SCREEN)
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
        // Just a sanity check: make sure movement only happens in Target mode
        if (!GameState.IsCurrentState(GameState.State.TARGET))
        {
            Debug.LogError("Targeter is enabled but GameState not in TARGET mode.");
        }

        if (!TouchScreenInputHandler.IS_TOUCH_SCREEN)
        {
            // Read in digital controller input
            ControllerDirectionInput();
            transform.position = (playerPosition.GetPosition() + relativeGridPosition) * GameGridCoords.WORLD_DIST_PER_UNIT;

            // Perform actions or exit target mode
            if (Input.GetButtonDown("Action"))
            {
                PerformActionAtLocation();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                EventManager.Game.OnStateSet(GameState.State.MOVE);
            }
        }
        else
        {
            // If screen clicked or touched this frame
            if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                // Read in touchscreen positional input
                GameGridCoords touchCoordinates = GetTouchCoordinates();

                // Perform actions immediately at location
                // If on valid location, perform function
                // If on invalid location (outside range), cancel targeter
                if(touchCoordinates == null)
                {
                    EventManager.Game.OnStateSet(GameState.State.MOVE);
                }
                else
                {
                    transform.position = touchCoordinates * GameGridCoords.WORLD_DIST_PER_UNIT;
                    PerformActionAtLocation();
                }
            }
        }        
	}

    
    /// <summary>
    /// Takes in controller input (Horizontal, Vertical) to determine if targeter is moved this frame.
    /// </summary>
    private void ControllerDirectionInput()
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


    /// <summary>
    /// Takes in touch input or mouse input (Raycast at game objects) to determine where targeter is moved this frame.
    /// Returns coordinates of touched or clicked location on targeter as a GameGridCoords.
    /// If targeter was not clicked, returns null.
    /// </summary>
    private GameGridCoords GetTouchCoordinates()
    { 
        if(Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                return new GameGridCoords(hit.point);
            }
            else
            {
                return null;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log(ray);
            if (Physics.Raycast(ray, out hit))
            {
                return new GameGridCoords(hit.point);
            }
            else
            {
                return null;
            }
        }
        Debug.LogError("Called GetTouchCoordinates, but no click nor touch was found.");
        return null;
    }


    // Perform an action at the given location.
    // Action performed is based on current targeted location and current item selected.
    private void PerformActionAtLocation()
    {
        GameGridCoords currentGridPosition = new GameGridCoords(transform.position);

        switch(GameData.Instance.currentItem)
        {
            case EntityType.DIRT:
                if (!GameGrid.Instance.IsCoordinatesOccupied(currentGridPosition) && !GameGrid.Instance.IsCoordinatesOccupied(currentGridPosition + new GameGridCoords(0, -1)))
                {
                    // Check for dirt in inventory.
                    if (GameData.Instance.dirtCollected > 0)
                    {
                        // Add dirt block and use one dirt resource.
                        spawner.SpawnDirtBlock(currentGridPosition);
                        GameData.Instance.dirtCollected--;
                    }
                }
                break;
            case EntityType.SHOVEL:
                GameObject objectAtTarget = GameGrid.Instance.GetObject(currentGridPosition);
                if (objectAtTarget != null)
                {
                    if (objectAtTarget.tag == "GroundDirt" || objectAtTarget.tag == "GroundGrass")
                    {
                        spawner.RemoveObject(objectAtTarget);
                        GameData.Instance.dirtCollected++;
                    }
                }
                break;
            default:
                break;
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
