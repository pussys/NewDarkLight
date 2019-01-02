using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Loading : TTUIPage
{
    public Slider slider;
    public Text sliderText;
    private string Scenename = GameCtrl.Instance.nextSceneName;

    public Loading() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = ("UIPrefab/Loading");
        
    }
    public override void Awake(GameObject go)
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        sliderText = transform.Find("Text").GetComponent<Text>();
        
    }

    public override void Refresh()
    {
        
    }
    public void Add1()
    {
        if (slider.value != 100)
        {
            System.Threading.Thread.Sleep(100);
            slider.value += 5*Time.deltaTime;
            
            sliderText.text = (slider.value / slider.maxValue).ToString() + "%";
            if (slider.value >= 100)
            {
                SceneManager.LoadScene(Scenename);
            }
        }
    }
}
