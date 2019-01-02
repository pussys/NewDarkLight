using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System;

public class ShopPanel : TTUIPage {
    private GameObject itemPrefab;
    public static Transform conter,shop;//子物体和父物体
    private GameObject iteminfo;
    private ToggleGroup group;
    private Text infoName, infoDes;
    public ShopPanel() : base(UIType.Normal, UIMode.NeedBack, UICollider.None)
    {
        uiPath = "UIPrefab/Shop";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        itemPrefab = Resources.Load<GameObject>("UIPrefab/Grid");
        conter = transform.Find("Scroll View/Viewport/Content");
        iteminfo = transform.Find("ItemInfo").gameObject;
        group = transform.Find("ToggleGroup").GetComponent<ToggleGroup>();
        ShopItem.OnItemSelected += ShowSelectedItemInfo;

        infoName = iteminfo.transform.Find("TextName").GetComponent<Text>();
        infoDes = iteminfo.transform.Find("TextDes").GetComponent<Text>();
        iteminfo.gameObject.SetActive(false);
        
        List<int> _itemID = (List<int>)data;
        for (int i = 0; i < _itemID.Count; i++)
        {
            GameObject Grid = GameObject.Instantiate(itemPrefab);
            Item info = DataMgr.GetInstance().GetItemByID(_itemID[i]);
            Grid.transform.SetParent(conter);
            Grid.transform.Find("ImageSlot").GetComponent<Toggle>().group = group;
            Grid.transform.GetComponent<ShopItem>().Init(info);
        }
    }

    private void ShowSelectedItemInfo(Item obj)
    {
        iteminfo.gameObject.SetActive(true);
        infoName.text = obj.item_Name;
        infoDes.text = obj.description;
    }

    /// <summary>
    /// 是否显示提示按钮
    /// </summary>
    /// <param name="isOn">按钮是否显示</param>
    /// <param name="_itemID">NPC传来的物品列表</param>
    public override void Hide()
    {
        base.Hide();
        Clear();
    }
    void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
