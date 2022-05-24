using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickLocation 
{
    public int touchID;
    public GameObject joystick;

    public JoystickLocation(int newTouchID, GameObject newCircle)
    {
        touchID = newTouchID;
        joystick = newCircle;
    }
}
