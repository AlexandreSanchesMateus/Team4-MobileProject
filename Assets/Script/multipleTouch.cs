using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    public GameObject UIJoystick;
    public GameObject circle;
    public List<touchLocation> touches = new List<touchLocation>();

    private Vector3 OriginalTransform;
    private Vector3 direction;
    [SerializeField] private float maxAmplitude;

    private void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Touch t = Input.GetTouch(i);
            if ((GetTouchPosition(t.position)).x < 0)
            {
                if (t.phase == TouchPhase.Began)
                {
                    Debug.Log("joystick began");
                    OriginalTransform = mousePos;
                    touches.Add(new touchLocation(t.fingerId, CreateJoystick(t)));
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    Debug.Log("joysticks ended");
                    touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                    Destroy(thisTouch.circle);
                    touches.RemoveAt(touches.IndexOf(thisTouch));
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    Vector3 persistentMousePos = new Vector2(0, 0);
                    float closestDistanceSqr = Mathf.Infinity;
                    if (Input.touchCount >= 2)
                    {
                        foreach (var item in touches)
                        {
                            Vector2 directionToTarget = item.circle.transform.position - OriginalTransform;
                            float dSqrToTarget = directionToTarget.sqrMagnitude;
                            if (dSqrToTarget < closestDistanceSqr)
                            {
                                closestDistanceSqr = dSqrToTarget;
                                persistentMousePos = item.circle.transform.position;
                            }
                        }
                    }
                    Debug.Log("Joystick moved");
                    direction = persistentMousePos - OriginalTransform;
                    if (direction.magnitude > maxAmplitude)
                    {
                        direction = direction.normalized * maxAmplitude;
                    }
                    touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                    thisTouch.circle.transform.position = direction + OriginalTransform;
                    Debug.DrawRay(OriginalTransform, direction, Color.red);
                }
            }
            else
            {
                if (t.phase == TouchPhase.Began)
                {
                    Debug.Log("touch began");
                    touches.Add(new touchLocation(t.fingerId, CreateCircle(t)));
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    Debug.Log("touch ended");
                    touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                    Destroy(thisTouch.circle);
                    touches.RemoveAt(touches.IndexOf(thisTouch));
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    Debug.Log("touch moved");
                }
            }
            i++;
        }
    }
    private GameObject CreateCircle(Touch t)
    {
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = GetTouchPosition(t.position);
        return c;
    }

    private GameObject CreateJoystick(Touch t)
    {
        GameObject c = Instantiate(UIJoystick) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = GetTouchPosition(t.position);
        return c;
    }

    private Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));
    }
}
