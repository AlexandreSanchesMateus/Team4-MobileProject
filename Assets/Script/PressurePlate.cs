using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Vector2 boxSize = new Vector2(0.1f, 1f);


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (/*rc.transform.gameObject.CompareTag("rock") ||*/ collision.transform.gameObject.CompareTag("Player"))
        {
            if (transform.position.y < 0.39)
            {
                gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) - 0.3f), 0.1f);
                Debug.Log("pressed");
            }
            Debug.Log(transform.position.y);
        }
    }
}
