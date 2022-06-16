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

    private bool shouldSpawn = true;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageCompleted = (elapsedTime / duration);

        if (isMoving)
        {
            spawnedLog.transform.position = Vector2.Lerp(departure.transform.position, arrival.transform.position, percentageCompleted);
        }
    }

    public void CutRope()
    {
        if (shouldSpawn)
        {
            spawnedLog = Instantiate(logToSpawn, oldTree.transform);
            isMoving = true;
            StartCoroutine("AddWeight");
            elapsedTime = 0;
            shouldSpawn = false;
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
