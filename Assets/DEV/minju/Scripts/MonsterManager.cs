using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < 20; i++)
        {
            CreateMonster(childrenSP[i]);
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
}
