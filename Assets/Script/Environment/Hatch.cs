using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    private float time = 0;
    private Vector3 originalePosition;
    [SerializeField] private PressurePlate plate;
    [SerializeField] private float movement;
    [SerializeField] private float speed;

    void Start()
    {
        originalePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (plate.isPressed)
        {
            time += Time.deltaTime * speed;
        }
        else
        {
            time -= Time.deltaTime * speed;
        }

        time = Mathf.Clamp(time, 0, 1);

        if(time != 0 || time != 1)
        {
            transform.position = Vector3.Lerp(originalePosition, new Vector3(originalePosition.x - movement, originalePosition.y, originalePosition.z), time);
        }
    }
}
