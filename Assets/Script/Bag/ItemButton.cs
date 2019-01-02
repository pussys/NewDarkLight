using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class ItemButton : MonoBehaviour, IPointerDownHandler
{
    //当前物品的图片
    public Sprite Sprite;
    //当前物品
    public GoodsModel CurrentGoods;
    public static event Action<GoodsModel> OnItemSelceted;

    internal void Init(GoodsModel _Good, Sprite _Sprite)
    {
        CurrentGoods = _Good; Sprite = _Sprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnItemSelceted != null)
        {
            OnItemSelceted(CurrentGoods);
        }
    }
}
