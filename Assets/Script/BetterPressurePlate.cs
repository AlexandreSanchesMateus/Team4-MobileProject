using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPressurePlate : MonoBehaviour
{
    private Vector2 topPos;
    private Vector2 bottomPos;

    private bool isRock = false;
    public Laddertest ladder;

    private void Start()
    {
        topPos = new Vector2(transform.position.x, transform.position.y);
        bottomPos = new Vector2(transform.position.x, transform.position.y - 0.15f);
        OnDrawGizmos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = bottomPos;
            ladder.activated = true;
        }else if (collision.gameObject.CompareTag("Draggable"))
        {
            transform.position = bottomPos;
            ladder.activated = true;
            isRock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isRock)
        {
            transform.position = topPos;
            ladder.activated = false;
        }else if (collision.gameObject.CompareTag("Draggable") && isRock)
        {
            transform.position = topPos;
            ladder.activated = false;
            isRock = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(topPos, bottomPos);
    }
}
