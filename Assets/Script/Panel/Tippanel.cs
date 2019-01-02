using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;

public class TipPanel : TTUIPage {

    Text textContent;
    CanvasGroup cg;
	public TipPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/TipPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        textContent = transform.Find("ImageBg/textContent").GetComponent<Text>();
        cg = transform.GetComponent<CanvasGroup>();
        
    }
    public override void Refresh()
    {
        base.Refresh();
        textContent.text = data.ToString();
        cg.alpha = 1;
        cg.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(() => Hide());
    }
}
