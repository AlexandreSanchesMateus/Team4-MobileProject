using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortraitMode : MonoBehaviour
{
    public bool portraitModeEnable = false;

    private Vector2 _lastPosition;
    private CinemachineVirtualCamera vitualCamera;

    [SerializeField] private GameObject _targerCam;
    [SerializeField] private float _sensitivity = 2;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minFocus;
    [SerializeField] private float _maxFocus;



    private void Start()
    {
        vitualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (portraitModeEnable)
        {
            // Focus Camera
            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                float touchesPrePosDifference = ((firstTouch.position - firstTouch.deltaPosition) - (secondTouch.position - secondTouch.deltaPosition)).magnitude;
                float touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                float zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * _zoomSpeed * Time.deltaTime;

                if (touchesPrePosDifference > touchesCurPosDifference)
                    vitualCamera.m_Lens.OrthographicSize += zoomModifier;
                if (touchesPrePosDifference < touchesCurPosDifference)
                    vitualCamera.m_Lens.OrthographicSize -= zoomModifier;

                vitualCamera.m_Lens.OrthographicSize = Mathf.Clamp(vitualCamera.m_Lens.OrthographicSize, _minFocus, _maxFocus);

                // Mouvement Camera pendant zoom
                Vector2 middlePos = (Camera.main.ScreenToWorldPoint(firstTouch.position) + Camera.main.ScreenToWorldPoint(secondTouch.position)) / 2;
                if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
                    _lastPosition = middlePos;

                CameraMovement(middlePos);

                if (firstTouch.phase == TouchPhase.Ended)
                    _lastPosition = Camera.main.ScreenToWorldPoint(secondTouch.position);
                else if (secondTouch.phase == TouchPhase.Ended)
                    _lastPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
            }
            else if (Input.touchCount == 1)
            {
                Touch _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began)
                {
                    if (Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(_touch.position), 0.2f).gameObject.CompareTag("Interactible"))
                    {
                        // Drag and drop
                    }
                    else
                        _lastPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                }
                else if (_touch.phase == TouchPhase.Moved)
                {
                    CameraMovement(Camera.main.ScreenToWorldPoint(_touch.position));
                }
            }
        }
        else
        {
            _targerCam.transform.position = PlayerMovement2.Instance.gameObject.transform.position;
        }
    }

    private void CameraMovement(Vector2 position)
    {
        Vector3 movement = _lastPosition - position;
        movement.z = 0;
        _targerCam.transform.position = _targerCam.transform.position + movement * _sensitivity;
        _lastPosition = position;
    }
}
