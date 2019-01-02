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

    public static event Action<bool,List<int>,string> OnNpcTrigger;//��������˳�NPC��������ص�ʱ��
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
            if (OnNpcTrigger != null)//���������NPC����������¼�
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


