using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        CreateMonster(spawnPoints[0]);

        CreateBossMonster(spawnPoints[1]);
        //monsters[1].GetComponent<MonsterAI>().Damage(-20);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
