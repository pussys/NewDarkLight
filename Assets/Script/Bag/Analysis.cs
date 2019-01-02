using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour {

	void Awake () {
        // 用户数据解析
        UserAnalysis();
        // 物品数据解析
        GoodsAnalysis();
        EquipAnalysis();
        ForgingAnalysis();
        TaskAnalysis();
        UserTaskAnalysis();
        //InvokeRepeating("UpdateUser", 1, 1);
    }
    private void UpdateUser()
    {
        Save.UpdateUser();
    }
    /// <summary>
    /// 用户数据解析
    /// </summary>
	void UserAnalysis()
    {
        TextAsset u = Resources.Load("Setting/UserJson") as TextAsset;
        if (!u)
        {
            return;
        }
        Save.SaveUser = JsonConvert.DeserializeObject<List<UserModel>>(u.text);
        print(u.text);
    }

    /// <summary>
    /// 物品数据解析
    /// </summary>
    void GoodsAnalysis()
    {
        TextAsset g = Resources.Load("Setting/GoodsList") as TextAsset;
        if (!g)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.SaveGoods = JsonConvert.DeserializeObject<List<GoodsModel>>(g.text);
        Debug.Log("GoodsAnalysis");
        print(g.text);
    }

    void EquipAnalysis()
    {
        TextAsset e = Resources.Load("Setting/EquipList") as TextAsset;
        if (!e)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.EquipList = JsonConvert.DeserializeObject<List<EquipModel>>(e.text);
        Debug.Log("EquipAnalysis");
        print(e.text);
    }
    void ForgingAnalysis()
    {
        TextAsset e = Resources.Load("Setting/ForgingList") as TextAsset;
        if (!e)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.ForgingList = JsonConvert.DeserializeObject<List<Forging>>(e.text);
        Debug.Log("ForgingAnalysis");
        print(e.text);
    }
    void TaskAnalysis()
    {
        TextAsset e = Resources.Load("Setting/TaskList") as TextAsset;
        if (!e)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.TaskList = JsonConvert.DeserializeObject<List<TaskModel>>(e.text);
        Debug.Log("ForgingAnalysis");
        print(e.text);
    }
    void UserTaskAnalysis()
    {
        TextAsset e = Resources.Load("Setting/UserTask") as TextAsset;
        if (!e)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.UserTask = JsonConvert.DeserializeObject<List<TaskModel>>(e.text);
        Debug.Log("ForgingAnalysis");
        print(e.text);
    }
}
