using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    public GameObject UIJoystick;
    public GameObject circle;
    public List<touchLocation> touches = new List<touchLocation>();

    private Vector2 OriginalTransform;
    private Vector2 direction;
    private Vector2 vectorZero = new Vector2(0,0);
    [SerializeField] private float maxAmplitude;
    private bool isJoystick = false;

    private void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");
                if ((GetTouchPosition(t.position)).x > 0)
                {
                    OriginalTransform = mousePos;
                    isJoystick = true;
                }
                else
                {
                    touches.Add(new touchLocation(t.fingerId, CreateCircle(t)));
                }
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
                if (isJoystick)
                {
                    direction = mousePos - OriginalTransform;
                    if (direction.magnitude > maxAmplitude)
                    {
                        direction = direction.normalized * maxAmplitude;
                    }
                    UIJoystick.gameObject.SetActive(true);
                    UIJoystick.transform.position = direction + OriginalTransform;
                    Debug.DrawRay(OriginalTransform, direction, Color.red);
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

    private Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));
    }
}
