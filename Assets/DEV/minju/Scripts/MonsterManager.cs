using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // GameManager ������Ʈ �ȿ��� �����ϴ� ��ũ��Ʈ
    // This exists only in Object "GameManager"

    public MonsterIdle idle;
    public MonsterChase chase;
    public MonsterPatrol patrol;
    public MonsterAttack attack;

    // ���� �������� ���� �迭
    // Managing Monsters
    List<Monster> littleMonster = new List<Monster>();
    List<Monster> bossMonster = new List<Monster>();

    // �ִ� �ο�
    // Max num of Monsters
    int maxNumMonsters = 0; 

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
    Monster tempMon;

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

    

    // ���� ���� �Լ�
    // Create Monster
    public void CreateMonster(Transform currentLocation)
    {
        // ���� 30���� ���� ����
        // Num of Monsters should be lower than 30
        if (maxNumMonsters <= 30)
        {
            // setting default state
            tempMon.state = Monster.States.Idle;
            // setting default hp
            tempMon.hp = 50f;
            // setting default speed
            tempMon.moveSpeed = 10f;
            tempMon.turnSpeed = 10f;
            // setting default power
            tempMon.attackForce = 10f;
            // setting default seeking state
            tempMon.isFound = false;
            // setting default destination
            tempMon.destPosition = null;
            
            // ���� ����Ʈ�� ������ ���� �߰�
            // add initial monster to manager
            littleMonster.Add(tempMon);

            // ������ ���� ���� ��Ÿ����
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("Monster"), 
                currentLocation.position, Quaternion.identity) as GameObject;

            // ���� ���� ���� ����
            // Start Monster's Idle
            StartCoroutine(objMonster.GetComponent<MonsterIdle>().Idle(littleMonster[littleMonster.Count-1]));
        }
    }
}
