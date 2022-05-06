using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
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
    public Transform spawnPoints;
    public Transform[] childrenSP;

    // For Random Spawn without duplicate
    public int[] duplicate;

    // Start is called before the first frame update
    void Start()
    {
        // 배열 0으로 초기화
        // initiate elements as 0
        duplicate = Enumerable.Repeat<int>(0, 50).ToArray<int>();

        // 스폰포인트 전부 가져오기
        // bring every spawnpoints
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // 처음 시작 시 스폰
        // Start to Spawn monsters
        for (int i = 0; i < 30; i++)
        {   // 30마리 생성
            // Random spawn points (0 ~ 49)
            int temp = Random.Range(0, 50);
            
            // 랜덤 생성이 중복이 아닌지 확인하기
            // Check duplicate
            if (repeatRandom(temp))
            {
                // 중복이 아닐 때만 생성
                CreateMonster(childrenSP[temp]);
            }
            else
                continue;
        }
        //monsters[1].GetComponent<MonsterAI>().Damage(-20);
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
        }
    }

    bool repeatRandom(int temp)
    {
        if (duplicate[temp] == 0)
        {
            duplicate[temp]++;
            return true;
        }
        else//중복확인해서 랜덤 다시 뽑는 거 재귀함수로 다시 고쳐야함!!
        {
            int temp2 = Random.Range(0, 50);
            return repeatRandom(temp2);
        }
    }
}
