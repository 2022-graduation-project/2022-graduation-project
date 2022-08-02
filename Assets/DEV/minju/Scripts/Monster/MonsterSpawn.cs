using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Transform[] A, B, C;

    private bool[] ADeath, BDeath, CDeath;

    public Object Melee, Range, SelfDes;
    
    private Object[] monsters;

    // Start is called before the first frame update
    void Start()
    {
        monsters = new Object[3]{Melee, Range, SelfDes};
        InitialSpawn();

        // 몬스터 스폰 위치 별, 몬스터 죽은 여부 초기화
        ADeath = new bool[2]{false, false};
        BDeath = new bool[3]{false, false, false};
        CDeath = new bool[7]{false, false, false, false, false, false, false};
    }

    private void InitialSpawn()
    {
        // A01 ~ A07
        for(int i = 0; i < 7; i++)
        {
            Spawn(Melee, A[i], "A0"+(i+1).ToString(), true);
        }
            
        
        // B01 3마리 생성
        for (int i = 0; i < 3; i++)
        {
            Spawn(Melee, B[0], "B01", false);
        }

        // B02 3마리 생성
        for (int i = 0; i < 3; i++)
        {
            Spawn(Range, B[1], "B02", false);
        }

        // B03 3마리 생성
        for (int i = 0; i < 3; i++)
        {
            Spawn(SelfDes, B[2], "B03", false);
        }

        // C01 ~ C07
        for(int i = 0; i < 7; i++)
        {
            Spawn(monsters[Random.Range(0,3)], C[i], "C0"+(i+1).ToString(), false);
        }
    }

    // Respawn == Take time and then Spawn
    private IEnumerator Respawn(Object kindOfMonster, Transform spawnPoint, float time, string location, bool isALoc)
    {
        yield return new WaitForSeconds(time);
        Spawn(kindOfMonster, spawnPoint, location, isALoc);
        yield return null;
    }

    private void Spawn(Object kindOfMonster, Transform spawnPoint, string location, bool isALoc)
    {
        // 게임 오브젝트 생성
        GameObject monster = Instantiate(kindOfMonster, spawnPoint.position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))) as GameObject;
        
        NormalMonster monController = monster.GetComponent<NormalMonster>();
        
        // 몬스터 객체에게 스폰위치 저장시키기
        monController.spawnLoc = location;

        // A구역 몬스터들만 체력 약하게 설정
        if(isALoc)
        {
            // Intro Monster HP setting for Beginner
            monController.monsterData.maxHp = 80;
            monController.monsterData.curHp = 80;
        }
    }

    // 몬스터 객체가 죽을 때, 본인의 위치에서 리스폰
    public void Decode(string spawnLoc)
    {
        switch (spawnLoc)
        {
            case "A01": StartCoroutine(Respawn(Melee, A[0], 180f, "A01", true)); break;
            case "A07": StartCoroutine(Respawn(Melee, A[6], 180f, "A07", true)); break;

            case "B01": StartCoroutine(Respawn(monsters[0], B[0], 60f, "B01", false)); break;
            case "B02": StartCoroutine(Respawn(monsters[1], B[1], 90f, "B02", false)); break;
            case "B03": StartCoroutine(Respawn(monsters[2], B[2], 120f, "B03", false)); break;

            case "C01": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[0], 60f, "C01", false)); break;
            case "C02": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[1], 60f, "C02", false)); break;
            case "C03": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[2], 60f, "C03", false)); break;
            case "C04": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[3], 60f, "C04", false)); break;
            case "C05": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[4], 60f, "C05", false)); break;
            case "C06": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[5], 60f, "C06", false)); break;
            case "C07": StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[6], 60f, "C07", false)); break;
        }
    }
}
