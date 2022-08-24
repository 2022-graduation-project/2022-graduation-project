using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] points;  // 몬스터 스폰 포인트 지정한 배열

    public string[] monsters = { "MeleeMonster", "RangeMonster", "SelfDesMonster" };   // 몬스터 종류 ???
    public NormalMonster prefab_monster;   // 몬스터 프리펩 할당할 변수 (???)

    public float createTime;    // 몬스터 발생 주기
    public int maxMonster = 5;  // 몬스터 최대 발생 개수
    public bool isGameOver = false; // 게임 종료 여부


    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }
    }

    IEnumerator CreateMonster()
    {
        // 게임 종료 시까지 무한 루프
        while (!isGameOver)
        {
            // 현재 생성된 몬스터 개수 산출
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < maxMonster)
            {
                // 몬스터의 생성 주기 시간만큼 대기
                yield return new WaitForSeconds(createTime);

                // 불규칙적인 위치 산출
                int index = Random.Range(0, points.Length);
                int monsterKind = Random.Range(0, monsters.Length);

                prefab_monster = GameObject.Find(monsters[monsterKind]).GetComponent<NormalMonster>();

                // 몬스터의 동적 생성
                Instantiate(prefab_monster, points[index].position, points[index].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}
