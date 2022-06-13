using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Rock") || collision.transform.gameObject.CompareTag("Player"))
        {
            isPressed = true;
            /*if (transform.position.y < 0.39)
            {
                gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) - 0.3f), 0.1f);
            }

            Debug.Log(transform.position.y);*/
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Rock") || collision.transform.gameObject.CompareTag("Player"))
        {
            isPressed = false;
        }
    }
}
