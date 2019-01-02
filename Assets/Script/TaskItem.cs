using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{

    private Button acceptTask, finishTask;
    private TaskModel task;
    Item item;
    public static event Action<int> OnItemSelected;
    public void Init(TaskModel _item)
    {
        task = _item;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        if (task.Task_Type==Task_Type.collect)
        {
            item = DataMgr.GetInstance().GetItemByID(task.id);
            Item Ritem= DataMgr.GetInstance().GetItemByID(task.Reward_ID);
            transform.Find("TaskName").GetComponent<Text>().text = "任务名：收集物品";
            transform.Find("TaskDescribe").GetComponent<Text>().text= "任务描述：收集" + item.item_Name + "*" + task.num;
            transform.Find("TaskProgress/Test").GetComponent<Text>().text = item.item_Name + "  " + "0" + "/" + task.num;
            transform.Find("TaskReward/Text").GetComponent<Text>().text="奖励"+Ritem.item_Name+"*"+task.Reward_num;
            transform.Find("FinishTask/Text").GetComponent<Text>().text = "未完成";
        }
        else
        {
            transform.Find("TaskName").GetComponent<Text>().text = "击杀小怪";
            //transform.Find("TaskDescribe").GetComponent<Text>().text = "击杀" + item.item_Name + "*" + task.num;
            //transform.Find("TaskProgress/Test").GetComponent<Text>();
            //transform.Find("TaskReward/Text").GetComponent<Text>().text = "奖励" + Ritem.item_Name + "*" + task.Reward_num;
        }
        switch (task.accept)
        {
            case Task_State.Accept:
                transform.Find("AcceptTask/Text").GetComponent<Text>().text = "接受任务";
                break;
            case Task_State.Renounce:
                transform.Find("AcceptTask/Text").GetComponent<Text>().text = "放弃任务";
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        acceptTask = transform.Find("AcceptTask").GetComponent<Button>();
        finishTask = transform.Find("FinishTask").GetComponent<Button>();
        acceptTask.onClick.AddListener(() =>
        {
            switch (task.accept)
            {
                case Task_State.Accept:
                    Save.Task(task.Id);
                    break;
                case Task_State.Renounce:
                    Save.RenounceTask(task.Id);
                    break;
            }
            
        });
        finishTask.onClick.AddListener(() =>
        {
            switch (task.finish)
            {
                case Task_State.Complete:
                    Save.FinishTask(task.Id);
                    break;
                case Task_State.Unfinished:
                    break;
            }
            
        });
        Debug.LogWarning("记住调用Init方法，对物品信息进行初始化");
    }
    private void Update()
    {
        GoodsModel gm = Save.SaveGoods.Find(x => x.Id == task.id);
        if (gm!=null)
        {
            transform.Find("TaskProgress/Test").GetComponent<Text>().text = item.item_Name + "  " + gm.Num + "/" + task.num;
            if (gm.Num >= task.num)
            {
                transform.Find("FinishTask/Text").GetComponent<Text>().text = "完成任务";
            }
        }
    }
}
