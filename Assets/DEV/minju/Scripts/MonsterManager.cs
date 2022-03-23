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
        //���� ���� ��ũ��Ʈ
        idle = GetComponent<MonsterIdle>();
        //���� �ѱ� ��ũ��Ʈ
        chase = GetComponent<MonsterChase>();
        //���� �ȱ� ��ũ��Ʈ
        patrol = GetComponent<MonsterPatrol>();
        //���� ���� ��ũ��Ʈ
        attack = GetComponent<MonsterAttack>();

        //Test ����
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
