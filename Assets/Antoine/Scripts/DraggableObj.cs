using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObj : MonoBehaviour
{
    public string nom;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    // Class
    //public UnlockedObj unlockedObj;

    // GameObject
    public GameObject goodPos;
    public PlayerMovement2 player;

    // Vector3
    private Vector3 draggOffset;
    private Vector3 initialPos;

    // Booleans
    public bool goodPosition;
    private bool movable = true;
    [SerializeField] private bool cailloux;
  

    private AudioSource chting;

    private void Awake()
    {
        chting = gameObject.GetComponent<AudioSource>();
    }
    public void Start()
    {
        initialPos = transform.position;
        
        
    }

    
    public void OnMouseDown()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {
            PortraitMode.canDoAnything = false;
            draggOffset = transform.position - GetMousePos();
        }
    }

            // Drop de L'objet
    public void OnMouseUp()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {
            if (goodPosition == false)
            {
               // transform.position = initialPos;
               
            }
            else
            {
                transform.position = goodPos.transform.position;
                //movable = false;
                chting.Play();
                CheckInteraction();
 
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CapsuleCollider2D>(), false);
                //unlockedObj.open = true;

            }

            

            PortraitMode.canDoAnything = true;
        }
    }

    // Drag de L'objet
    public void OnMouseDrag()
    {
        if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CapsuleCollider2D>());
            if (movable == true)
                transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + draggOffset, 10);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nom) )
        {           
            goodPosition = true;
                
            //goodPos = collision.gameObject;
            if (collision.gameObject.CompareTag("Maison"))
                collision.gameObject.tag = null;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(nom))
        {
            if (cailloux)
                goodPosition = false;
        }
    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
