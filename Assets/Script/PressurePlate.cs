using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public bool isPressed;
    public float speed;

    private bool isRock = false;

    [SerializeField] private GameObject barrier;
    [SerializeField] private TP portail = null;
    [SerializeField] private DraggableObj draggableObj = null;

    private void Start()
    {
        if (portail)
            portail.gameObject.GetComponent<Collider2D>().enabled = false;

        if (draggableObj)
            draggableObj.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Draggable"))
        {
            isPressed = true;

            if (portail)
            {
                portail.gameObject.GetComponent<Collider2D>().enabled = true;
                barrier.gameObject.SetActive(false);
            }

            if (draggableObj)
                draggableObj.enabled = true;

           
            /*if (transform.position.y < 0.39)
            {
                gameObject.transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (transform.position.y) - 0.3f), 0.1f);
            }

            Debug.Log(transform.position.y);*/
        }else if (collision.transform.gameObject.CompareTag("Player"))
        {
            isPressed = true;

            if (portail)
            {
                portail.gameObject.GetComponent<Collider2D>().enabled = true;
                barrier.gameObject.SetActive(false);
            }

            if (draggableObj)
                draggableObj.enabled = true;

            isRock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Draggable") || !isRock)
        {
            isPressed = false;

            if (portail)
            {
                portail.gameObject.GetComponent<Collider2D>().enabled = false;
                barrier.gameObject.SetActive(true);
            }


            if (draggableObj)
                draggableObj.enabled = false;
        }else if (collision.gameObject.CompareTag("Draggable") && isRock)
        {
            isPressed = false;

            if (portail)
            {
                portail.gameObject.GetComponent<Collider2D>().enabled = false;
                barrier.gameObject.SetActive(true);
            }


            if (draggableObj)
                draggableObj.enabled = false;

            isRock = false;
        }
    }
}
