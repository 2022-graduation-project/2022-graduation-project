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
        //몬스터 상태
        public enum States
        {
            Idle, Patrol, Chase, Attack
        };
        public States state;
        //몬스터 이름
        public string name;
        //몬스터 체력
        public float hp;
        //몬스터 속도
        public float moveSpeed, turnSpeed;
        //몬스터 공격력
        public float attackForce;
        //플레이어 찾았는지 여부
        public bool isFound;
        //목적지 위치 (Patrol: random location, Chase: player location)
        public Transform destPosition;
    }
    public Monster test;

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

    // player가 monster 공격 했을 때 호출
    public void Damage(int scale)
    {
        //scale(-)만큼 몬스터 체력이 줄어든다.
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
