using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TitlePanel : TTUIPage
{
    public Image imageTitle;
    //public Image imageAnyKey;
    public Image imageWhite;
    public Button buttonNew, buttonLoad;
    public TitlePanel() : base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/TitlePanal";
    }
    public override void Awake(GameObject go)
    {
        imageTitle = transform.Find("ImageTitle").GetComponent<Image>();
        imageWhite = transform.Find("ImageBG").GetComponent<Image>();
        buttonNew = transform.Find("ButtonNew").GetComponent<Button>();
        buttonLoad = transform.Find("ButtonLoad").GetComponent<Button>();

        imageTitle.color = new Color(1, 1, 1, 0);
        buttonNew.GetComponent<Image>().gameObject.SetActive(false);
        buttonLoad.GetComponent<Image>().gameObject.SetActive(false);
        imageWhite.DOFade(0, 0.8f).SetDelay(0.5f);
        
        imageTitle.DOFade(1, 1).SetDelay(4);
        buttonLoad.GetComponent<Image>().DOFade(1, 1).SetDelay(5).OnStart(() => {
            buttonLoad.GetComponent<Image>().gameObject.SetActive(true);
        });
        buttonNew.GetComponent<Image>().DOFade(1, 1).SetDelay(5).OnStart(() => {
            buttonNew.GetComponent<Image>().gameObject.SetActive(true);
        });
    }
    public override void Refresh()
    {
        buttonNew.onClick.AddListener(()=> {
            Tools.SceneManagers("My Character Creation");
        });
        buttonLoad.onClick.AddListener(() => {
            Tools.SceneManagers("DreamDev Village");
        });
    }
}
