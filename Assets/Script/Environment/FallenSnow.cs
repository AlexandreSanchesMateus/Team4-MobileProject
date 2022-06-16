using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenSnow : MonoBehaviour
{
    private GameObject groundRef;
    private Sprite _sp;

    private void Start()
    {
        groundRef = transform.GetChild(0).gameObject;
        _sp = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            groundRef.GetComponent<SpriteRenderer>().sprite = _sp;
            groundRef.GetComponent<BoxCollider2D>().enabled = true;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
