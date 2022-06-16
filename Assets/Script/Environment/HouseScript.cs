using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprite;
    private float time;
    [SerializeField] private float speed;

    private bool isTransparent = false;

    public GameObject house;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("entered");
            isTransparent = true;
            house.GetComponent<Collider2D>().enabled = true;

            foreach (SpriteRenderer _sp in sprite)
                _sp.color = new Vector4(255, 255, 255, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            isTransparent = false;

            foreach (SpriteRenderer _sp in sprite)
                _sp.color = new Vector4(255, 255, 255, 255);
        }
    }
}
