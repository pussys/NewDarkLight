using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TinyTeam.UI;

public class ShopItem : MonoBehaviour,IPointerDownHandler
{
    
    private Button buttonBuy;
    private Item itemInfo;
    private Toggle toggle;
    public static event Action<Item> OnItemSelected;
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectItem();
    }
    public void Init(Item _item)
    {
        itemInfo = _item;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        
        transform.Find("name").GetComponent<Text>().text = _item.item_Name;
        transform.Find("item_Type").GetComponent<Text>().text = _item.item_Type;
        transform.Find("price").GetComponent<Text>().text = _item.price;
        transform.Find("ImageSlot/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/" + _item.item_Img);
    }

    // Use this for initialization
    void Start () {
        buttonBuy = transform.Find("ButtonBuy").GetComponent<Button>();
        toggle = transform.Find("ImageSlot").GetComponent<Toggle>();
        buttonBuy.onClick.AddListener(() =>
        {
            Save.BuyItem(itemInfo);
            //SoundManager.instance.PlayingSound("BuyItem");
            TTUIPage.ShowPage<TipPanel>("购买成功！");

        });
        toggle.onValueChanged.AddListener(x => { SelectItem(); });

        Debug.LogWarning("记住调用Init方法，对物品信息进行初始化");
	}
	private void SelectItem()
    {
        if (OnItemSelected!=null)
        {
            OnItemSelected(itemInfo);
        }
    }
}
