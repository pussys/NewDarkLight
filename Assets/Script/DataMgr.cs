using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class DataMgr : MonoBehaviour
{
    private static DataMgr instance;
    private List<Item> itemList = new List<Item>();
    private void Awake()
    {
        Debug.Log(itemList.Count);
    }
    
    private DataMgr()
    {
        TextAsset ta = Resources.Load("Json/JsonText") as TextAsset;
        itemList = JsonConvert.DeserializeObject<List<Item>>(ta.text);
    }
    public static DataMgr GetInstance()
    {
        if (instance == null)
        {
            instance = new DataMgr();
        }
        return instance;
    }
    public Item GetItemByID(int _id)
    {
        return itemList.Find( a => a.item_ID == _id);
    }
    public int[] GetItem()
    {
        int[] sp = new int[itemList.Count];
        for (int i = 0; i < itemList.Count; i++)
        {
            sp[i] = itemList[i].item_ID;
        }
        return sp;
    }
    
}
public enum Task_Type
{
    collect,Fight
}
public enum Task_State
{
    Accept, Renounce, Complete, Unfinished
}
public enum Equipment_Type
{
    Null = 0, Head_Gear = 1, Armor = 2, Shoes = 3, Accessory = 4, Left_Hand = 5, Right_Hand = 6, Two_Hand = 7
}
[System.Serializable]
public class Item
{
    public string item_Name = "Item Name";
    public string item_Type = "Item Type";
    [Multiline]
    public string description = "Description Here";
    public int item_ID;
    public string item_Img;
    public string item_Effect;
    public string item_Sfx;
    public Equipment_Type equipment_Type;
    public string price;
    public int hp, mp, atk, def, spd, hit;
    public float criPercent, atkSpd, atkRange, moveSpd;
}


