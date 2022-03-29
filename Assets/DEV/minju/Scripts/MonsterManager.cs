using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // GameManager 오브젝트 안에만 존재하는 스크립트
    // This exists only in Object "GameManager"

    public MonsterIdle idle;
    public MonsterChase chase;
    public MonsterPatrol patrol;
    public MonsterAttack attack;

    // 몬스터 여러마리 관리 배열
    // Managing Monsters
    List<Monster> littleMonster = new List<Monster>();
    List<Monster> bossMonster = new List<Monster>();

    // 최대 인원
    // Max num of Monsters
    int maxNumMonsters = 0; 

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
    Monster tempMon;

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

    

    // 몬스터 생성 함수
    // Create Monster
    public void CreateMonster(Transform currentLocation)
    {
        // 몬스터 30마리 이하 유지
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
            
            // 몬스터 리스트에 생성한 몬스터 추가
            // add initial monster to manager
            littleMonster.Add(tempMon);

            // 생성한 몬스터 씬에 나타내기
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("Monster"), 
                currentLocation.position, Quaternion.identity) as GameObject;

            // 몬스터 역할 수행 시작
            // Start Monster's Idle
            StartCoroutine(objMonster.GetComponent<MonsterIdle>().Idle(littleMonster[littleMonster.Count-1]));
        }
    }
}
