using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PortraitMode : MonoBehaviour
{
    public static bool canDoAnything = true;
    public bool portraitModeEnable = false;
    public GameObject trailObj;
    private Trail trail;

    private Vector2 _lastPosition;
    private CinemachineVirtualCamera vitualCamera;
    public static Item _selectedItem = null;

    public float radius;
    private bool spritechange = false;

    [SerializeField] private GameObject _targerCam;
    [SerializeField] private float _sensitivity = 2;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minFocus;
    [SerializeField] private float _maxFocus;



    private void Start()
    {
        vitualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        trail = trailObj.GetComponent<Trail>();
    }

    private void Update()
    {
        if (canDoAnything)
        {
            if (portraitModeEnable)
            {
                // Focus Camera
                if (Input.touchCount == 2)
                {
                    Touch firstTouch = Input.GetTouch(0);
                    Touch secondTouch = Input.GetTouch(1);

                    float touchesPrePosDifference = ((firstTouch.position - firstTouch.deltaPosition) - (secondTouch.position - secondTouch.deltaPosition)).magnitude;
                    float touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                    float zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * _zoomSpeed * Time.deltaTime;

                    if (touchesPrePosDifference > touchesCurPosDifference)
                        vitualCamera.m_Lens.OrthographicSize += zoomModifier;
                    if (touchesPrePosDifference < touchesCurPosDifference)
                        vitualCamera.m_Lens.OrthographicSize -= zoomModifier;

                    vitualCamera.m_Lens.OrthographicSize = Mathf.Clamp(vitualCamera.m_Lens.OrthographicSize, _minFocus, _maxFocus);

                    // Mouvement Camera pendant zoom
                    Vector2 middlePos = (Camera.main.ScreenToWorldPoint(firstTouch.position) + Camera.main.ScreenToWorldPoint(secondTouch.position)) / 2;
                    if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
                        _lastPosition = middlePos;

                    CameraMovement(middlePos);

                    if (firstTouch.phase == TouchPhase.Ended)
                        _lastPosition = Camera.main.ScreenToWorldPoint(secondTouch.position);
                    else if (secondTouch.phase == TouchPhase.Ended)
                        _lastPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
                }
                else if (Input.touchCount == 1)
                {
                    Touch _touch = Input.GetTouch(0);

                    if (_touch.phase == TouchPhase.Began)
                    {

                        // ###########################        Selection      ########################### //


                        Item currentPressedItem = SelectColor(_touch.position);

                        if (currentPressedItem != null)
                        {

                            if (currentPressedItem != _selectedItem)
                            {
                                if (spritechange)
                                    _selectedItem.m_GameObject.GetComponent<Image>().sprite = _selectedItem.m_Sprite;

                                _selectedItem = currentPressedItem;
                                Debug.Log("Couleur " + _selectedItem.m_name + " est selectionné");
                                _selectedItem.m_GameObject.GetComponent<Image>().sprite = _selectedItem.m_ActiveSprite;
                                spritechange = true;
                            }
                            else
                            {
                                _selectedItem.m_GameObject.GetComponent<Image>().sprite = _selectedItem.m_Sprite;
                                _selectedItem = null;
                                _lastPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                                Debug.Log("Couleur " + currentPressedItem.m_name + " est désactiver");
                                spritechange = false;
                            }
                        }
                        //else if (Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(_touch.position), 0.2f).gameObject.CompareTag("Interactible"))
                        //{
                        //    canDoAnything = false;
                        //    Debug.Log("Object Interactible");
                        //}
                        else
                            _lastPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                    }
                    else if (_touch.phase == TouchPhase.Moved)
                    {
                        if (_selectedItem == null)
                            CameraMovement(Camera.main.ScreenToWorldPoint(_touch.position));
                        else if (_selectedItem.m_name == "grey")
                        {
                            trailObj.transform.position = Camera.main.ScreenToWorldPoint(_touch.position);
                            trail.CheckInteraction();
                        }
                    }
                    else if (_touch.phase == TouchPhase.Ended)
                    {
                        if (_selectedItem != null && _selectedItem.m_name == "red")
                        {
                            Debug.Log("bonjour");
                            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(_touch.position), 1f);
                            foreach (Collider2D collider in hitInfo)
                            {
                                if (collider.gameObject.CompareTag("Torch"))
                                {
                                    collider.gameObject.GetComponent<Torch>().AskToLight();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _targerCam.transform.position = PlayerMovement2.Instance.gameObject.transform.position;
            }
        }
    }

    private void CameraMovement(Vector2 position)
    {
        Vector3 movement = _lastPosition - position;
        movement.z = 0;
        _targerCam.transform.position = _targerCam.transform.position + movement * _sensitivity;
        _lastPosition = position;
    }

    private Item SelectColor(Vector2 position)
    {
        foreach(Item other in ItemManager.Instance.playerItems)
        {
            Sprite otherSprite = other.m_GameObject.GetComponent<Image>().sprite;
            float widthDiv = otherSprite.rect.width / 2;
            float heightDiv = otherSprite.rect.height / 2;

            if ((position.x < other.m_GameObject.transform.position.x + widthDiv && position.x > other.m_GameObject.transform.position.x - widthDiv)
                && (position.y < other.m_GameObject.transform.position.y + heightDiv && position.y > other.m_GameObject.transform.position.y - heightDiv))
                return other;
        }
        return null;
    }
}
