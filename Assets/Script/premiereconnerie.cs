using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class premiereconnerie : MonoBehaviour
{
    public GameObject UIJoystick;
    private Vector2 OriginalTransform;
    private Vector2 direction;
    [SerializeField] private float maxAmplitude;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OriginalTransform = mousePos;

        }

        if (Input.GetKey(KeyCode.Mouse0))
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

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            UIJoystick.SetActive(false);
        }
    }
}
