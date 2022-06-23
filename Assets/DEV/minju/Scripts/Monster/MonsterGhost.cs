using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGhost : MonsterController
{
    private MonsterController monCtrl = new MonsterController();
    // Start is called before the first frame update
    void Start()
    {
        // MonsterData Json file Load 필요
        {

            // 고스트 몬스터 기본값 처음 설정하기
            // setting default values of Ghost
            monsterData = new MonsterData();
            // setting default state
            monsterData.state = MonsterData.States.Idle;
            // setting name
            monsterData.name = "Ghost";
            // setting default hp
            monsterData.maxHp = 100f;
            monsterData.curHp = 100f;
            // setting default speed
            monsterData.moveSpeed = 80f;
            monsterData.turnSpeed = 2f;
            // setting default power
            monsterData.attackForce = 10f;
            // setting default seeking state
            monsterData.isFound = false;
            // setting default destination
            monsterData.destPosition = null;
            // attack available distance
            monsterData.distance = 3f;
        }


        monCtrl.SetHpBar();
        hpBar.SetActive(false);

        // 몬스터 애니메이터
        // Monster Animator
        anim = GetComponent<Animator>();
        // 몬스터 물리작용
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // 몬스터 매니저 스크립트 찾기
        // get MonsterManager script from GamaManager
        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();


        monCtrl.UpdateHpBar(monsterData.curHp);

        // start with default state
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
