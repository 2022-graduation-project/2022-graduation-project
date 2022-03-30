using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
<<<<<<< HEAD
    // GameManager ������Ʈ �ȿ��� �����ϴ� ��ũ��Ʈ
    // This exists only in Object "GameManager"

    // ���� �ο�
    // Max num of Monsters: 30
    int curNumMonsters = 0;

    // ��� ���� ���� ����Ʈ
    // List of all monsters
    List<GameObject> monsters = new List<GameObject>();

    // ��������Ʈ �迭
    // array of spawn points
    public Transform[] spawnPoints;
=======
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
>>>>>>> 688b3dd274f3b286943cc00ee332572c7129ffda

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        CreateMonster(spawnPoints[0]);

        CreateBossMonster(spawnPoints[1]);
        //monsters[1].GetComponent<MonsterAI>().Damage(-20);
=======
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

    // ���� ���� �Լ�
    // Create Monster
    public void CreateMonster(Transform currentLocation)
    {
        // ���� 30���� ���� ����
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // ���� ���� �ο� update
            // update current num of Monsters
            curNumMonsters++;

            // ���� ���� �����ϱ�
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("Monster"), 
                currentLocation.position, Quaternion.identity) as GameObject;

            // ���� ���� ����Ʈ�� �߰��ϱ�
            // add monster object in list
            monsters.Add(objMonster);

            // ���� ��ũ��Ʈ�� ���� ������ȣ�� ����Ʈ�� index��ȣ�� ����
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);
        }
    }

    // ���� ���� �Լ�
    // Delete Monster
    public void DeleteMonster(int indexOfMonster)
    {
        // ������ �ش� ���� ������Ʈ ����
        Destroy(monsters[indexOfMonster]);

        // ����Ʈ���� �ش� ���� ����
        monsters.RemoveAt(indexOfMonster);

        // ���� �ο� �� Update
        // update current num of Monsters
        curNumMonsters--;
    }

    // ���� ���� ���� �Լ�
    // Create Boss Monster
    public void CreateBossMonster(Transform currentLocation)
    {
        // ���� 30���� ���� ����
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // ���� ���� �ο� update
            // update current num of Monsters
            curNumMonsters++;

            // ���� ���� �����ϱ�
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load("BossMonster"),
                currentLocation.position, Quaternion.identity) as GameObject;

            // ���� ���� ����Ʈ�� �߰��ϱ�
            // add monster object in list
            monsters.Add(objMonster);

            // ���� ��ũ��Ʈ�� ���� ������ȣ�� ����Ʈ�� index��ȣ�� ����
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);

            // ���� ��ũ��Ʈ�� ���� ���� ���� ����
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
