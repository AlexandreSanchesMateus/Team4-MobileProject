using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
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
        if (On == true)
        {
            if (GyroManager.Instance._portrait && PortraitMode._selectedItem == null)
            {
                tuto.SetActive(false);
                Time.timeScale = 1f;
            }
        }
      
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
}
