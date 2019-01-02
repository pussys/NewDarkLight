using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class ForgingPanel : TTUIPage {

    private Button buttonForging;
    private Transform Item1, Item2;

    public ForgingPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/ForgingPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        buttonForging = transform.Find("ButtonForging").GetComponent<Button>();
        Item1 = transform.Find("Forging1");
        Item2 = transform.Find("Forging2");
        buttonForging.onClick.AddListener(Forging);
    }
    public void Forging()
    {
        Image item1, item2;
        item1 = Item1.GetChild(0).GetComponent<Image>();
        item2 = Item2.GetChild(0).GetComponent<Image>();
        if (item1.sprite.name==item2.sprite.name)
        {
            Save.ForgingItem(int.Parse(item1.sprite.name));
            ShowPage<TipPanel>("锻造成功");
            GameObject.Destroy(Item1.GetChild(0).gameObject);
            GameObject.Destroy(Item2.GetChild(0).gameObject);
        }
        else
        {
            Debug.Log("无法锻造不同武器");
        }
    }
}
