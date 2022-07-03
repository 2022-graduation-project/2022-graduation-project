using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkeleton : MonsterController
{
    public GameObject prefab;
    // Start is called before the first frame update
    override public void Start()
    {
        hpBarPrefab = prefab;
        monsterData = DataManager.instance.LoadJsonFile
                      <Dictionary<string, MonsterData>>
                      (Application.dataPath + "/MAIN/Data", "skeleton")
                      ["002_skeleton"];
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
            monsterData.name = "Skeleton";
            // setting default hp
            monsterData.maxHp = 70f;
            monsterData.curHp = 70f;
            // setting default speed
            monsterData.moveSpeed = 100f;
            monsterData.turnSpeed = 0.1f;
            // setting default power
            monsterData.attackForce = 6f;
            // setting default seeking state
            monsterData.isFound = false;
            // setting default destination
            monsterData.destPosition = null;
            // attack available distance
            monsterData.distance = 2f;
            */
        }
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
