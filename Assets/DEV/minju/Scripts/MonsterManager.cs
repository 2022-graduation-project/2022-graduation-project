using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public MonsterIdle idle;
    public MonsterChase chase;
    public MonsterPatrol patrol;
    public MonsterAttack attack;
    

    public class Monster
    {
        //���� ����
        public enum States
        {
            Idle, Patrol, Chase, Attack
        };
        public States state;
        //���� �̸�
        public string name;
        //���� ü��
        public float hp;
        //���� �ӵ�
        public float moveSpeed, turnSpeed;
        //���� ���ݷ�
        public float attackForce;
        //�÷��̾� ã�Ҵ��� ����
        public bool isFound;
        //������ ��ġ (Patrol: random location, Chase: player location)
        public Transform destPosition;
    }
    public Monster test;

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
        test = new Monster();
        test.hp = 50;
        test.state = Monster.States.Idle;
        test.isFound = false;

        if (test.state == Monster.States.Idle)
        {
            StartCoroutine(idle.Idle());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(test.state);
    }

    // player�� monster ���� ���� �� ȣ��
    public void Damage(int scale)
    {
        //scale(-)��ŭ ���� ü���� �پ���.
        test.hp += scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            test.isFound = true;
            test.destPosition = other.transform;
            attack.player = other.GetComponent<PlayerController>();
            chase.player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            test.isFound = false;
            test.destPosition = null;
            attack.player = null;
        }
    }
}
