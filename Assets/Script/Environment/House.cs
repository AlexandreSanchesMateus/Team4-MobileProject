using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Interactable
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject plaque;
    [SerializeField] private Sprite sprite;

    public override void Interact()
    {
        GameObject a = Instantiate(prefab);
        a.transform.position = spawnPos.position;
        a.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        a.GetComponent<DraggableObj>().goodPos = plaque;
        a.GetComponent<DraggableObj>().nom = "GoodPosition2";
        a.GetComponent<DraggableObj>().player = PlayerMovement2.Instance;
        a.GetComponent<SpriteRenderer>().sprite = sprite;
        a.GetComponent<SpriteRenderer>().sortingLayerID = 0;
        a.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
