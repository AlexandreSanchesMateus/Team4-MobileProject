using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private float maxAmplitude;

    private bool isMovementEnable = false;
    private Vector3 startPosition;
    
    void Update()
    {
        InputEndle();

        if (isMovementEnable)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.DrawLine(startPosition, mousePosition, Color.red);

            Vector3 direction = mousePosition - startPosition;

            if (direction.magnitude > maxAmplitude)
                direction = direction.normalized * maxAmplitude;

            Debug.DrawRay(startPosition, direction, Color.green);
        }
    }

    private void InputEndle()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("press");
            isMovementEnable = true;
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("release");
            isMovementEnable = false;
        }
    }
}
