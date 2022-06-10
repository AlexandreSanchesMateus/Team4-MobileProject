using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private List<GameObject> iceToMelt = new List<GameObject>();
    public int timeToMelt;

    private bool shouldLight = true;
    private bool shouldStartMelting = false;
    private float extinguishTimer = 0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void AskToLight()
    {
        if (shouldLight)
            TontonJohnny();
        else
            Pompiers();
    }

    private void TontonJohnny()
    {
        animator.SetBool("LightTorch", true);
        StartMelt();
        shouldLight = false;
    }

    private void Pompiers()
    {
        animator.SetBool("LightTorch", false);
        extinguishTimer = 0f;
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
