using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class MainPanel : TTUIPage
{
    private Button buttonStatus, buttonEquip, buttonBag, buttonSkill, buttonTishi,buttonSetting,buttonClose, buttonForging,buttonTask;
    private bool isBag,isEquip,isStatus, isForging,isTask;
    private Button mapzoomin, mapzoomount;
    Camera minicamera;
    Transform conter, par;//子物体和父物体
    List<TaskModel> UserTask;
    List<TaskModel> TaskList;
    public MainPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIprefab/MainPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        buttonStatus = transform.Find("Button/ButtonStatus").GetComponent<Button>();
        buttonEquip = transform.Find("Button/ButtonEquip").GetComponent<Button>();
        buttonBag = transform.Find("Button/ButtonBag").GetComponent<Button>();
        buttonSkill = transform.Find("Button/ButtonSkill").GetComponent<Button>();
        buttonTishi = transform.Find("ButtonTishi").GetComponent<Button>();
        buttonSetting = transform.Find("ButtonSetting").GetComponent<Button>();
        buttonForging = transform.Find("Button/ButtonForging").GetComponent<Button>();
        buttonTask = transform.Find("Button/ButtonTask").GetComponent<Button>();
        mapzoomin = transform.Find("MiniMap/minimap_zoomin").GetComponent<Button>();
        mapzoomount = transform.Find("MiniMap/minimap_zoomout").GetComponent<Button>();
        minicamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();
        mapzoomin.onClick.AddListener(() => {
            if (minicamera.orthographicSize<15)
            {
                minicamera.orthographicSize += 2;
            }
        });
        mapzoomin.onClick.AddListener(() => {
            if (minicamera.orthographicSize > 4)
            {
                minicamera.orthographicSize -= 2;
            }
        });
        par = Resources.Load<Transform>("UIPrefab/Grid");

        buttonTishi.gameObject.SetActive(false);
        ShopItemlist.OnNpcTrigger += ShowTishi;//给Npc绑定事件
        //buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>());
        buttonBag.onClick.AddListener(ShowBag);
        buttonTask.onClick.AddListener(ShowTask);
        buttonEquip.onClick.AddListener(ShowEquip);
        buttonStatus.onClick.AddListener(ShowStatus);
        buttonForging.onClick.AddListener(ShowForging);
    }
    public void ShowBag()
    {
        isBag = !isBag;
        if (isBag)
        {
            ShowPage<BagPanel>();
        }
        else
        {
            ClosePage<BagPanel>();
        }
    }
    public void ShowEquip()
    {
        isStatus = !isStatus;
        if (isStatus)
        {
            ShowPage<EquipPanel>();
        }
        else
        {
            ClosePage<EquipPanel>();
        }
    }
    public void ShowStatus()
    {
        isEquip = !isEquip;
        if (isEquip)
        {
            ShowPage<StatusPanel>();
        }
        else
        {
            ClosePage<StatusPanel>();
        }
    }
    public void ShowForging()
    {
        isForging = !isForging;
        if (isForging)
        {
            ShowPage<ForgingPanel>();
        }
        else
        {
            ClosePage<ForgingPanel>();
        }
    }
    public void ShowTask()
    {
        isTask = !isTask;
        
        if (isTask)
        {
            UserTask = Save.UserTask;
            ShowPage<TaskPanel>(UserTask);
        }
        else
        {
            ClosePage<TaskPanel>();
        }
    }
    public void ShowTishi(bool isOn, List<int> _itemID,string name)
    {
        buttonTishi.gameObject.SetActive(isOn);
        
        if (isOn)
        {
            if (name!= "Quest_NPC")
            {
                buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(_itemID));
            }
            else
            {
                TaskList = Save.TaskList;
                buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<TaskPanel>(TaskList));
            }
        }

        if (!isOn)
        {
            if (name != "Quest_NPC")
            {
                TTUIPage.ClosePage<ShopPanel>();
            }
            else
            {
                TTUIPage.ClosePage<TaskPanel>();
            }
            buttonTishi.onClick.RemoveAllListeners();
        }
    }
}
