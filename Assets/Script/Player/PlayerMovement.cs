using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject UIJoystickOuterCircle;
    public GameObject UIJoystick;

    private GameObject respawnPreview;
    private SpriteRenderer respawnSR;

    private Vector2 OriginalTransform;
    private Vector2 direction;
    private int leftTouch = 99;
    private bool canMove = true;

    [SerializeField] private float distanceDeath;

    private void Start()
    {
        respawnPreview = Instantiate(UIJoystick);
        respawnPreview.SetActive(true);
        respawnSR = respawnPreview.GetComponent<SpriteRenderer>();
        respawnSR.color = Color.red;
    }

    private void Update()
    {
        if (canMove)
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Touch t = Input.GetTouch(i);
                Vector2 touchPos = GetTouchPosition(t.position);
                if (t.phase == TouchPhase.Began)
                {
                    if (t.position.x > Screen.width / 2)
                    {
                        Interact();
                    }
                    else
                    {
                        leftTouch = t.fingerId;
                        OriginalTransform = touchPos;
                        UIJoystickOuterCircle.transform.position = touchPos;
                        UIJoystick.transform.position = UIJoystickOuterCircle.transform.position;
                        UIJoystick.SetActive(true);
                        UIJoystickOuterCircle.SetActive(true);
                    }
                }
                else if ((t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) && leftTouch == t.fingerId)
                {
                    Vector2 offset = touchPos - OriginalTransform;
                    direction = Vector2.ClampMagnitude(offset, 1);
                    MoveCharacter(direction);

                    OriginalTransform = UIJoystickOuterCircle.transform.position;
                    UIJoystick.transform.position = new Vector2(UIJoystickOuterCircle.transform.position.x + direction.x, UIJoystickOuterCircle.transform.position.y + direction.y);
                    respawnPreview.transform.position = GetRespawnPosition(direction) + transform.position;
                }
                else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
                {
                    leftTouch = 99;
                    UIJoystickOuterCircle.SetActive(false);
                    UIJoystick.SetActive(false);
                }
                i++;
            }
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        gameObject.transform.Translate(direction * 2 * Time.deltaTime);
    }

    private Vector3 GetRespawnPosition(Vector2 direction)
    {
        Vector3 respawnPos = -direction * 2;
        return respawnPos;
    }

    private void Interact()
    {
        Debug.Log("interacted");
    }

    private Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y));
    }

    private void Respawn(Vector3 respawnPos)
    {
        transform.position = transform.position - respawnPos.normalized * distanceDeath;
        canMove = false;

        StartCoroutine("DeathMoveThreshhold");
    }

    private IEnumerator DeathMoveThreshhold()
    {
        yield return new WaitForSeconds(3);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrapTrigger"))
            Respawn(direction);
    }
}
