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
            Spawn(Melee, A[i], "A0"+i.ToString(), true);
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
            Spawn(monsters[Random.Range(0,3)], C[i], "C0"+i.ToString(), false);
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

    // 몬스터 객체가 죽을 때, 본인의 위치에 죽음 플래그 켜기
    public void Decode(string spawnLoc)
    {
        switch (spawnLoc)
        {
            case "A01": ADeath[0] = true; break;
            case "A07": ADeath[1] = true; break;

            case "B01": BDeath[0] = true; break;
            case "B02": BDeath[0] = true; break;
            case "B03": BDeath[0] = true; break;

            case "C01": CDeath[0] = true; break;
            case "C02": CDeath[1] = true; break;
            case "C03": CDeath[2] = true; break;
            case "C04": CDeath[3] = true; break;
            case "C05": CDeath[4] = true; break;
            case "C06": CDeath[5] = true; break;
            case "C07": CDeath[6] = true; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // A구역 전체 죽음 플래그 확인
        for (int i=0; i<2; i++)
        {
            bool isDead = ADeath[i];
            if (isDead)
            {
                // 3분 대기 후 리스폰
                StartCoroutine(Respawn(Melee, A[i], 180f, "A0"+i.ToString(), true));
                // 리스폰 한 번만 호출하기 위해, 바로 기본값으로 돌아가야함.
                ADeath[i] = false;
            }
        }

        // B구역 전체 죽음 플래그 확인
        for (int i=0; i<3; i++)
        {
            bool isDead = BDeath[i];
            if (isDead)
            {
                // 몬스터 재생시간 B01: 1분, B02: 1분 30초, B03: 2분
                float time = 0f;
                switch(i)
                {
                    case 0:
                        time = 60f;
                        break;
                    case 1:
                        time = 90f;
                        break;
                    case 2:
                        time = 120f;
                        break;
                }

                StartCoroutine(Respawn(monsters[i], B[i], time, "B0"+i.ToString(), false));
                BDeath[i] = false;
            }
        }

        // C구역 전체 죽음 플래그 확인
        for (int i=0; i<7; i++)
        {
            bool isDead = CDeath[i];
            if (isDead)
            {
                // 몬스터 랜덤 리스폰
                StartCoroutine(Respawn(monsters[Random.Range(0,3)], C[i], 60f, "C0"+i.ToString(), false));
                CDeath[i] = false;
            }
        }
    }
}
