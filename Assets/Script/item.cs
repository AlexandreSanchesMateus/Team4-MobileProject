using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class item 
{
    public item(item originalItem)
    {
        this.m_name = originalItem.m_name;
        this.m_Sprite = originalItem.m_Sprite;
    }

    public string m_name;
    public Sprite m_Sprite;
    [HideInInspector] public GameObject m_GameObject;
    [HideInInspector] public int m_Index;
}
