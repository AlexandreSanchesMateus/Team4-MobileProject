using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPeinture : MonoBehaviour
{
    public GameObject tuto;
    private bool On;
    private bool Active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.tag == "Player") && (Active == true))
        {
            tuto.SetActive(true);
            Time.timeScale = 0f;
            On = true;
            Active = false;
        }
    }

    public void Anule()
    {
        if (On == true)
        {
                tuto.SetActive(false);
                Time.timeScale = 1f;
        }
    }
}
