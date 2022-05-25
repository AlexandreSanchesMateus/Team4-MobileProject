using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    private Gyroscope _gyroscope;
    private bool isGyroEnable = false;
    private ScreenOrientation _screenOrientation = ScreenOrientation.PORTRAIT;
    private bool needToBeOriented = false;
    
    [Header("Debug Settings")]
    [SerializeField] private GameObject rep;

    [Header("Orientation settings")]
    [SerializeField] [Range(0, 45)] private float minDegToPortrait;
    [SerializeField] [Range(0, 45)] private float minDegToLandscape;

    [Header("Camera Anim Settings")]
    [SerializeField] private float _timeAfterRotation;
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private float _sizeTime;
    [SerializeField] private AnimationCurve _rotationCurve;
    [SerializeField] private float _rotationTime;
    

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("Gyroscope Enable");

            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            isGyroEnable = true;
        }
        else
            Debug.LogWarning("Gyroscope not supported by the platform");
    }

    private void Update()
    {
        if (isGyroEnable)
        {
            float zRotationPhone = (_gyroscope.attitude.eulerAngles.z + 90) % 360;

            // DEBUG
            rep.transform.rotation = Quaternion.Euler(new Vector3(0,0, -zRotationPhone));
            // Debug.Log(zRotationPhone);

            if (_screenOrientation == ScreenOrientation.PORTRAIT)
            {
                if (zRotationPhone < 90 + minDegToLandscape)
                {
                    _screenOrientation = ScreenOrientation.LANDSCAPE_RIGHT;
                    needToBeOriented = true;
                }
                else if (zRotationPhone > 270 - minDegToLandscape)
                {
                    _screenOrientation = ScreenOrientation.LANDSCAPE_LEFT;
                    needToBeOriented = true;
                }
            }
            else
            {
                if (zRotationPhone < 180 + minDegToPortrait && zRotationPhone > 180 - minDegToPortrait)
                {
                    _screenOrientation = ScreenOrientation.PORTRAIT;
                    needToBeOriented = true;
                }
                else if(_screenOrientation == ScreenOrientation.LANDSCAPE_LEFT && zRotationPhone < 180)
                {
                    _screenOrientation = ScreenOrientation.LANDSCAPE_RIGHT;
                    needToBeOriented = true;
                }
                else if (_screenOrientation == ScreenOrientation.LANDSCAPE_RIGHT && zRotationPhone > 180)
                {
                    _screenOrientation = ScreenOrientation.LANDSCAPE_LEFT;
                    needToBeOriented = true;
                }
            }

            if(needToBeOriented)
                ChangeOrientation();
        }
    }

    private void ChangeOrientation()
    {
        Debug.Log("Change orientation for " + _screenOrientation.ToString());
        needToBeOriented = false;
    }

    private enum ScreenOrientation
    {
        PORTRAIT,
        LANDSCAPE_RIGHT,
        LANDSCAPE_LEFT
    }
}
