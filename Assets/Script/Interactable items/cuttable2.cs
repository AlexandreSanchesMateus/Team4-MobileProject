using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttable2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject oldTree;
    public GameObject logToSpawn;

    public GameObject departure;
    public GameObject arrival;

    private GameObject spawnedLog;
    private bool isMoving = false;
    private float elapsedTime;
    private float duration = 4f;

    [SerializeField] private Animator lanimTroNazeMdr;

    private bool shouldSpawn = true;

    private void Start()
    {
        
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageCompleted = (elapsedTime / duration);

        
    }

    public void CutRope()
    {
        if (shouldSpawn)
        {
            lanimTroNazeMdr.SetBool("Cassage", true);
        }
    }

    private void AddBullshit()
    {
        spawnedLog.AddComponent<PolygonCollider2D>();
    }

    private IEnumerator AddWeight()
    {
        yield return new WaitForSeconds(6);
        
    }
}
