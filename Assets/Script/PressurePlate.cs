using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;
    [SerializeField] private LayerTP portail = null;

    private void Start()
    {
        if (portail)
            portail.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Rock") || collision.transform.gameObject.CompareTag("Player"))
        {
            isPressed = true;

            if (portail)
                portail.gameObject.GetComponent<Collider2D>().enabled = true;
            /*if (transform.position.y < 0.39)
            {
                gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) - 0.3f), 0.1f);
            }

            Debug.Log(transform.position.y);*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Rock") || collision.transform.gameObject.CompareTag("Player"))
        {
            isPressed = false;

            if (portail)
                portail.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
