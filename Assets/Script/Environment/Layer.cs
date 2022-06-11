using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
    public GameObject ground;
    public List<Zone> m_zones = new List<Zone>();
    [HideInInspector] public Vector2 m_startScale;
}
