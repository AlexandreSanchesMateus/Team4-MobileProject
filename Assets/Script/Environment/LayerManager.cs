using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private int layerID = 0;
    private int layerIDtoGo = 0;
    private Zone _inZone = null;
    private bool initSecurityZone = false;
    [SerializeField] private int _startLayer = 0;
    [SerializeField] private float changeScale = 10f;
    [SerializeField] private List<Layer> layers = new List<Layer>();


    private void Start()
    {
        foreach(Layer other in layers)
        {
            other.m_startScale = other.ground.transform.localScale;
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

        Vector2 playerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Player position !!!!

        if (_inZone == null)
        {
           if (layerID == 0)
                _inZone = CollideWithLayerZones(layers[layerID].m_zones, playerPosition, 1);
            else if (layerID == layers.Count - 1)
                _inZone = CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition, layerID - 1);
            else
            {
                _inZone = CollideWithLayerZones(layers[layerID].m_zones, playerPosition, layerID + 1);

                if (_inZone == null)
                    _inZone = CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition, layerID - 1);
            }

            // Changer le parallaxe

        }
        else
        {
            if (_inZone.CollideWithZone(playerPosition))
            {
                float _stairPosition = (playerPosition.y - _inZone.bottomRightCorner.y) / (_inZone.topLeftCorner.y - _inZone.bottomRightCorner.y);

                float xScaleIn = layers[layerID].m_startScale.x + changeScale;
                float xScaleOut = layers[layerIDtoGo].m_startScale.x - changeScale;

                // Agrandir
                layers[layerID].ground.transform.localScale = Vector2.Lerp(layers[layerID].m_startScale, new Vector2(xScaleIn, (xScaleIn * layers[layerID].m_startScale.y) / layers[layerID].m_startScale.x), _stairPosition);
                // Reduire
                layers[layerIDtoGo].ground.transform.localScale = Vector2.Lerp(new Vector2(xScaleOut, (xScaleOut * layers[layerIDtoGo].m_startScale.y) / layers[layerIDtoGo].m_startScale.x), layers[layerID].m_startScale, _stairPosition);
                
                // doit changer le scale avec l'encre + tout ce derrière

                Debug.Log("Stay");
            }
            else if (_inZone.CollideWithSecurityZone(playerPosition))
            {
                if (!initSecurityZone)
                {
                    // Rigidbody
                    // mouvement verticaux
                    // mouvement horizontaux
                    Debug.Log("INIT");
                    initSecurityZone = true;
                }

                Debug.Log("Security");
            }
            else
            {
                Debug.Log("Exit");
                initSecurityZone = false;
                _inZone = null;

                // Rigidbody
                // mouvement verticaux
                // mouvement horizontaux
            }
        }
    }

    private void LoadLayer(int id)
    {
        float xScale;

        if (id > layerID)
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
                layerIDtoGo = _changeLayerId;
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