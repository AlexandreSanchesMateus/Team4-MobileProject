using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObj : MonoBehaviour
{
    public string nom;

    // Class
    public UnlockedObj unlockedObj;

    // GameObject
    public GameObject goodPos;

    // Vector3
    private Vector3 draggOffset;
    private Vector3 initialPos;

    // Booleans
    public bool goodPosition;
    private bool movable = true;

    public void Start()
    {
        initialPos = transform.position;
    }

    public void Update()
    {
        
    }
    public void OnMouseDown()
    {
        draggOffset = transform.position - GetMousePos();
    }

            // Drop de L'objet
    public void OnMouseUp()
    {
        if(goodPosition == false)
        {
            transform.position = initialPos;
        }
        else
        {
            transform.position = goodPos.transform.position;
            movable = false;
            //unlockedObj.open = true;
            Debug.Log("Bien place");
            
        }
    }

            // Drag de L'objet
    public void OnMouseDrag()
    {    
        if(movable == true)
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + draggOffset, 10);        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nom))
        {           
            goodPosition = true;
            goodPos = collision.gameObject;
            
        }
    }
    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
