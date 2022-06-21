using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    private Animator _animator;

    public List<Item> playerItems { get; private set; }
    [SerializeField] private float spacer = 50f;
    [SerializeField] private List<Item> itemList = new List<Item>();    
    

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();

        Instance = this;
        playerItems = new List<Item>();
        //AddItem("red");
        //AddItem("grey");
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem(playerItems[0]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddItem(itemList[Random.Range(0, itemList.Count)].m_name);
        }
    }*/

    public void AddItem(string name)
    {
        foreach (Item thisItem in itemList)
        {
            if (thisItem.m_name == name)
            {
                Item newItem = (new Item(thisItem));
                playerItems.Add(newItem);
                AddItemToViewport(newItem);
                
                /*if(name == "gris" && PlayGameService.Instance.isConnectedToGoogleService)
                    Social.ReportProgress("Cfjewijawiu_QA", 100, null);
                else if(name == "red" && PlayGameService.Instance.isConnectedToGoogleService)
                    Social.ReportProgress("Cfjewijawiu_QA", 100, null);*/
                return;
            }
        }

        Debug.LogWarning("The item '" + name + "' cannot be found, please check the name");
    }
    
    public void RemoveItem(Item itemToRemove)
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

    public void EnableItemManager(bool active)
    {
        _animator.SetBool("Active", active);
    }

    public bool IsItemInInventory(string _name)
    {
        foreach (Item other in playerItems)
        {
            if(other.m_name == _name)
                return true;
        }
        return false;
    }

    private void AddItemToViewport(Item itemToAdd)
    {
        GameObject thisImage = new GameObject();
        thisImage.transform.parent = transform;
        thisImage.AddComponent<RectTransform>().localScale = new Vector3(5,5);
        thisImage.AddComponent<CanvasRenderer>();
        thisImage.AddComponent<Image>().sprite = itemToAdd.m_Sprite;
        itemToAdd.m_GameObject = thisImage;
        itemToAdd.m_Index = playerItems.Count - 1;
        thisImage.transform.localPosition = new Vector3(spacer * (playerItems.Count-1), transform.position.y, transform.position.z);
    }
}
