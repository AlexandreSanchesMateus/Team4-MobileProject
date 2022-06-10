using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private int layerOn = 0;
    private int layerGoing = 0;
    private Zone _inZone = null;
    private bool initSecurityZone = false;
    [SerializeField] private int _startLayer = 0;
    [SerializeField] private float changeScale = 10f;
    [SerializeField] private List<Layer> layers = new List<Layer>();


    private void Start()
    {
        for(int i = 0; i < layers.Count; i++)
        {
            layers[i].m_startScale = layers[i].ground.transform.localScale;
            layers[i]._id = i;

            foreach(Zone zone in layers[i].m_zones)
            {
                zone.m_layer = layers[i];
            }
        }

        if(_startLayer == 0 && layers.Count > 1)
        {
            LoadLayer(1);
        }

        // les rétrécire celon l'ordre dans les layers
    }

    private void Update()
    {
        if (layers.Count == 0)
            return;

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

            layers[layerGoing].ground.gameObject.GetComponent<Collider2D>().enabled = false;

            if (_inZone.CollideWithZone(playerPosition))
            {
                float _stairPosition = (playerPosition.y - _inZone.bottomRightCorner.y) / (_inZone.topLeftCorner.y - _inZone.bottomRightCorner.y);

                float xScaleIn = layers[layerOn].m_startScale.x + changeScale;
                float xScaleOut = layers[layerGoing].m_startScale.x - changeScale;

                /*if (layerOn < layerGoing)
                {*/
                    // Agrandir
                    layers[layerOn].ground.transform.localScale = Vector2.Lerp(layers[layerOn].m_startScale, new Vector2(xScaleIn, (xScaleIn * layers[layerOn].m_startScale.y) / layers[layerOn].m_startScale.x), _stairPosition);
                    // Reduire
                    layers[layerGoing].ground.transform.localScale = Vector2.Lerp(new Vector2(xScaleOut, (xScaleOut * layers[layerGoing].m_startScale.y) / layers[layerGoing].m_startScale.x), layers[layerOn].m_startScale, _stairPosition);
                //}

                PlayerMovement2.Instance.usingLayerChanger = true;
                PlayerMovement2.Instance._rb.gravityScale = 0f;

                if (PlayerMovement2.Instance.canJump)
                {
                    PlayerMovement2.Instance._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                    PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                    if (_inZone.m_layer._id != layerOn)
                        layerOn = layerGoing;
                }
                else
                {
                    PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                }
            }
            else if (_inZone.CollideWithSecurityZone(playerPosition))
            {
                Debug.Log("Security");

                if (!initSecurityZone)
                {
                    Debug.Log("INIT");

                    PlayerMovement2.Instance.usingLayerChanger = true;
                    PlayerMovement2.Instance._rb.gravityScale = 0f;

                    initSecurityZone = true;
                }
            }
            else
            {
                Debug.Log("Exit");

                if (initSecurityZone)
                {
                    initSecurityZone = false;

                    /*if (_inZone.m_layer._id != layerOn)
                    {
                        layerOn = layerGoing;
                        Debug.Log("BITE");
                    }*/
                }

                layers[layerGoing].ground.gameObject.GetComponent<Collider2D>().enabled = true;
                _inZone = null;

                PlayerMovement2.Instance._rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                PlayerMovement2.Instance._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                PlayerMovement2.Instance._rb.gravityScale = 1f;
                PlayerMovement2.Instance.usingLayerChanger = false;
            }
        }
    }

    private void LoadLayer(int id)
    {
        float xScale;

        if (id > layerOn)
            xScale = layers[id].m_startScale.x - changeScale;
        else
            xScale = layers[id].m_startScale.x + changeScale;

        layers[id].ground.transform.localScale = new Vector2(xScale, (xScale * layers[id].m_startScale.y) / layers[id].m_startScale.x);
    }

    private Zone CollideWithLayerZones(List<Zone> _zones, Vector2 position, int _changeLayerId)
    {
        foreach (Zone other in _zones)
        {
            if (other.CollideWithZone(position) || other.CollideWithSecurityZone(position))
            {
                Debug.Log("Enter");
                layerGoing = _changeLayerId;
                return other;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Layer layerID in layers)
        {
            foreach(Zone other in layerID.m_zones)
            {
                Gizmos.color = new Vector4(0.5f,0.5f,0.5f,0.5f);
                Gizmos.DrawCube((other.topLeftCorner + other.bottomRightCorner) / 2, new Vector3(other.bottomRightCorner.x - other.topLeftCorner.x, other.topLeftCorner.y - other.bottomRightCorner.y, 0));
                Gizmos.color = new Vector4(0,1,0,0.5f);
                Gizmos.DrawCube((other.topLeftCorner + new Vector2(other.bottomRightCorner.x, other.topLeftCorner.y + other.security)) / 2, new Vector3(other.bottomRightCorner.x - other.topLeftCorner.x, other.security, 0));
            }
        }
    }
}