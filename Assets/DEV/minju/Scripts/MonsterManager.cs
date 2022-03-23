using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public MonsterIdle idle;
    public MonsterChase chase;
    public MonsterPatrol patrol;
    public MonsterAttack attack;
    public MonsterData.Monster test;

    // Start is called before the first frame update
    void Start()
    {
        //몬스터 정지 스크립트
        idle = GetComponent<MonsterIdle>();
        //몬스터 쫓기 스크립트
        chase = GetComponent<MonsterChase>();
        //몬스터 걷기 스크립트
        patrol = GetComponent<MonsterPatrol>();
        //몬스터 공격 스크립트
        attack = GetComponent<MonsterAttack>();

        //Test 생성
        test.state = MonsterData.Monster.States.Idle;

        if (test.state == MonsterData.Monster.States.Idle)
        {
            StartCoroutine(idle.Idle());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
