using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GyroManager : MonoBehaviour
{
    private Gyroscope _gyroscope;
    private bool isGyroEnable = false;
    private bool movementMode;
    private DeviceOrientation _currentScreenOrientation = DeviceOrientation.Unknown;

    private CinemachineVirtualCamera m_vitualCamera;

    private float curveTime;

    private Vector3 lastPosition;
    private bool isMovementEnable;

    [Header("General Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private AnimationCurve _rotationCurve;

    [Header("Movement Mode Settings")]
    [SerializeField] private GameObject _targerCam;
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private float _movementSize;
    [SerializeField] private float _movementRotation;

    [Header("Action Mode Settings")]
    [SerializeField] private float _actionSize;
    [SerializeField] private float _actionRotation;
    

    private void Start()
    {
        m_vitualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        isGyroEnable = EnableGyro();
    }

    private void Update()
    {
        if (isGyroEnable)
        {
            // Changement de mode : Portrait ou Paysage
            if (_currentScreenOrientation != Input.deviceOrientation)
            {
                curveTime += Time.deltaTime;

                switch (Input.deviceOrientation)
                {
                    case DeviceOrientation.Portrait:
                        OrientScreen(_actionSize, _actionRotation);
                        movementMode = true;
                        break;

                    case DeviceOrientation.LandscapeLeft:
                        OrientScreen(_movementSize, _movementRotation);
                        movementMode = false;
                        break;
                }
            }

            if(movementMode)
            {
                InputHandle();

                if (isMovementEnable)
                {
                    Vector3 movement = lastPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    movement.z = 0;
                    _targerCam.transform.position = _targerCam.transform.position + movement * sensitivity;

                    lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                _targerCam.transform.position = player.transform.position;
            }
        }
    }

    private void InputHandle()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMovementEnable = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isMovementEnable = false;
        }
    }

    private void OrientScreen(float endSize, float endRotation)
    {
        m_vitualCamera.m_Lens.OrthographicSize = Mathf.Lerp(m_vitualCamera.m_Lens.OrthographicSize, endSize, _sizeCurve.Evaluate(curveTime));
        m_vitualCamera.transform.rotation = Quaternion.Lerp(m_vitualCamera.transform.rotation, Quaternion.Euler(0, 0, endRotation), _sizeCurve.Evaluate(curveTime));

        if (m_vitualCamera.m_Lens.OrthographicSize == endSize && m_vitualCamera.transform.rotation == Quaternion.Euler(0, 0, endRotation))
        {
            _currentScreenOrientation = Input.deviceOrientation;
            curveTime = 0;
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("Gyroscope Enable");
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;

            return true;
        }
        Debug.LogWarning("Gyroscope not supported by the platform");
        return false;
    }
}
