using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class StatusPanel : TTUIPage {
    private Text atk, def, spd, hit,atkTotal,defTotal,spdTotal,hitTotal;
	public StatusPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/StatusPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        atk = transform.Find("Atk/Num").GetComponent<Text>();
        def = transform.Find("Def/Num").GetComponent<Text>();
        spd = transform.Find("Spd/Num").GetComponent<Text>();
        hit = transform.Find("Hit/Num").GetComponent<Text>();
        atkTotal = transform.Find("AtkTotal").GetComponent<Text>();
        defTotal = transform.Find("DefTotal").GetComponent<Text>();
        spdTotal = transform.Find("SpdTotal").GetComponent<Text>();
        hitTotal = transform.Find("HitTotal").GetComponent<Text>();
        Save.UpdateUser();
    }
    public override void Refresh()
    {
        base.Refresh();
        UpdateUser();
    }
    public void UpdateUser()
    {
        atk.text = Save.SaveUser[0].Attack.ToString() + "+" + Save.SaveUser[1].Attack.ToString();
        def.text = Save.SaveUser[0].Defend.ToString() + "+" + Save.SaveUser[1].Defend.ToString();
        spd.text = Save.SaveUser[0].Speed.ToString() + "+" + Save.SaveUser[1].Speed.ToString();
        hit.text = Save.SaveUser[0].Hit.ToString() + "+" + Save.SaveUser[1].Hit.ToString();
        atkTotal.text = "Attack："+(Save.SaveUser[0].Attack + Save.SaveUser[1].Attack).ToString();
        defTotal.text = "Defend："+(Save.SaveUser[0].Defend + Save.SaveUser[1].Defend).ToString();
        spdTotal.text = "Speed：" + (Save.SaveUser[0].Speed + Save.SaveUser[1].Speed).ToString();
        hitTotal.text = "Hit：" + (Save.SaveUser[0].Hit + Save.SaveUser[1].Hit).ToString();
    }
}
