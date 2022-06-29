using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGhost : MonsterController
{
    public GameObject prefab;
    // Start is called before the first frame update
    override public void Start()
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

            // ��Ʈ ���� �⺻�� ó�� �����ϱ�
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

        // ���� �ִϸ�����
        // Monster Animator
        anim = GetComponent<Animator>();
        // ���� �����ۿ�
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // ���� �Ŵ��� ��ũ��Ʈ ã��
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
