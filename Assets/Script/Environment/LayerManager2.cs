using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LayerManager2 : MonoBehaviour
{
    public static LayerManager2 Instance { get; private set; }

    [SerializeField] private CinemachineConfiner2D confiner2D;

    [SerializeField] private PolygonCollider2D[] cofineCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private float time;

    private void Start()
    {
        Instance = this;
        //confiner2D = GyroManager.Instance.gameObject.GetComponent<CinemachineConfiner2D>();
    }

    public void TransitionScreen(Vector2 newPosition, int layer)
    {
        PlayerMovement2.Instance.playerMovementEnable = false;
        GyroManager.Instance.isGyroEnable = false;
        StartCoroutine(Fade(newPosition, layer));
    }

    private IEnumerator Fade(Vector2 position, int layer)
    {
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(time);

        if (layer >= 0)
        {
            confiner2D.m_BoundingShape2D = cofineCollider[layer];
        }

        PlayerMovement2.Instance.gameObject.transform.position = position;
        PlayerMovement2.Instance.playerMovementEnable = true;
        GyroManager.Instance.isGyroEnable = true;
        animator.SetBool("Active", false);
    }
}
