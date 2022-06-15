using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public static PlayerMovement2 Instance { get; private set; }

    private Vector2 _startPosition;
    private int _movementFingerID = -1;
    private Coroutine _coroutine;
    private bool jump = false;
    private bool isJumping = false;


    [Header("Generale Settings")]
    [SerializeField] private Animator animator;
    public Rigidbody2D _rb;
    public int playerLayer = -1;

    [Header("Jump Settings")]
    public Transform _groundPos;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _checkLayer;
    public bool canJump = false;
    
    [Header("Movement Settings")]
    [SerializeField] private float _timeMovementAccepted = 1f;
    [SerializeField] private float _rangeMovementAccepted = 1f;
    [SerializeField] private float _moveSpeed;
    public Vector2 direction;
    public bool playerMovementEnable = true;
    public bool usingLayerChanger = false;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject _UIJoystick;
    [SerializeField] private GameObject _UIJoystickOuterCircle;
    [SerializeField] private float _maxAmplitude;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;

    [Header("Sound")]
    public AudioClip bruitDePas;

    private void Start()
    {
        Instance = this;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _UIJoystickOuterCircle.SetActive(false);
        _UIJoystick.SetActive(false);
    }

    private void Update()
    {
        // -------------------------------------------------------- //
        // #################### DEBUG MOVEMENT #################### //
        // -------------------------------------------------------- //

        // ATTENTION : DOIT ETRE RETIRER AVANT DE BUILD

        if (!playerMovementEnable)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            Collider2D[] col = Physics2D.OverlapCircleAll(_groundPos.position, _checkRadius, _checkLayer);
            canJump = false;
            foreach (Collider2D other in col)
            {
                if (other.gameObject.CompareTag("Platform"))
                {
                    canJump = true;
                    if (isJumping)
                    {
                        isJumping = false;
                        animator.SetBool("Jumping", false);
                    }
                } 
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                jump = true;
                isJumping = true;
                animator.SetBool("Jumping", true);
            }

            return;
        }

        Collider2D[] _info = Physics2D.OverlapCircleAll(_groundPos.position, _checkRadius, _checkLayer);
        canJump = false;
        foreach(Collider2D other in _info)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                canJump = true;
                if (isJumping)
                {
                    isJumping = false;
                    animator.SetBool("Jumping", false);
                }
            }
            //playerLayer = (int)System.Char.GetNumericValue(_info.tag[_info.tag.Length - 1]);
        }

        foreach(Touch _touch in Input.touches)
        {
            if(_touch.phase == TouchPhase.Began)
            {
                if (_movementFingerID == -1)
                {
                    _startPosition = _touch.position;
                    if(_coroutine != null)
                        StopCoroutine(_coroutine);
                    _coroutine = StartCoroutine(AssignedMovementTouch(_touch));
                }
            }
            else if ((_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary))
            {
                
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
                    DisableTouche();
                }
                else
                {
                    if(_coroutine != null)
                        StopCoroutine(_coroutine);
                    Interaction(Camera.main.ScreenToWorldPoint(_touch.position));
                }
            }
        }
    }

    public void DisableTouche()
    {
        direction = Vector2.zero;
        _UIJoystick.SetActive(false);
        _UIJoystickOuterCircle.SetActive(false);
        _movementFingerID = -1;
    }

    private void FixedUpdate()
    {
        if (direction.magnitude > 0.1f)
        {
            animator.SetFloat("Velocity", direction.x);
            _rb.AddForce(new Vector2((direction.x / _maxAmplitude) * _moveSpeed * Time.fixedDeltaTime, 0f));
            if (usingLayerChanger)
                _rb.AddForce(new Vector2(0f, (direction.y / _maxAmplitude) * _moveSpeed * Time.fixedDeltaTime));
        }

        if (jump)
        {
            animator.SetBool("Jumping", true);
            _rb.AddForce(new Vector2(0f, _jumpForce * Time.fixedDeltaTime));
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
                other.gameObject.GetComponent<Elevator>().Interact();
                return;
            }
        }

        if (canJump)
        {
            jump = true;
            isJumping = true;
            animator.SetBool("Jumping", true);
            Debug.Log("JUMP");
        }
    }

    private void InitTouch(Touch m_touch)
    {
        StopAllCoroutines();
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