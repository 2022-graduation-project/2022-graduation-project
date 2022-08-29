using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Transform[] points;  // 몬스터 스폰 포인트 지정한 배열
    public GameObject models; // 몬스터 모델들 포함하고 있는 빈 게임오브젝트
    public NormalMonster prefab_monster;   // 몬스터 프리펩 할당할 변수

    public int monsterCount;  // 현재 생성된 몬스터 수
    public int maxMonster;  // 몬스터 최대 생성 가능 수
    public float createTime;    // 몬스터 발생 주기

    public bool isGameOver = false;

    public int pointIdx, monsterIdx;




    void Start()
    {
        points = GameObject.Find("Spawn Points").GetComponentsInChildren<Transform>();
        models = GameObject.Find("Normal Monster");
        maxMonster = 5;
        createTime = 2f;
        
        // 현재 생성된 몬스터 개수 산출
        monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;
        if (monsterCount < maxMonster)
            CreateMonster();
    }

    public void CreateMonster()
    {
        while (monsterCount < maxMonster)
        {
        // 위치 및 몬스터 종류 불규칙적으로 산출
        pointIdx = Random.Range(1, points.Length);
        monsterIdx = Random.Range(0, 3);

        // 몬스터의 동적 생성
        prefab_monster = Instantiate(models.transform.GetChild(monsterIdx).GetComponent<NormalMonster>(), points[pointIdx].position, points[pointIdx].rotation);
        prefab_monster.gameObject.SetActive(true);
        monsterCount++;

        }
    }
}
