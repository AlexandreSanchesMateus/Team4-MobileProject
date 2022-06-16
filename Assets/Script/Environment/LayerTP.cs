using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTP : MonoBehaviour
{
    private Collider2D m_collider;
    [SerializeField] private int nextLayerID = 0;
    [SerializeField] private Transform tpPosition;

    private void Start()
    {
        m_collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LayerManager2.Instance.TransitionScreen(tpPosition.position, nextLayerID);
        }
    }
}
