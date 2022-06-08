using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public Item(Item originalItem)
    {
        this.m_name = originalItem.m_name;
        this.m_Sprite = originalItem.m_Sprite;
        this.m_ActiveSprite = originalItem.m_ActiveSprite;
    }

    public Sprite m_ActiveSprite;
    public string m_name;
    public Sprite m_Sprite;
    [HideInInspector] public GameObject m_GameObject;
    [HideInInspector] public int m_Index;
}
