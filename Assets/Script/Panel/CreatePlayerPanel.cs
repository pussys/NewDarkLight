using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CreatePlayerPanel : TTUIPage
{
    public Button buttonPre, buttonNext, buttonRandom, buttonOK;//左右选择按钮，随机按钮，确定按钮
    public InputField inputFieldName;//名字输入框

    public GameObject[] hero;  //your hero
    [HideInInspector]
    public int indexHero = 0;  //index select hero

    public string[] xings = { "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊", "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐", "钟离", "宇文", "长孙", "慕容", "鲜于", "闾丘", "司徒", "司空", "丌官", "司寇", "仉督", "子车", "颛孙", "端木", "巫马", "公西", "漆雕", "乐正", "壤驷", "公良", "拓跋", "夹谷", "宰父", "谷梁", "晋楚", "闫法", "汝鄢", "涂钦", "段干", "百里", "东郭", "南门", "呼延", "归海", "羊舌", "微生", "岳帅", "缑亢", "况郈", "有琴", "梁丘", "左丘", "东门", "西门", "商牟", "佘佴", "伯赏", "南宫", "墨哈", "谯笪", "年爱", "阳佟", "第五", "言福" };
    public string[] names = { "蔗", "蔼", "熙 ", "噩", "橱", "橙", "瓢", "蟥", "霍", "霎", "辙", "冀", " 踱", "蹂", "蟆", "螃", "螟", "噪", "鹦", "黔", "穆", "篡", "篷", "篙", "篱", "儒", "膳", "鲸", "瘾", "瘸", "糙", "燎", "濒", "憾", "懈", "窿", "缰", "壕", "藐", "檬", "檐", "檩", "檀", "礁", "磷", "了", "瞬", "瞳", "瞪", "曙", "蹋", "蟋", "蟀", "嚎", "赡", "镣", "魏", "簇", "儡", "徽", "爵", "朦", "臊", "鳄", "糜", "癌", "懦", "豁", "臀", "藕", "藤", "瞻", "嚣", "鳍", "癞", "瀑", "襟", "璧", "戳", "攒", "孽", "蘑", "藻", "鳖", "蹭", "蹬", "簸", "蟹", "靡", "癣", "羹", "鬓", "攘", "蠕", "巍", "鳞", "糯", "譬", "霹", "躏", "髓", "蘸", "镶", "瓤", "矗"};

    public void GetRandomName()
    {
        string xing = xings[Random.Range(0, xings.Length)];
        string ming = names[Random.Range(0, names.Length)];
        inputFieldName.text = xing + ming;
    }

    private GameObject[] heroInstance;
    /// <summary>
    /// 构造函数
    /// </summary>
    public CreatePlayerPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/CreatePlayerPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //找到四个Button按钮和文本框
        buttonPre = transform.Find("ButtonPre").GetComponent<Button>();
        buttonNext = transform.Find("ButtonNext").GetComponent<Button>();
        buttonRandom = transform.Find("ButtonRandom").GetComponent<Button>();
        buttonOK = transform.Find("ButtonOK").GetComponent<Button>();
        inputFieldName = transform.Find("InputField").GetComponent<InputField>();

        hero = Resources.LoadAll<GameObject>("Player/HeroPreview");
        heroInstance = new GameObject[hero.Length]; //add array size equal hero size
        indexHero = 0; //set default selected hero
        SpawnHero();

        if (hero.Length <= 1)
        {
            buttonNext.gameObject.SetActive(false);
            buttonPre.gameObject.SetActive(false);
        }
        //上一个和下一个按钮
        buttonNext.onClick.AddListener(() =>
        {
            indexHero++;
            if (indexHero >= heroInstance.Length)
            {
                indexHero = 0;
            }
            UpdateHero(indexHero);
        });
        buttonPre.onClick.AddListener(() =>
        {
            indexHero--;
            if (indexHero <= 0)
            {
                indexHero = heroInstance.Length-1;
            }
            UpdateHero(indexHero);
        });
        buttonRandom.onClick.AddListener(()=> {
            GetRandomName();
            if (buttonRandom.GetComponent<RectTransform>().rotation.eulerAngles.z==180)
            {
                buttonRandom.transform.DORotate(Vector3.forward * 360, 0.5f);
            }
            else
            {
                buttonRandom.transform.DORotate(Vector3.forward * 180, 0.5f);
            }
        });
        buttonOK.onClick.AddListener(ButtonOKClick);
    }
    /// <summary>
    /// 显示指定索引所对应的角色
    /// </summary>
    /// <param name="_indexHero"></param>
    public void UpdateHero(int _indexHero)
    {
        for (int i = 0; i < hero.Length; i++)
        {
            //Show only select character
            if (i == _indexHero)
            {
                heroInstance[i].SetActive(true);
            }
            else
            {
                //Hide Other Character
                heroInstance[i].SetActive(false);
            }
        }
    }
    public void SpawnHero()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            heroInstance[i] = (GameObject)GameObject.Instantiate(hero[i], new Vector3(-0.22f, -0.62f,4.62f), Quaternion.Euler(0,180,0));
        }

        UpdateHero(indexHero);
    }
    public void ButtonOKClick()
    {
        PlayerPrefs.SetString("pName", inputFieldName.text);
        PlayerPrefs.SetInt("pSelect", indexHero);
        SceneManager.LoadScene("Loading");
        GameCtrl.Instance.nextSceneName = "ceshi";
    }
}
