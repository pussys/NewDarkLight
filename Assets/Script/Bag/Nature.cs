using UnityEngine;
using System.Collections;
/// <summary>
/// 属性
/// </summary>
public class Nature : MonoBehaviour {
    public static Nature Instance;
    public int Hp, MaxHp, Attack, Speed;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        AssigNature();// 给属性赋值
    }
    /// <summary>
    /// 给属性赋值  assig分配，任务，作业，功课
    /// </summary>
    void AssigNature()
    {    
      //  for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        //{
        //    Hp = Save.SaveUser[0].Hp;
        //    MaxHp = Save.SaveUser[0].MaxHp;
        //    Attack = Save.SaveUser[0].Attack;
        //    Speed = Save.SaveUser[0].Speed;
        //}
    }
    /// <summary>
    /// 吃药的方法
    /// 使用物品后 属性改变
    /// </summary>
	public  void Eat()
    {
       // for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        //{
        //    Save.SaveUser[0].Hp = Hp ;
        //    Save.SaveUser[0].MaxHp= MaxHp;
        //    Save.SaveUser[0].Attack = Attack;
        //    Save.SaveUser[0].Speed= Speed;
        //}
    }
}
