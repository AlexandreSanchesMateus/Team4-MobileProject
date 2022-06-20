using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private List<GameObject> iceToMelt = new List<GameObject>();

    private Animator animator;
    private AudioSource fireStart;
    [SerializeField] private AudioSource fire;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        fireStart = GetComponent<AudioSource>();
    }

    public void LightTorch()
    {
        animator.SetBool("LightTorch", true);
        fireStart.Play();
        StartCoroutine("MeltIce");
    }


    IEnumerator MeltIce()
    {
        yield return new WaitForSeconds(1.5f);
        fire.Play();
        foreach (GameObject ice in iceToMelt)
            ice.GetComponent<Animator>().SetBool("isMelting", true);

        yield return new WaitForSeconds(0.7f);
        
        foreach (GameObject ice in iceToMelt)
            Destroy(ice.gameObject);
    }
}
