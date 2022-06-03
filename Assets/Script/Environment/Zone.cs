using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zone
{
    public Vector2 topLeftCorner;
    public Vector2 bottomRightCorner;
    public float security;

    public bool CollideWithSecurityZone(Vector2 position)
    {
        if ((position.x < bottomRightCorner.x && position.x > topLeftCorner.x) && (position.y < topLeftCorner.y + security && position.y > topLeftCorner.y))
            return true;
        else
            return false;
    }

    public bool CollideWithZone(Vector2 position)
    {
        if ((position.x < bottomRightCorner.x && position.x > topLeftCorner.x) && (position.y < topLeftCorner.y && position.y > bottomRightCorner.y))
            return true;
        else
            return false;
    }
}