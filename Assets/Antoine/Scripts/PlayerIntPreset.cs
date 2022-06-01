using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntPreset : MonoBehaviour
{
    public GameObject interactableIcon;

    private Vector2 boxSize = new Vector2(0.1f, 1f);

    // Booleans

    public bool inLayer1;
    public bool inLayer2;
    public bool inLayer3;
    public bool inLayer4;
    public bool inLayer5;
    public bool inLayer6;

    private void Start()
    {
        inLayer1 = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();

        if (inLayer1 == true)
        {
            Physics2D.IgnoreLayerCollision(6, 7);
            gameObject.layer = 6;
        }
            

        if (inLayer2 == true)
        {
            Physics2D.IgnoreLayerCollision(7, 6);
            gameObject.layer = 7;
        }
            
    }

    public void OpenInteractableIcon()
    {
        interactableIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactableIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
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
