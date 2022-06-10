using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GyroManager : MonoBehaviour
{
    public static GyroManager Instance { get; set; }

    public bool isGyroEnable = false;
    public bool _portrait;
    private bool _last;
    private bool isReorienting;

    private PortraitMode _portraitMode;
    private CinemachineVirtualCamera vitualCamera;
    private float curveTime;

    [Header("General Settings")]
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private AnimationCurve _rotationCurve;

    [Header("Portrait Mode Settings")]
    [SerializeField] private float _portraitSize;
    [SerializeField] private float _portraitRotation;

    [Header("Landscape Mode Settings")]
    [SerializeField] private float _landscapeSize;
    [SerializeField] private float _landscapeRotation;



    private void Start()
    {
        Instance = this;

        vitualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        _portraitMode = gameObject.GetComponent<PortraitMode>();
        isGyroEnable = EnableGyro();

        if(Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            // TEST
            _portrait = true;
            _last = true;
            PlayerMovement2.Instance.playerMovementEnable = false;
            _portraitMode.portraitModeEnable = true;
            ItemManager.Instance.EnableItemManager(true);

            vitualCamera.m_Lens.OrthographicSize = _portraitSize;
            vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _portraitRotation);;
           
        }
        else
        {
            //TEST
            _portrait = false;
            _last = false;
            PlayerMovement2.Instance.playerMovementEnable = true;
            _portraitMode.portraitModeEnable = false;
            ItemManager.Instance.EnableItemManager(false);

            vitualCamera.m_Lens.OrthographicSize = _landscapeSize;
            vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _landscapeRotation);
        }
    }

    private void Update()
    {
        if (isGyroEnable)
        {
            //       <<<<<<<<<<<<<<<<<<<<<<<<<<<     Final Orientation Manager     >>>>>>>>>>>>>>>>>>>>>>>>>>>        // 


            if (Input.deviceOrientation == DeviceOrientation.Portrait)
                 _portrait = true;  
             else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
                 _portrait = false;

            /*if (Input.GetKeyDown(KeyCode.A))
            {
                _portrait = !_portrait;
                Debug.Log("PRESSED");
            }*/

            if(_last != _portrait)
            {
                isReorienting = true;
                OrientScreen(false);
            }
            else if (isReorienting)
                OrientScreen(true);
        }
    }
    
    private void OrientScreen(bool isRevers)
    {
        int finalValue = 1;
        if (isRevers)
        {
            curveTime -= Time.deltaTime;
            finalValue = 0;
        }
        else
            curveTime += Time.deltaTime;


        if (!_last)
        {
            vitualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_landscapeSize, _portraitSize, _sizeCurve.Evaluate(curveTime));
            vitualCamera.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, _landscapeRotation), Quaternion.Euler(0, 0, _portraitRotation), _sizeCurve.Evaluate(curveTime));
        }
        else
        {
            ItemManager.Instance.EnableItemManager(false);
            vitualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_portraitSize, _landscapeSize, _sizeCurve.Evaluate(curveTime));
            vitualCamera.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, _portraitRotation), Quaternion.Euler(0, 0, _landscapeRotation), _sizeCurve.Evaluate(curveTime));
        }

        
        if (_sizeCurve.Evaluate(curveTime) == finalValue && _sizeCurve.Evaluate(curveTime) == finalValue)
        {
            _last = _portrait;
            isReorienting = false;
            curveTime = 0;

            if (_portrait)
            {
                vitualCamera.m_Lens.OrthographicSize = _portraitSize;
                vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _portraitRotation);

                PlayerMovement2.Instance.DisableTouche();
                PlayerMovement2.Instance.playerMovementEnable = false;
                _portraitMode.portraitModeEnable = true;

                ItemManager.Instance.EnableItemManager(true);
            }
            else
            {
                vitualCamera.m_Lens.OrthographicSize = _landscapeSize;
                vitualCamera.transform.rotation = Quaternion.Euler(0, 0, _landscapeRotation);

                _portraitMode.portraitModeEnable = false;
                PlayerMovement2.Instance.playerMovementEnable = true;
            }
        }
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
