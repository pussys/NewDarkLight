using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class EquipPanel : TTUIPage
{
    //6个格子
    private Transform head, arrmor, leftHand, rightHand, shoes, accessory;
    private Text infoName, infoDes;
    private Transform infoParent;
    public GameObject itemPrefab;
    public Button buttonUnload;
    Item tempItem;
    public EquipPanel() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/EquipPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        itemPrefab = Resources.Load<GameObject>("UIPrefab/equipItem");
        head = transform.Find("ImageHead");
        arrmor = transform.Find("ImageArmor");
        rightHand = transform.Find("ImageRHand");
        leftHand = transform.Find("ImageLhand");
        shoes = transform.Find("ImageShoe");
        accessory = transform.Find("ImageAcc");
        buttonUnload = transform.Find("ItemInfo/ButtonUnload").GetComponent<Button>();
        infoParent = transform.Find("ItemInfo");
        infoName = transform.Find("ItemInfo/TextName").GetComponent<Text>();
        infoDes = transform.Find("ItemInfo/TextDes").GetComponent<Text>();
        BagItem.OnItemSelceted += ShowSelectedItemInfo;
        buttonUnload.onClick.AddListener(() =>
        {
            infoParent.gameObject.SetActive(false);
            Save.UnloadEquip(tempItem);
            ShowBag();
            Save.UpdateUser();
        });
    }
    public override void Refresh()
    {
        base.Refresh();
        infoParent.gameObject.SetActive(false);
        ShowBag();
    }
    private void ShowSelectedItemInfo(Item gm)
    {
        infoParent.gameObject.SetActive(true);
        //Item tempItem = DataMgr.GetInstance().GetItemByID(gm.Id);
        tempItem = gm;
        infoName.text = gm.item_Name;
        infoDes.text = gm.description;

        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            infoParent.position = worldPos;
        }
    }
    public void ShowBag()
    {
        //清除背包
        ClearBag();
        //Item headItem = DataMgr.GetInstance().GetItemByID(Save.EquipList[0].Id);
        //SetPar(head, headItem);
        //遍历物品信息
        int j = 0;
        foreach (EquipModel item in Save.EquipList)
        {
            switch (item.Equipment_Type)
            {
                case Equipment_Type.Head_Gear:
                    SetPar(head,item);
                    j++;
                    break;
                case Equipment_Type.Armor:
                    SetPar(arrmor, item);
                    j++;
                    break;
                case Equipment_Type.Shoes:
                    SetPar(shoes, item);
                    j++;
                    break;
                case Equipment_Type.Accessory:
                    SetPar(accessory, item);
                    j++;
                    break;
                case Equipment_Type.Left_Hand:
                    SetPar(leftHand, item);
                    j++;
                    break;
                case Equipment_Type.Right_Hand:
                    SetPar(rightHand, item);
                    j++;
                    break;
                case Equipment_Type.Two_Hand:
                    SetPar(leftHand, item);
                    SetPar(rightHand, item);
                    j++;
                    break;
            }
        }
    }
    public void SetPar(Transform trans, EquipModel item)
    {
        GameObject go = GameObject.Instantiate(itemPrefab);
        go.transform.SetParent(trans);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        Sprite tempSprite = Resources.Load<Sprite>("Image/" + item.Id);
        go.GetComponent<Image>().sprite = tempSprite;
        Item gm = DataMgr.GetInstance().GetItemByID(item.Id);
        go.GetComponent<BagItem>().Init(gm, tempSprite);
    }
    public void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).transform.childCount != 0)
            {
                Transform t = transform.GetChild(i).transform.GetChild(0);
                t.parent = null;
                GameObject.Destroy(t.gameObject);
            }
        }
    }
}
