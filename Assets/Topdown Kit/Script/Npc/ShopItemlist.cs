/// <summary>
/// Npc shop.
/// This script use to create a shop to sell item
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ShopItemlist : MonoBehaviour {

    public static event Action<bool,List<int>,string> OnNpcTrigger;//跟进入和退出NPC触发器相关的时间
	public List<int> itemID = new List<int>();
    
    //public Button gantanhao;

    void Start()
	{
        
        if (this.gameObject.tag == "Untagged")
			this.gameObject.tag = "Npc_Shop";
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (OnNpcTrigger != null)//如果碰到了NPC就启动这个事件
            {
                OnNpcTrigger(true, itemID,transform.name);
            }   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (OnNpcTrigger!=null)
        {
            OnNpcTrigger(false, itemID, transform.name);
        }
    }
}


