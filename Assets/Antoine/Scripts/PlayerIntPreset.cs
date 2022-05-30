using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntPreset : MonoBehaviour
{
    public GameObject interactableIcon;

    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();

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
