using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGhost : MonsterController
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        hpBarPrefab = prefab;
        monsterData = DataManager.instance.LoadJsonFile
                      <Dictionary<string, MonsterData>>
                      (Application.dataPath + "/MAIN/Data", "ghost")
                      ["000_ghost"];
        // setting default state
        monsterData.state = MonsterData.States.Idle;
        // setting default seeking state
        monsterData.isFound = false;
        // setting default destination
        monsterData.destPosition = null;
        {/*

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
            monsterData.turnSpeed = 0.1f;
            // setting default power
            monsterData.attackForce = 10f;
            // setting default seeking state
            monsterData.isFound = false;
            // setting default destination
            monsterData.destPosition = null;
            // attack available distance
            monsterData.distance = 3f;
            */
        }

        SetHpBar();
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


        UpdateHpBar(monsterData.curHp);

        // start with default state
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
