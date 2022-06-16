using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LayerManager2 : MonoBehaviour
{
    public static LayerManager2 Instance { get; private set; }

    private CinemachineConfiner2D confiner2D;

    [SerializeField] private PolygonCollider2D[] cofineCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private float time;

    private void Start()
    {
        Instance = this;
        confiner2D = GyroManager.Instance.gameObject.GetComponent<CinemachineConfiner2D>();
    }

    public void TransitionScreen(Vector2 newPosition, int layer)
    {
        StartCoroutine(Fade(newPosition, layer));
    }

    private IEnumerator Fade(Vector2 position, int layer)
    {
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(time);
        confiner2D.m_BoundingShape2D = cofineCollider[layer];
        PlayerMovement2.Instance.gameObject.transform.position = position;
        animator.SetBool("Active", false);
    }
}
