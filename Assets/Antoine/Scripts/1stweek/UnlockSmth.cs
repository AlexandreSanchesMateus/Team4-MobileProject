using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSmth : MonoBehaviour
{
    // Classes 
    public UnlockedObj unlockedObj;

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        unlockedObj.open = true;
        Debug.Log("L'église va désormais répondre");
    }
}
