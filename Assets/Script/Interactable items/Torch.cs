using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private List<GameObject> iceToMelt = new List<GameObject>();
    public int timeToMelt;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void LightTorch()
    {
        animator.SetBool("LightTorch", true);
        StartMelt();
        StartCoroutine("MeltIce");
    }

    private void StartMelt()
    {
        foreach (GameObject ice in iceToMelt)
            ice.GetComponent<Animator>().SetBool("isMelting", true);
    }

    IEnumerator MeltIce()
    {
        yield return new WaitForSeconds(timeToMelt);

        foreach (GameObject ice in iceToMelt)
            Destroy(ice.gameObject);
    }
}
