using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
    public Collider2D ground;
    public List<Zone> m_zones = new List<Zone>();
}
