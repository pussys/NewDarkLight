using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rays : MonoBehaviour {
    CharacterController myCharacterController;
    public float moveV, moveH;
    public float rH, rV;
    public float spd = 5;
    Animator myAnimator;

    public GameObject effect;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        rH = Input.GetAxisRaw("Horizontal");
        rV = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveH, 0, moveV);

        myAnimator.SetFloat("speed", movement.magnitude);
        
        //动画切换发生在speed>0.1，如果位移的动画播放不匹配，请检查动画参数speed的过渡值
        if (movement.magnitude > 0.1f)
        {
            myCharacterController.Move(-movement * spd * Time.deltaTime);
        }

        if (rH != 0 || rV != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-new Vector3(rH, 0, rV)), 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnimator.SetTrigger("skill1");

        }

    }
    void MySkill1()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Camera.main.DOShakePosition(1f);
    }
}
