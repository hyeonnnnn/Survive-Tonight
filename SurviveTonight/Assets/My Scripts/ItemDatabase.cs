using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    
    void Awake()
    {
        instance = this;
    }

    // 아이템 리스트
    public List<Item> itemDB;
    [Space(20)]
    public GameObject fieldItemPrefab;
    public Vector3[] pos;
    
    public Item GetItemByName(string itemName)
    {
        return itemDB.Find(item => item.itemName == itemName);
    }
}
