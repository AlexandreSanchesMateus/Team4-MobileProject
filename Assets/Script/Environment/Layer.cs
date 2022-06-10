using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
    [HideInInspector] public int _id;
    public GameObject ground;
    public List<Zone> m_zones = new List<Zone>();
    public Vector2 m_startScale;
}
