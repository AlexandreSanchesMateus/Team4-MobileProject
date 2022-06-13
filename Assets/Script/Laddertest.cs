using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laddertest : MonoBehaviour
{
    public bool activated = false;

    private Vector2 startPos;
    private Vector2 LerpStart;
    private Vector2 LerpEnd;

    public Transform activatedGoal;

    private float elapsedTime;
    private float duration = 2f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageCompleted = (elapsedTime / duration) ;

        if (activated)
        {
            elapsedTime = 0;
            LerpStart = transform.position;
            LerpEnd = activatedGoal.position;
        }
        else
        {
            elapsedTime = 0;
            percentageCompleted = (elapsedTime / duration) + 0.03f;
            LerpStart = transform.position;
            LerpEnd = startPos;
        }

        transform.position = Vector2.Lerp(LerpStart, LerpEnd, percentageCompleted);
    }
}
