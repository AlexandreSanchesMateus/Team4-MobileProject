using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private int layerID = 0;
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

        if(layerID == 0)
        {
            if (CollideWithLayerZones(layers[layerID].m_zones, playerPosition))
                PlayerMovement2.Instance.usingLayerChanger = true;
        }
        else if(layerID == layers.Count - 1)
        {
            CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition);
        }
        else
        {
            CollideWithLayerZones(layers[layerID].m_zones, playerPosition);
            CollideWithLayerZones(layers[layerID - 1].m_zones, playerPosition);
        }
    }

    private bool CollideWithLayerZones(List<Zone> _zones, Vector2 position)
    {
        foreach (Zone other in _zones)
        {
            if (other.CollideWithZone(position) || other.CollideWithSecurityZone(position))
                return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Layer layerID in layers)
        {
            foreach(Zone other in layerID.m_zones)
            {
                Gizmos.DrawCube((other.topLeftCorner + other.bottomRightCorner) / 2, new Vector3(other.bottomRightCorner.x - other.topLeftCorner.x, other.topLeftCorner.y - other.bottomRightCorner.y, 0));
                Gizmos.color = Color.green;
                Gizmos.DrawCube((other.topLeftCorner + new Vector2(other.bottomRightCorner.x, other.topLeftCorner.y + other.security)) / 2, new Vector3(other.bottomRightCorner.x - other.topLeftCorner.x, other.security, 0));
                Gizmos.color = Color.black;
            }
        }
    }
}