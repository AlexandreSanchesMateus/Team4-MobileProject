using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GyroManager : MonoBehaviour
{
    public static GyroManager Instance { get; set; }

    private bool isGyroEnable = false;
    private bool portraitMode = false;
    private DeviceOrientation _currentScreenOrientation;
    private DeviceOrientation _lastScreenOrientation = DeviceOrientation.Unknown;

    private CinemachineVirtualCamera vitualCamera;

    private float curveTime;
    private Vector3 _lastPosition;

    [Header("General Settings")]
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private AnimationCurve _rotationCurve;

    [Header("Movement Mode Settings")]
    [SerializeField] private GameObject _targerCam;
    [SerializeField] private float _sensitivity = 2;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minFocus;
    [SerializeField] private float _maxFocus;
    [SerializeField] private float _movementSize;
    [SerializeField] private float _movementRotation;

    [Header("Action Mode Settings")]
    [SerializeField] private float _actionSize;
    [SerializeField] private float _actionRotation;

    [HideInInspector] public float _currentSize;
    

    private void Start()
    {
        Instance = this;

        vitualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        isGyroEnable = EnableGyro();

        if(Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            vitualCamera.m_Lens.OrthographicSize = _actionSize;
            vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _actionRotation);
            _currentScreenOrientation = DeviceOrientation.Portrait;
            portraitMode = true;
           
        }
        else
        {
            vitualCamera.m_Lens.OrthographicSize = _movementSize;
            vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _movementRotation);
            _currentScreenOrientation = DeviceOrientation.LandscapeLeft;
        }
    }

    private void Update()
    {
        if (isGyroEnable)
        {
            if(portraitMode)
            {
                // Focus Camera
                if (Input.touchCount == 2)
                {
                    Touch firstTouch = Input.GetTouch(0);
                    Touch secondTouch = Input.GetTouch(1);

                    float touchesPrePosDifference = ((firstTouch.position - firstTouch.deltaPosition) - (secondTouch.position - secondTouch.deltaPosition)).magnitude;
                    float touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                    float zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * _zoomSpeed * Time.deltaTime;

                    if(touchesPrePosDifference > touchesCurPosDifference)
                        vitualCamera.m_Lens.OrthographicSize += zoomModifier;
                    if (touchesPrePosDifference < touchesCurPosDifference)
                        vitualCamera.m_Lens.OrthographicSize -= zoomModifier;

                    vitualCamera.m_Lens.OrthographicSize = Mathf.Clamp(vitualCamera.m_Lens.OrthographicSize, _minFocus, _maxFocus);
                    // _currentSize = vitualCamera.m_Lens.OrthographicSize;

                    // Mouvement Camera pendant zoom
                    Vector2 middlePos = (Camera.main.ScreenToWorldPoint(firstTouch.position) + Camera.main.ScreenToWorldPoint(secondTouch.position)) / 2;
                    if(firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
                        _lastPosition = middlePos;
                    
                    CameraMovement(middlePos);

                    if (firstTouch.phase == TouchPhase.Ended)
                        _lastPosition = Camera.main.ScreenToWorldPoint(secondTouch.position);
                    else if(secondTouch.phase == TouchPhase.Ended)
                        _lastPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);

                // Mouvement Camera
                }else if(Input.touchCount == 1)
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



            // Changement de mode : Portrait ou Paysage
            if (_currentScreenOrientation != Input.deviceOrientation)
            {
                switch (Input.deviceOrientation)
                {
                    case DeviceOrientation.Portrait:
                        _lastScreenOrientation = DeviceOrientation.Portrait;
                        OrientScreen(_movementSize, _movementRotation, _actionSize, _actionRotation, DeviceOrientation.Portrait, false);
                        portraitMode = true;
                        break;

                    case DeviceOrientation.LandscapeLeft:
                        _lastScreenOrientation = DeviceOrientation.LandscapeLeft;
                        OrientScreen(_actionSize, _actionRotation, _movementSize, _movementRotation, DeviceOrientation.LandscapeLeft, false);
                        portraitMode = false;
                        break;

                    default:
                        if (_lastScreenOrientation == DeviceOrientation.Portrait)
                        {
                            OrientScreen(_movementSize, _movementRotation, _actionSize, _actionRotation, DeviceOrientation.Portrait, false);
                            Debug.Log("Boo");
                            portraitMode = true;
                        }
                        else if (_lastScreenOrientation == DeviceOrientation.LandscapeLeft)
                        {
                            OrientScreen(_actionSize, _actionRotation, _movementSize, _movementRotation, DeviceOrientation.LandscapeLeft, false);
                            portraitMode = false;
                        }
                        break;
                }
            }
            else if (_lastScreenOrientation != DeviceOrientation.Unknown)
            {
                if (_lastScreenOrientation == DeviceOrientation.Portrait)
                {
                    OrientScreen(_movementSize, _movementRotation, _actionSize, _actionRotation, DeviceOrientation.LandscapeLeft, true);
                    portraitMode = true;
                }
                else
                {
                    OrientScreen(_actionSize, _actionRotation, _movementSize, _movementRotation, DeviceOrientation.Portrait, true);
                    portraitMode = false;
                }
            }
            else
                return;

            PlayerMovement2.Instance.playerMovementEnable = !portraitMode;

            if (_currentScreenOrientation == DeviceOrientation.Portrait)
                ItemManager.Instance.EnableItemManager();
            else
                ItemManager.Instance.DisableItemManager();

        }
    }
    
    private void OrientScreen(float startSize, float startRotation, float endSize, float endRotation, DeviceOrientation orientation, bool isRevers)
    {
        int finalValue = 1;
        if (isRevers)
        {
            curveTime -= Time.deltaTime;
            finalValue = 0;
        }
        else
            curveTime += Time.deltaTime;

        vitualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, _sizeCurve.Evaluate(curveTime));
        vitualCamera.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, startRotation), Quaternion.Euler(0, 0, endRotation), _sizeCurve.Evaluate(curveTime));

        if (_sizeCurve.Evaluate(curveTime) == finalValue && _sizeCurve.Evaluate(curveTime) == finalValue)
        {
            _currentScreenOrientation = orientation;
            _lastScreenOrientation = DeviceOrientation.Unknown;
            curveTime = 0;

            if (!isRevers)
            {
                vitualCamera.m_Lens.OrthographicSize = endSize;
                vitualCamera.transform.rotation = Quaternion.Euler(0, 0, endRotation);
            }
            else
            {
                vitualCamera.m_Lens.OrthographicSize = startSize;
                vitualCamera.transform.rotation = Quaternion.Euler(0, 0, startRotation);
            }
        }
    }

    private void CameraMovement(Vector3 position)
    {
        Vector3 movement = _lastPosition - position;
        movement.z = 0;
        _targerCam.transform.position = _targerCam.transform.position + movement * _sensitivity;
        _lastPosition = position;
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("Gyroscope Enable");
            Input.gyro.enabled = true;

            return true;
        }
        Debug.LogWarning("Gyroscope not supported by the platform");
        return false;
    }
}
