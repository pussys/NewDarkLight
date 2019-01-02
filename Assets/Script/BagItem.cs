using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 挂载在预设物上  
/// </summary>
public class BagItem : MonoBehaviour, IPointerEnterHandler
{
    private Button buttonBuy;

    //当前物品的图片
    private Sprite Sprite;
    //当前物品
    public Item CurrentGoods;

    public static event Action<Item> OnItemSelceted;
    
    private void Start()
    {
        InvokeRepeating("ShowBags", 1, 5);
    }
    public void Init(Item _Good,Sprite _sprite)
    {
        CurrentGoods = _Good;Sprite = _sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnItemSelceted != null)
        {
            OnItemSelceted(CurrentGoods);
        }
    }
}
