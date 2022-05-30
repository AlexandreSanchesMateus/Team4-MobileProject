using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    public GameObject seconndpart;
    private void OnMouseDown()
    {
        Debug.Log("Cuted");
        seconndpart.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
