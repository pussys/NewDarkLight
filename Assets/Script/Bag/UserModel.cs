using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;



[System.Serializable]
public class UserModel {
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Speed;
    public int Defend;
    public int Hit;
}
[System.Serializable]
public class TaskModel
{
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Id;
    public Task_Type Task_Type;
    public int id;
    public int num;
    public int Reward_ID;
    public int Reward_num;
    public Task_State accept;
    public Task_State finish;
}
[System.Serializable]
public class Forging
{
    public int id;
    public int ID;
}
[System.Serializable]
public class GoodsModel //商品信息
{
    public int Id;
    public int Num;//数量
}
[System.Serializable]
public class EquipModel //商品信息
{
    public int Id;
    public int Value;//值
    public Equipment_Type Equipment_Type;
}

public class Save
{
    private static List<GoodsModel> GoodsList;//背包数据
    private static List<EquipModel> equipList;//武器数据
    private static List<UserModel> saveUser;//属性数据
    private static List<Forging> forginglist;//锻造数据
    private static List<TaskModel> taskList;//任务大厅数据
    private static List<TaskModel> userList;//角色任务数据

    /// <summary>
    /// 任务数据属性
    /// </summary>
    public static List<TaskModel> TaskList
    {
        get { return taskList; }
        set { taskList = value; }
    }
    /// <summary>
    /// 角色任务数据
    /// </summary>
    public static List<TaskModel> UserTask
    {
        get { return userList; }
        set { userList = value; }
    }
    /// <summary>
    /// 锻造数据属性
    /// </summary>
    public static List<Forging> ForgingList
    {
        get { return forginglist; }
        set { forginglist = value; }
    }
    /// <summary>
    /// 人物数据属性
    /// </summary>
    public static List<UserModel> SaveUser
    {
        get { return saveUser; }
        set { saveUser = value; }
    }
    /// <summary>
    /// 武器数据属性
    /// </summary>
    public static List<EquipModel> EquipList
    {
        get { return equipList; }
        set { equipList = value; }
    }
    /// <summary>
    /// 背包数据属性
    /// </summary>
    public static List <GoodsModel> SaveGoods
    {
        get { return GoodsList; }
        set { GoodsList = value; }
    }
    /// <summary>
    /// 购买物品，背包数据更新
    /// </summary>
    /// <param name="_item">要保存的物品</param>
    public static void BuyItem(Item _item)
    {
        if (GoodsList == null)
        {
            GoodsList = new List<GoodsModel>();
        }
        GoodsModel gm = GoodsList.Find(x => x.Id == _item.item_ID);
        if (gm!=null)
        {
            gm.Num += 1;
        }
        else
        {
            GoodsList.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
        }
        SaveGood();
    }
    /// <summary>
    /// 使用物品时装备栏更新和人物属性更新
    /// </summary>
    /// <param name="_item">要使用的物品</param>
    public static void UseItem(Item _item)
    {
        if (equipList == null)
        {
            equipList = new List<EquipModel>();
        }
        
        GoodsModel gm = GoodsList.Find(x => x.Id == _item.item_ID);//在背包找到这个物品
        if (gm.Num <= 1)//使用了这个物品，背包要移除这个物品
        {
            GoodsList.Remove(gm);//如果背包栏这个物品数据为1则移除这个物品
        }
        else
        {
            gm.Num -= 1;//如果这个物品数据大于1则数量减一
        }
        if (_item.equipment_Type != Equipment_Type.Null)//判断这个物品是不是装备
        {
            //判断背包栏是不是有这个类型的武器，类型是穿戴在武器穿戴的位置
            EquipModel em = EquipList.Find(x => x.Equipment_Type == _item.equipment_Type);
            if (em != null)//如果这个位置已经有物体就要替换
            {
                em.Value += 1;//如果这个位置有物体了，让这个物体的数量加1
                if (em.Value > 1)//看这个位置的物体数量是不是1，如果大于一则移除这个物体
                {
                    EquipList.Remove(em);//在武器数据中移除这个数据
                    Item item = DataMgr.GetInstance().GetItemByID(em.Id);//在数据库中找到移除的这个物体
                    BuyItem(item);//将他保存回背包数据中
                }
            }
            //给背包数据添加这个新的要使用的物品，达到替换的效果
            equipList.Add(new EquipModel() { Id = _item.item_ID, Equipment_Type = _item.equipment_Type, Value = 1 });
        }
        else
        {
            if (saveUser[0].Hp<saveUser[0].MaxHp)
            {
                saveUser[0].Hp += _item.hp;
            }
        }
        SaveEquip();
    }
    /// <summary>
    /// 卸下装备更新数据
    /// </summary>
    /// <param name="_item"></param>
    public static void UnloadEquip(Item _item)
    {
        EquipModel gm = EquipList.Find(x => x.Id == _item.item_ID);
        EquipList.Remove(gm);
        Item item = DataMgr.GetInstance().GetItemByID(gm.Id);
        BuyItem(item);
        UpdateUser();
        SaveEquip();
    }
    /// <summary>
    /// 锻造装备更新数据
    /// </summary>
    /// <param name="_id"></param>
    public static void ForgingItem(int _id)
    {
        GoodsModel gm = GoodsList.Find(x => x.Id == _id);//在背包找到这个物品
        if (gm.Num>2)
        {
            gm.Num -= 2;
            Forging fg = ForgingList.Find(x => x.id == _id);
            Item item = DataMgr.GetInstance().GetItemByID(fg.ID);
            BuyItem(item);
        }
        
    }
    /// <summary>
    /// 使用物品后人物属性更新
    /// </summary>
    public static void UpdateUser()
    {
        if (saveUser == null)
        {
            saveUser = new List<UserModel>();
        }
        saveUser[1].Attack = 0;
        saveUser[1].Defend = 0;
        saveUser[1].MaxHp = 0;
        saveUser[1].Speed = 0;
        saveUser[1].Hit = 0;
        foreach (EquipModel em in equipList)
        {
            Item item = DataMgr.GetInstance().GetItemByID(em.Id);
            saveUser[1].Attack += item.atk;
            saveUser[1].Defend += item.def;
            saveUser[1].MaxHp += item.hp;
            saveUser[1].Speed += item.spd;
            saveUser[1].Hit += item.hit;
        }
        if (saveUser[0].Hp> saveUser[0].MaxHp)
        {
            saveUser[0].Hp = saveUser[0].MaxHp;
        }
    }
    public static void Task(int taskID)
    {
        if (userList == null)
        {
            userList = new List<TaskModel>();
        }
        TaskModel tm = taskList.Find(x => x.Id == taskID);
        TaskModel tm1 = userList.Find(x => x.Id == taskID);
        tm.accept = Task_State.Renounce;
        tm.finish = Task_State.Unfinished;
        if (tm1==null)
        {
            UserTask.Add(tm);
        }
        SaveTask();
    }
    public static void FinishTask(int taskID)
    {
        TaskModel tm = userList.Find(x => x.Id == taskID);
        Item item = DataMgr.GetInstance().GetItemByID(tm.Reward_ID);
        GoodsModel gm = GoodsList.Find(x => x.Id == tm.id);
        gm.Num -= tm.num;
        BuyItem(item);
        UserTask.Remove(tm);
        SaveTask();
    }
    public static void RenounceTask(int taskID)
    {
        TaskModel tm = userList.Find(x => x.Id == taskID);
        UserTask.Remove(tm);
        SaveTask();
    }
    public static void SaveGood()
    {
        string path = Application.dataPath + @"/Resources/Setting/GoodsList.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(GoodsList);
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }
    public static void SaveEquip()
    {
        string path1 = Application.dataPath + @"/Resources/Setting/EquipList.json";
        using (StreamWriter sw1 = new StreamWriter(path1))
        {
            string json1 = JsonConvert.SerializeObject(equipList);
            sw1.Write(json1);
        }
        AssetDatabase.Refresh();
    }
    public static void SavePer()
    {
        string path2 = Application.dataPath + @"/Resources/Setting/UserJson.json";
        using (StreamWriter sw2 = new StreamWriter(path2))
        {
            string json2 = JsonConvert.SerializeObject(saveUser);
            sw2.Write(json2);
        }
        AssetDatabase.Refresh();
    }
    public static void SaveTask()
    {
        string path2 = Application.dataPath + @"/Resources/Setting/UserTask.json";
        using (StreamWriter sw2 = new StreamWriter(path2))
        {
            string json2 = JsonConvert.SerializeObject(userList);
            sw2.Write(json2);
        }
        AssetDatabase.Refresh();
    }
}
