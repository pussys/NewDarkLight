using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System;

public class BagPanel : TTUIPage {

    public GameObject itemPrefab;
    public Transform Grid;
    public List<GoodsModel> BagList;
    public List<GameObject> GridArray=new List<GameObject>();
    public ToggleGroup group;
    private Text infoName, infoDes;
    private Transform iteminfo;
    public Button buttonUse, buttonCancel;
    MonoBehaviour mo;
    //public static event Action<>
    Item tempItem;
    public BagPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/Bag";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化物品预制
        itemPrefab = Resources.Load<GameObject>("UIPrefab/BagItem");
        Grid = transform.Find("Content").transform;
        BagItem.OnItemSelceted += ShowSelectedItemInfo;

        iteminfo = transform.Find("Eject");
        infoName = iteminfo.transform.Find("TextName").GetComponent<Text>();
        infoDes = iteminfo.transform.Find("TextDes").GetComponent<Text>();

        iteminfo.gameObject.SetActive(false);
        buttonUse = iteminfo.transform.Find("ButtonUse").GetComponent<Button>();
        buttonCancel = iteminfo.transform.Find("ButtonCancel").GetComponent<Button>();
        buttonCancel.onClick.AddListener(() => iteminfo.gameObject.SetActive(false));
        buttonUse.onClick.AddListener(() =>
        {
            iteminfo.gameObject.SetActive(false);
            Save.UseItem(tempItem);
            //SoundManager.instance.PlayingSound("BuyItem");
            TTUIPage.ShowPage<TipPanel>("使用成功！");
            Save.UpdateUser();
            ShowBag();
        });
        for (int i = 0; i < Grid.childCount; i++)
        {
            GridArray.Add(Grid.GetChild(i).gameObject);
        }
        
    }
    private void ShowSelectedItemInfo(Item gm)
    {
        iteminfo.gameObject.SetActive(true);
        tempItem = gm;
        //tempItem = DataMgr.GetInstance().GetItemByID(gm.Id);
        infoName.text = gm.item_Name;
        infoDes.text = gm.description;

        Vector3 worldPos;
        if( RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            iteminfo.position = worldPos;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        iteminfo.gameObject.SetActive(false);
        ShowBag();
    }
    public void ShowBag()
    {
        //清除背包
        ClearBag();

        //遍历物品信息
        int j = 0;
        foreach (GoodsModel item in Save.SaveGoods)
        {
            if (item.Num != 0)//物品数量不等于零时
            {
                //创建物品
                GameObject go = GameObject.Instantiate(itemPrefab);
                go.transform.SetParent(Grid.GetChild(j));
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                //显示物体的图片及数量
                Sprite tempSprite = Resources.Load<Sprite>("Image/" + item.Id);
                go.GetComponent<Image>().sprite = tempSprite;

                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                Item i = DataMgr.GetInstance().GetItemByID(item.Id); 
                go.GetComponent<BagItem>().Init(i, tempSprite);
                j++;
            }
        }
    }
    public void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < GridArray.Count; i++)
        {
            if (GridArray[i].transform.childCount != 0)
            {
                Transform t = GridArray[i].transform.GetChild(0);
                t.parent = null;
                GameObject.Destroy(t.gameObject);
            }
        }
    }
}
