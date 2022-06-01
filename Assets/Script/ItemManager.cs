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

    [SerializeField] private float spacer = 50f;

    private void Start()
    {
        Instance = this;
        playerItems = new List<item>();
        AddItem("blue");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItemFromViewport(playerItems[0]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddItem(itemList[Random.Range(0, itemList.Count)].m_name);
        }
    }

    public void AddItem(string name)
    {
        foreach (item thisItem in itemList)
        {
            if (thisItem.m_name == name)
            {
                item newItem = (new item(thisItem));
                playerItems.Add(newItem);
                AddItemToViewport(newItem);
            }
        }
    }

    private void AddItemToViewport(item itemToAdd)
    {
        GameObject thisImage = new GameObject();
        thisImage.transform.parent = transform;
        thisImage.AddComponent<RectTransform>().localScale = new Vector3(2,2);
        thisImage.AddComponent<CanvasRenderer>();
        thisImage.AddComponent<Image>().sprite = itemToAdd.m_Sprite;
        itemToAdd.m_GameObject = thisImage;
        itemToAdd.m_Index = playerItems.Count - 1;
        thisImage.transform.localPosition = new Vector3(spacer * (playerItems.Count-1), transform.position.y, transform.position.z);
    }

    private void RemoveItemFromViewport(item itemToRemove)
    {
        int thisIndex = itemToRemove.m_Index;
        Destroy(itemToRemove.m_GameObject);
        playerItems.Remove(itemToRemove);
        for (int i = thisIndex; i < playerItems.Count; i++)
        {
            playerItems[i].m_GameObject.transform.localPosition = new Vector3(spacer * i, transform.position.y, transform.position.z);
            playerItems[i].m_Index = i;
        }
    }
}
