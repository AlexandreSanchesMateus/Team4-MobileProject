using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    // private int layerOn = 0;

    private int layerUpper = 0;
    private int layerLower = 0;

    private Zone _inZone = null;
    private bool initSecurityZone = false;
    private float _stairPosition;

    [SerializeField] private int _startLayer = 0;
    [SerializeField] [Range(0.01f, 1f)] private float changeScale = 0.5f;
    [SerializeField] private List<Layer> layers = new List<Layer>();


    private void Start()
    {
        for(int i = layers.Count - 1; i > 0; i--)
        {
            Debug.Log(i);

            layers[i].m_startScale = layers[i].ground.transform.localScale;
            layers[i].m_startPosition = layers[i].ground.transform.position;
            InitLayer(i);
        }

        layers[0].m_startScale = layers[0].ground.transform.localScale;
        layers[0].m_startPosition = layers[0].ground.transform.position;

        // les rétrécire celon l'ordre dans les layers
    }

    private void Update()
    {
        int layerOn = PlayerMovement2.Instance.playerLayer;

        if (layers.Count == 0 || layerOn == -1)
        {
            Debug.LogWarning("Liste layers null ou problème de détection du joueur");
            return;
        }
            

        // Vector2 playerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = PlayerMovement2.Instance._groundPos.position;

        if (_inZone == null)
        {
             if (layerOn == 0)
                _inZone = CollideWithLayerZones(layers[layerOn].m_zones, playerPosition, 1);
            else if (layerOn == layers.Count - 1)
                _inZone = CollideWithLayerZones(layers[layerOn - 1].m_zones, playerPosition, layerOn - 1);
            else
            {
                _inZone = CollideWithLayerZones(layers[layerOn].m_zones, playerPosition, layerOn + 1);

                if (_inZone == null)
                    _inZone = CollideWithLayerZones(layers[layerOn - 1].m_zones, playerPosition, layerOn - 1);
            }

            // Changer le parallaxe

        }
        else
        {
            Debug.Log("Stay");

            if (_inZone.CollideWithZone(playerPosition))
            {
                _stairPosition = (playerPosition.y - _inZone.bottomRightCorner.y) / (_inZone.topLeftCorner.y - _inZone.bottomRightCorner.y);

                float xScaleIn = layers[layerUpper].m_startScale.x + changeScale;
                //float xScaleOut = layers[layerUpper].m_startScale.x - changeScale;

                // Agrandir
                layers[layerLower].ground.transform.localScale = Vector2.Lerp(layers[layerLower].m_startScale, new Vector2(xScaleIn, (xScaleIn * layers[layerLower].m_startScale.y) / layers[layerLower].m_startScale.x), _stairPosition);

                // Reduire
                //layers[layerUpper].ground.transform.localScale = layers[layerLower].ground.transform.localScale / ((xScaleIn * layers[layerLower].m_startScale.y) / layers[layerLower].m_startScale.x);
                //layers[layerUpper].ground.transform.localScale = Vector2.Lerp(new Vector2(xScaleOut, (xScaleOut * layers[layerUpper].m_startScale.y) / layers[layerUpper].m_startScale.x), layers[layerLower].m_startScale, _stairPosition);
                //layers[layerUpper].ground.transform.localScale = Vector2.Lerp(layers[layerLower].m_startScale, new Vector2(xScaleIn, (xScaleIn * layers[layerUpper].m_startScale.y) / layers[layerUpper].m_startScale.x), _stairPosition);

                layers[layerUpper].ground.gameObject.transform.position = layers[layerUpper].m_startPosition;

                if (!initSecurityZone)
                {
                    Debug.Log("INIT");

                    PlayerMovement2.Instance.usingLayerChanger = true;
                    PlayerMovement2.Instance._rb.gravityScale = 0f;

                    initSecurityZone = true;
                }

                if (PlayerMovement2.Instance.canJump)
                {
                    PlayerMovement2.Instance._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                    PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                }
            }
            else
            {
                Debug.Log("Exit");

                initSecurityZone = false;

                if (_stairPosition > 0.9f)
                {
                    layers[layerUpper].ground.gameObject.GetComponent<Collider2D>().enabled = true;
                    layers[layerLower].ground.gameObject.GetComponent<Collider2D>().enabled = false;

                    layers[layerUpper].ground.transform.localScale = new Vector2(changeScale, changeScale);
                    layers[layerLower].ground.transform.localScale = new Vector2(layers[layerLower].m_startScale.x + changeScale, ((layers[layerLower].m_startScale.x + changeScale) * layers[layerLower].m_startScale.y) / layers[layerLower].m_startScale.x);
                }
                else
                {
                    layers[layerUpper].ground.gameObject.GetComponent<Collider2D>().enabled = false;

                    layers[layerUpper].ground.transform.localScale = new Vector2(changeScale, changeScale);
                    layers[layerLower].ground.transform.localScale = new Vector2(layers[layerLower].m_startScale.x + changeScale, ((layers[layerLower].m_startScale.x + changeScale) * layers[layerLower].m_startScale.y) / layers[layerLower].m_startScale.x);
                }

                _inZone = null;

                PlayerMovement2.Instance._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                PlayerMovement2.Instance._rb.gravityScale = 1f;
                PlayerMovement2.Instance.usingLayerChanger = false;
            }
        }
    }

    private void InitLayer(int id)
    {
        float xScale;

        if (id > PlayerMovement2.Instance.playerLayer)
            xScale = layers[id].m_startScale.x - changeScale;
        else
            xScale = layers[id].m_startScale.x + changeScale;

        layers[id].ground.transform.localScale = new Vector2(xScale, (xScale * layers[id].m_startScale.y) / layers[id].m_startScale.x);
    }

    private Zone CollideWithLayerZones(List<Zone> _zones, Vector2 position, int _changeLayerId)
    {
        foreach (Zone other in _zones)
        {
            if (other.CollideWithZone(position))
            {
                Debug.Log("Enter");

                // init layer
                if (PlayerMovement2.Instance.playerLayer < _changeLayerId)
                {
                    // Monté
                    layerLower = PlayerMovement2.Instance.playerLayer;
                    layerUpper = _changeLayerId;
                }
                else
                {
                    // Descente
                    layerLower = _changeLayerId;
                    layerUpper = PlayerMovement2.Instance.playerLayer;
                    layers[layerLower].ground.gameObject.GetComponent<Collider2D>().enabled = true;
                }
                
                layers[layerUpper].ground.gameObject.GetComponent<Collider2D>().enabled = false;
               
                return other;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Layer layerID in layers)
        {
            foreach (Zone other in layerID.m_zones)
            {
                Gizmos.color = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
                Gizmos.DrawCube((other.topLeftCorner + other.bottomRightCorner) / 2, new Vector3(other.bottomRightCorner.x - other.topLeftCorner.x, other.topLeftCorner.y - other.bottomRightCorner.y, 0));
            }
        }
    }
}