using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeToPC : Interactable
{
    public GameObject ladder;

    public void Awake()
    {
        ladder.SetActive(false);
    }

    public override void Interact()
    {
        ladder.SetActive(true);
        FindObjectOfType<GameManager>().MyLoadScene("Antoine");
        
    }
}
