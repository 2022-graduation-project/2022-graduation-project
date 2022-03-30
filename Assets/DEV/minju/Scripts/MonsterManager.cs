using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
<<<<<<< HEAD
    // GameManager 오브젝트 안에만 존재하는 스크립트
    // This exists only in Object "GameManager"

    // 현재 인원
    // Max num of Monsters: 30
    int curNumMonsters = 0;

    // 모든 몬스터 관리 리스트
    // List of all monsters
    List<GameObject> monsters = new List<GameObject>();

    // 스폰포인트 배열
    // array of spawn points
    public Transform[] spawnPoints;
=======
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
>>>>>>> 688b3dd274f3b286943cc00ee332572c7129ffda

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        CreateMonster(spawnPoints[0]);

        CreateBossMonster(spawnPoints[1]);
        //monsters[1].GetComponent<MonsterAI>().Damage(-20);
=======
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
        test.state = Monster.States.Idle;
        test.isFound = false;

        if (test.state == Monster.States.Idle)
        {
            StartCoroutine(idle.Idle());
        }
>>>>>>> 688b3dd274f3b286943cc00ee332572c7129ffda
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        
    }

    // 몬스터 생성 함수
    // Create Monster
    public void CreateMonster(Transform currentLocation)
    {
        // 몬스터 30마리 이하 유지
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // 몬스터 현재 인원 update
            // update current num of Monsters
            curNumMonsters++;

            // 몬스터 씬에 생성하기
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("Monster"), 
                currentLocation.position, Quaternion.identity) as GameObject;

            // 몬스터 관리 리스트에 추가하기
            // add monster object in list
            monsters.Add(objMonster);

            // 몬스터 스크립트의 몬스터 고유번호를 리스트의 index번호로 지정
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);
        }
    }

    // 몬스터 삭제 함수
    // Delete Monster
    public void DeleteMonster(int indexOfMonster)
    {
        // 씬에서 해당 몬스터 오브젝트 삭제
        Destroy(monsters[indexOfMonster]);

        // 리스트에서 해당 몬스터 삭제
        monsters.RemoveAt(indexOfMonster);

        // 현재 인원 수 Update
        // update current num of Monsters
        curNumMonsters--;
    }

    // 보스 몬스터 생성 함수
    // Create Boss Monster
    public void CreateBossMonster(Transform currentLocation)
    {
        // 몬스터 30마리 이하 유지
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // 몬스터 현재 인원 update
            // update current num of Monsters
            curNumMonsters++;

            // 몬스터 씬에 생성하기
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("BossMonster"),
                currentLocation.position, Quaternion.identity) as GameObject;

            // 몬스터 관리 리스트에 추가하기
            // add monster object in list
            monsters.Add(objMonster);

            // 몬스터 스크립트의 몬스터 고유번호를 리스트의 index번호로 지정
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);

            // 몬스터 스크립트의 보스 몬스터 여부 저장
            // make monster script remember it's boss
            objMonster.GetComponent<MonsterAI>().isBossMonster
                = true;
=======
        //print(test.state);
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
>>>>>>> 688b3dd274f3b286943cc00ee332572c7129ffda
        }
    }
}
