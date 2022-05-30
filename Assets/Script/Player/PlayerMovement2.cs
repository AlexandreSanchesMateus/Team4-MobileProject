using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public static PlayerMovement2 Instance { get; private set; }

    private Vector2 _startPosition;
    private int _movementFingerID = -1;
    private Coroutine _coroutine;

    private Rigidbody2D _rb;
    private bool canJump = false;
    private bool jump = false;

    [Header("Jump Settings")]
    [SerializeField] private Transform _groundPos;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _checkLayer;
    

    [Header("Movement Settings")]
    [SerializeField] private float _timeMovementAccepted = 1f;
    [SerializeField] private float _rangeMovementAccepted = 1f;
    [SerializeField] private float _moveSpeed;
    public Vector2 direction;
    public bool canMove = true;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject _UIJoystick;
    [SerializeField] private GameObject _UIJoystickOuterCircle;
    [SerializeField] private float _maxAmplitude;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;


    private void Start()
    {
        Instance = this;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _UIJoystickOuterCircle.SetActive(false);
        _UIJoystick.SetActive(false);
    }

    private void Update()
    {
        if (!canMove)
            return;

        Collider2D[] _info = Physics2D.OverlapCircleAll(_groundPos.position, _checkRadius, _checkLayer);
        canJump = false;
        foreach (Collider2D other in _info)
        {
            if (other.gameObject.CompareTag("Platform"))
                canJump = true;
        }

        foreach(Touch _touch in Input.touches)
        {
            if(_touch.phase == TouchPhase.Began)
            {
                /*if (_movementFingerID != -1)
                    Interaction();
                else
                {
                    _startPosition = _touch.position;
                    _coroutine = StartCoroutine(AssignedMovementTouch(_touch));
                }*/

                if (_movementFingerID == -1)
                {
                    _startPosition = _touch.position;
                    _coroutine = StartCoroutine(AssignedMovementTouch(_touch));
                }

                /*if (Input.touchCount > 1)
                    Debug.Log("Interaction");
                else
                {
                    _startScreenPosition = _touch.position;
                    _mouvementFingerID = _touch.fingerId;

                    UIJoystick.transform.position = _startScreenPosition;
                    UIJoystickOuterCircle.transform.position = _startScreenPosition;
                    UIJoystick.SetActive(true);
                    UIJoystickOuterCircle.SetActive(true);
                }*/

            }
            else if ((_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary))
            {
                    /*Vector3 worldPositionTouch = Camera.main.ScreenToWorldPoint(_touch.position);

                    direction = worldPositionTouch - Camera.main.ScreenToWorldPoint(_startPosition);
                    direction = Vector2.ClampMagnitude(direction, _maxAmplitude);*/
                
                if (_touch.fingerId == _movementFingerID)
                {
                    direction = GetDirection(_touch.position);
                    direction = Vector2.ClampMagnitude(direction, _maxAmplitude);

                    _UIJoystick.transform.position = Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(_startPosition) + new Vector3(direction.x, direction.y));
                    Debug.DrawRay(Camera.main.ScreenToWorldPoint(_startPosition), direction, Color.green);
                }
                else if(_movementFingerID == -1)
                {
                    Vector2 CheckMovementVec = GetDirection(_touch.position);

                    if (CheckMovementVec.magnitude > _rangeMovementAccepted)
                    {
                        StopCoroutine(_coroutine);
                        InitTouch(_touch);
                    }
                    Debug.DrawRay(Camera.main.ScreenToWorldPoint(_startPosition), CheckMovementVec, Color.red);
                }
                    
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                if (_touch.fingerId == _movementFingerID)
                {
                    direction = Vector2.zero;
                    _UIJoystick.SetActive(false);
                    _UIJoystickOuterCircle.SetActive(false);
                    _movementFingerID = -1;
                }
                else
                {
                    StopCoroutine(_coroutine);
                    Interaction(Camera.main.ScreenToWorldPoint(_touch.position));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(direction.magnitude > 0.1f)
            _rb.AddForce(new Vector2((direction.x / _maxAmplitude) * _moveSpeed * Time.fixedDeltaTime, 0f), ForceMode2D.Impulse);

        if (jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            jump = false;
        }
    }

    private Vector2 GetDirection(Vector2 screenPosition)
    {
        Vector3 worldPositionTouch = Camera.main.ScreenToWorldPoint(screenPosition);
        return worldPositionTouch - Camera.main.ScreenToWorldPoint(_startPosition);
    }

    private void Interaction(Vector2 position)
    {
        Collider2D[] _info = Physics2D.OverlapCircleAll(position, _interactionRadius, _interactionLayer);
        foreach(Collider2D other in _info)
        {
            if (other.gameObject.CompareTag("Interactible"))
            {
                Debug.Log("INTERACTION");
                return;
            }
        }

        if (canJump)
        {
            jump = true;
            Debug.Log("JUMP");
        }
    }

    private void InitTouch(Touch m_touch)
    {
        _movementFingerID = m_touch.fingerId;
        _UIJoystick.transform.position = _startPosition;
        _UIJoystickOuterCircle.transform.position = _startPosition;
        _UIJoystick.SetActive(true);
        _UIJoystickOuterCircle.SetActive(true);
    }

    private IEnumerator AssignedMovementTouch(Touch m_touch)
    {
        yield return new WaitForSeconds(_timeMovementAccepted);
        InitTouch(m_touch);
    }

    private void OnDrawGizmos()
    {
        if(canJump)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.black;
        Gizmos.DrawSphere(_groundPos.position, _checkRadius);
    }
}