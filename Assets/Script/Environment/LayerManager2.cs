using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager2 : MonoBehaviour
{
    public static LayerManager2 Instance { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private float time;

    private void Start()
    {
        Instance = this;
    }

    public void TransitionScreen(Vector2 newPosition)
    {
        StartCoroutine(Fade(newPosition));
    }

    private IEnumerator Fade(Vector2 position)
    {
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(time);
        PlayerMovement2.Instance.gameObject.transform.position = position;
        animator.SetBool("Active", false);
    }
}
