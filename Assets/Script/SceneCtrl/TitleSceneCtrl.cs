using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleSceneCtrl : MonoBehaviour {


    public Camera cam;
    public Transform targetPoint;
	// Use this for initialization
	void Start () {
        //第一个值是起点，第二个值是终点。第三个值是速度
        //cube.position = Vector3.MoveTowards(Vector3.zero, Vector3.forward * 10, 10);
        cam.transform.DOMove(targetPoint.position, 8);
        TTUIPage.ShowPage<TitlePanel>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.anyKeyDown&&Time.time>5)
        //{
        //    SceneManager.LoadScene("My Character Creation");
        //}
	}
}
