using UnityEngine;
using System.Collections;

/// <summary>
/// A coordinate system for GameGrid, should be more efficient and safer than Vector2 as keys in Dictionary.
/// </summary> 
public class GameGridCoords {

    public static int WORLD_DIST_PER_UNIT = 2;

	public short x, y;

    /// <summary>
    /// Basic constructor
    /// </summary>
	public GameGridCoords(short newX, short newY)
	{
		x = newX;
        y = newY;
	}
    

    /// <summary>
    /// Takes in a Vector2 and produces a 2D coordinate with short values.
    /// Potential for Vector2 to be converted to the wrong respective coordinate here due to floating point error,
    /// this constructor attempts to mitigate this issue through rounding.
    /// The world position Vector2 is divided by two to match grid units properly (2 units world space = 1 unit grid)
    /// Additionally, a unit is represented by the center of its position. (0,0 is a position centered on world space Vector2.zero)
    /// </summary>
    public GameGridCoords(Vector2 coords)
    {
        coords /= WORLD_DIST_PER_UNIT;

        x = (short)Mathf.Round(coords.x);
        y = (short)Mathf.Round(coords.y);

        //Debug.Log("Created new coordinates at " + x + ", " + y);
    }


    public static GameGridCoords operator +(GameGridCoords obj1, GameGridCoords obj2)
    {
        return new GameGridCoords((short)(obj1.x + obj2.x), (short)(obj1.y + obj2.y));
    }

    public static Vector2 operator *(GameGridCoords obj, int scalar)
    {
        return new Vector2(obj.x * scalar, obj.y * scalar);
    }

    public Vector2 ToWorldSpace()
    {
        return new Vector2(x * WORLD_DIST_PER_UNIT, y * WORLD_DIST_PER_UNIT);
    }

    public override string ToString()
    {
        return x.ToString() + ", " + y.ToString();
    }
}
