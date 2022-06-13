using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPressurePlate : MonoBehaviour
{
    private Vector2 topPos;
    private Vector2 bottomPos;

    private bool isAtBottom = false;
    private bool goUp = false;
    private bool goDown = false;

    private void Start()
    {
        topPos = transform.position;
        bottomPos = new Vector2(transform.position.x, transform.position.y - 0.4f);
    }

    private void Update()
    {


        OnDrawGizmos();

        if (transform.position.x == topPos.x)
        {
            isAtBottom = false;
        }
        else if (transform.position.x == bottomPos.x)
        {
            isAtBottom = true;
            Debug.Log("isatbottom");
        }
    }

    private void GoUp()
    {
        while(goUp)
        {
            transform.position = Vector2.Lerp(bottomPos, topPos, 0.01f);
            Debug.Log("looping up");
            if (transform.position.x == topPos.x)
            {
                goUp = false;
            }
        }
    }

    private void GoDown()
    {
        while (goDown)
        {
            transform.position = Vector2.Lerp(topPos, bottomPos, 0.01f);
            Debug.Log("looping down");
            if (transform.position.x == bottomPos.x)
            {
                goDown = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered");
        if (!isAtBottom)
        {
            Debug.Log("going down");
            if (collision.gameObject.CompareTag("Player"))
            {
                goDown = true;
                GoDown();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        if (isAtBottom)
        {
            Debug.Log("going up");
            if (collision.gameObject.CompareTag("Player"))
            {
                goUp = true;
                GoUp();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(topPos, bottomPos);
        Gizmos.DrawWireSphere(bottomPos, 1);
    }
}
