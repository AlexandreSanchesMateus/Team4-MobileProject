using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    private Animator _animator;

    public List<item> playerItems { get; private set; }
    [SerializeField] private float spacer = 50f;
    [SerializeField] private List<item>itemList = new List<item>();
    
    

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();

        Instance = this;
        playerItems = new List<item>();
        AddItem("blue");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem(playerItems[0]);
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
    
    public void RemoveItem(item itemToRemove)
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

    public void EnableItemManager()
    {
        Debug.Log("ItemActive");
        // _animator.SetBool(0, true);
    }

    public void DisableItemManager()
    {
        Debug.Log("ItemDesactive");
        // _animator.SetBool(0, false);
    }

    public bool IsItemInInventory(string _name)
    {
        foreach (item other in playerItems)
        {
            if(other.m_name == _name)
                return true;
        }
        return false;
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
}
