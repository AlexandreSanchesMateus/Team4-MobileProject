using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    public GameObject UIJoystickOuterCircle;
    public GameObject UIJoystick;

    private Vector2 OriginalTransform;
    private int leftTouch = 99;

    private void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = GetTouchPosition(t.position);
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    Interact();
                }
                else
                {
                    leftTouch = t.fingerId;
                    OriginalTransform = touchPos;
                    UIJoystickOuterCircle.transform.position = touchPos;
                    UIJoystick.transform.position = UIJoystickOuterCircle.transform.position;
                    UIJoystick.SetActive(true);
                    UIJoystickOuterCircle.SetActive(true);
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - OriginalTransform;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1);
                MoveCharacter(direction);

                UIJoystick.transform.position = new Vector2(UIJoystickOuterCircle.transform.position.x + direction.x, UIJoystickOuterCircle.transform.position.y + direction.y);
            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                UIJoystickOuterCircle.SetActive(false);
                UIJoystick.SetActive(false);
            }
            i++;
        }

    }

    private void MoveCharacter(Vector2 direction)
    {
        gameObject.transform.Translate(direction * 2 * Time.deltaTime);
    }

    private void Interact()
    {
        Debug.Log("interacted");
    }

    private Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
        
    }
}
