using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamZoom : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private bool isScreenFocus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isScreenFocus = !isScreenFocus;
            _anim.SetTrigger("change");
        }
    }
}
