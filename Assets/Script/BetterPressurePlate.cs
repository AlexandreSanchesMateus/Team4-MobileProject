using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPressurePlate : MonoBehaviour
{
    private Vector2 topPos;
    private Vector2 bottomPos;

    public Laddertest ladder;

    private void Start()
    {
        topPos = new Vector2(transform.position.x, transform.position.y);
        bottomPos = new Vector2(transform.position.x, transform.position.y - 0.15f);
        OnDrawGizmos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Draggable"))
        {
            transform.position = bottomPos;
            ladder.activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Draggable"))
        {
            transform.position = topPos;
            ladder.activated = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(topPos, bottomPos);
    }
}
