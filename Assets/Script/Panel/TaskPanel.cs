using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class TaskPanel : TTUIPage {

    private Transform content;
    private GameObject  taskItem;
    public TaskPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/TaskPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        content = transform.Find("Scroll View/Viewport/Content");
        taskItem = Resources.Load<GameObject>("UIPrefab/TaskItem");

        List<TaskModel> _itemID = (List<TaskModel>)data;
        for (int i = 0; i < _itemID.Count; i++)
        {
            GameObject Grid = GameObject.Instantiate(taskItem);
            Grid.transform.SetParent(content);
            Grid.transform.GetComponent<TaskItem>().Init(_itemID[i]);
        }
    }
    public override void Refresh()
    {
        base.Refresh();
    }
    public override void Hide()
    {
        base.Hide();
        Clear();
    }
    void Clear()
    {
        int num = content.childCount;
        for (int i = 0; i < num; i++)
        {
            GameObject.Destroy(content.GetChild(0));
        }
        GameObject.Destroy(gameObject);
    }
}
