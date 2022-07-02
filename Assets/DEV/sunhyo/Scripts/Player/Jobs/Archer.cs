﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Archer : PlayerController
{
    public Button btn;

    public GameObject bulletFactory;    // 총알 생산할 공장
    public GameObject firePosition; // 총구


    void Start()
    {
        btn.onClick.AddListener(Shot);
    }


    public void Shot()
    {
    }

    public override void UseSkill()
    {
        animator.SetTrigger("doShot");

        GameObject bullet = Instantiate(bulletFactory); // 총알 생성
        bullet.transform.position = firePosition.transform.position;    // 총알 발사 (총알을 총구 위치로)


        //StartCoroutine(Shot());
    }
}