using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }    


    [SerializeField] private List<item>itemList = new List<item>();
    public List<item> playerItems { get; private set; }


    private Transform baseTransform;
    private Vector2 orientation;

    private void Start()
    {
        Instance = this;
        playerItems = new List<item>();
    }

    public void AddItem(string name)
    {
        foreach (item thisItem in itemList)
        {
            if (thisItem.m_name == name)
            {
                playerItems.Add(thisItem);
                AddItemToViewport(thisItem);
            }
        }
    }

    private void AddItemToViewport(item itemToAdd)
    {
        GameObject thisImage = Instantiate<GameObject>(new GameObject(), transform);
        thisImage.AddComponent<RectTransform>();
        thisImage.AddComponent<Image>().sprite = itemToAdd.m_Sprite;
    }
}
