using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension 
{
    public static Vector2 Round(this Vector3 position)
    {
        position.x = Mathf.RoundToInt(position.x);
        position.y = Mathf.RoundToInt(position.y);
        return new Vector2((int)position.x, (int)position.y);
    }
 
}
