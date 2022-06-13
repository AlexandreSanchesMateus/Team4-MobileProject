using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool canDescend = true;
    private bool canAscend = false;
    private bool hasReachedBottom = false;
    private bool hasReachedTop = false;

    private void Update()
    {
        if (transform.position.y > 0.59)
        {
            hasReachedTop = true;
            canAscend = false;
            canDescend = true;
            gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) + 0.3f), 0.1f);
        }
        else if (transform.position.y < 0.33)
        {
            hasReachedBottom = true;
            canDescend = false;
            canAscend = true;
        }
    }

    private void Ascend()
    {
        gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) + 0.26f), 0.1f);
    }

    private void Descend()
    {
        gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) - 0.26f), 0.1f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (/*rc.transform.gameObject.CompareTag("rock") ||*/ collision.transform.gameObject.CompareTag("Player"))
        {
            if (canDescend)
            {
                Descend();
                canDescend = false;
                StartCoroutine("DescendTimer");
                Debug.Log("pressed");
                Debug.Log(transform.position.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAscend = true;
            Ascend();
        }
    }

    IEnumerator DescendTimer()
    {
        yield return new WaitForSeconds(0.13f);
        canDescend = true;
    }

    IEnumerator AscendTimer()
    {
        yield return new WaitForSeconds(0.13f);
        canAscend = true;
    }
}
