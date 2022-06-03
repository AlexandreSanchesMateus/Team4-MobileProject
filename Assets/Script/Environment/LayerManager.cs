using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private int layerID = 0;
    private Zone _inZone;
    private bool takeChangerLayer = false;
    [SerializeField] private float changeScale = 10f;
    [SerializeField] private List<Layer> layers = new List<Layer>();


    private void Start()
    {
        for(int i = 0; i < layers.Count; i++)
        {
            layers[i].ground.enabled = false;
        }
    }

    private void Update()
    {
        Vector2 playerPosition = PlayerMovement2.Instance.gameObject.transform.position;

        if (_inZone == null)
        {
            if (layerID == 0)
                _inZone = CollideWithLayerZones(layers[layerID].m_zones, playerPosition);
            else if (layerID == layers.Count - 1)
                _inZone = CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition);
            else
            {
                _inZone = CollideWithLayerZones(layers[layerID].m_zones, playerPosition);
                if(_inZone == null)
                    _inZone = CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition);
            }
        }
        else
        {
            
        }
    }

    private Zone CollideWithLayerZones(List<Zone> _zones, Vector2 position)
    {
        foreach (Zone other in _zones)
        {
            if (other.CollideWithZone(position) || other.CollideWithSecurityZone(position))
            {
                Debug.Log("Enter");
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