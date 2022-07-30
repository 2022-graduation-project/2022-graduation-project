using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : PlayerController
{
    // Skill #1 & Skill #3
    private List<GameObject> monsterList;
    RaycastHit[] hits;
    float MaxDistance = 10f;
    Vector3 offset;



    int Blast_coolDelay = 60;
    int Blast_maxCoolDelay = 60;
    int Blast_MpConsume = 20;
    float Blast_duration = 0;

    /*****************************************
     * Blast (Skill #1) 공격
     * Blast() -> Blast_DuringSkill() -> Blast_CountDelay() 
     *
     * @ param - X @ return - X @ exception - X
     * @ 쿨타임: 60초
     * @ MP 소모량: 20
     * @ 지속시간: 2초 
    ******************************************/
    override public void UseSkill()
    {
        Blast();
    }

    private void Blast()
    {
        // 쿨타임이 차지 않았을 때 또는
        // 현재 MP가 소모량보다 적을 때
        if (Blast_coolDelay < Blast_maxCoolDelay || playerManager.playerData.curMp < Blast_MpConsume)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        Blast_duration = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 플레이어 MP 소모
        ConsumeMP(Blast_MpConsume);

        // 지속시간 동안 Monster 리스트에 담고, 공격
        StartCoroutine(Blast_DuringSkill());
    }
    
    // Blast (Skill #1) 지속시간 동안 Monster 리스트에 담기
    private IEnumerator Blast_DuringSkill()
    {
        GameObject temp;
        // Blast 지속 시간: 2초
        while (Blast_duration < 2)
        {
            // 지속 시간 재기
            yield return new WaitForSeconds(1f);
            Blast_duration++;

            // RayCast에 닿는 All Monsters, 배열(hits)로 가져오기
            hits = Physics.RaycastAll(transform.position + offset, transform.forward, MaxDistance);

            if (hits.Length != 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    // hits 원소들 모두 mosterList로 넣기
                    temp = hits[i].collider.gameObject;
                    if (temp.tag == "Monster" && monsterList.Find(x => x == temp) == null)
                    {
                        monsterList.Add(temp);
                    }
                }
            }

            MonsterListDebug(monsterList);

            if (Blast_duration >= 2)
            {

                // RayCast에 닿은 몬스터 리스트에
                // 만약 몬스터가 한 마리 이상 있다면
                if (monsterList.Count != 0)
                {
                    // 모든 몬스터 동시에 Damage 주기
                    foreach (GameObject monsters in monsterList)
                    {
                        monsters.GetComponent<NormalMonster>().Damaged(playerManager.playerData.STR);
                        print("Mons Damaged by BLAST skill!");
                    }
                }

                // 쿨타임 초기화 및 초 세기
                Blast_coolDelay = 0;
                StartCoroutine(Blast_CountDelay(Blast_maxCoolDelay));

                // 다음 차례를 위해 몬스터 리스트 비워주기
                monsterList.Clear();

                yield break;
            }
        }
    }

    // Blast (Skill #1) 쿨타임 초 세기
    private IEnumerator Blast_CountDelay(int tempCoolDelay1)
    {
        while (Blast_coolDelay < tempCoolDelay1)
        {
            yield return new WaitForSeconds(1f);
            Blast_coolDelay++;

            if (Blast_coolDelay >= tempCoolDelay1)
            {
                // set as default
                Blast_coolDelay = 60;
                yield break;
            }
        }
    }


    int Effect_coolDelay = 60;
    int Effect_maxCoolDelay = 60;
    int Effect_MpConsume = 20;
    float Effect_duration = 0f;

    /*****************************************
     * Effect (Skill #2) 스킬
     * Effect() -> Effect_DuringSkill() -> Effect_CountDelay() 
     * 
     * @ param - X @ return - X @ exception - X
     * @ 쿨타임: 60초
     * @ MP 소모량: 10
     * @ 지속시간: 6초 
    ******************************************/
    override public void UseSkill2()
    {
        Effect();        
    }

    private void Effect()
    {
        // 쿨타임이 차지 않았을 때 또는
        // 현재 MP가 소모량보다 적을 때
        if (Effect_coolDelay < Effect_maxCoolDelay || playerManager.playerData.curMp < Effect_MpConsume)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        Effect_duration = 0;

        // 플레이어 MP 소모
        ConsumeMP(Effect_MpConsume);

        // Blast스킬의 쿨타임을 60->40초로 줄여줌
        Blast_maxCoolDelay = 40;

        // Blast스킬의 MP 소모량을 20->10로 줄여줌
        Blast_MpConsume = 10;

        print("Effect Skill ON");
        // 지속 시간 후에 BLAST스킬의 속성 초기화
        StartCoroutine(Effect_DuringSkill());
    }

    // Effect (Skill #2) 지속시간 후 Skill #1의 속성 초기화
    private IEnumerator Effect_DuringSkill()
    {
        while (Effect_duration < 6)
        {
            yield return new WaitForSeconds(1f);
            Effect_duration++;

            if (Effect_duration >= 6)
            {
                
                print("Effect Skill OFF");
                // Blast스킬의 쿨타임 초기화
                Blast_maxCoolDelay = 60;

                // Blast스킬의 MP 소모량 초기화
                Blast_MpConsume = 20;

                // 쿨타임 초기화 및 초 세기
                Effect_coolDelay = 0;
                StartCoroutine(Effect_CountDelay());

                yield break;
            }
        }
    }

    // Effect (Skill #2) 쿨타임 초 세기
    private IEnumerator Effect_CountDelay()
    {
        while (Effect_coolDelay < Effect_maxCoolDelay)
        {
            yield return new WaitForSeconds(1f);
            Effect_coolDelay++;

            if (Effect_coolDelay >= Effect_maxCoolDelay)
            {
                Effect_coolDelay = Effect_maxCoolDelay;
                yield break;
            }
        }
    }

    int Stun_coolDelay = 60 * 5;
    int Stun_maxCoolDelay = 60 * 5;
    int Stun_MpConsume = 30;
    int Stun_duration = 0;


    float playerTempSpeed = 0;
    /*****************************************
     * Stun (Skill #3) 스킬
     * Stun() -> Stun_DuringSkill() -> Stun_CountDelay() 
     * 
     * @ param - X @ return - X @ exception - X
     * @ 쿨타임: 300초
     * @ MP 소모량: 30
     * @ 지속시간: 2초 
    ******************************************/
    override public void UseSkill3()
    {
        Stun();
    }

    private void Stun()
    {
        // 쿨타임이 차지 않았을 때 또는
        // 현재 MP가 소모량보다 적을 때
        if (Stun_coolDelay < Stun_maxCoolDelay || playerManager.playerData.curMp < Stun_MpConsume)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        Stun_duration = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 플레이어 MP 소모
        ConsumeMP(Stun_MpConsume);

        // 플레이어 이동속도 증가
        playerTempSpeed = playerManager.playerData.moveSpeed;
        playerManager.playerData.moveSpeed = playerTempSpeed * 1.3f;

        // 지속시간 동안 Monster 리스트에 담고, 공격
        StartCoroutine(Stun_DuringSkill());
    }


    // Stun (Skill #3) 지속시간 동안 몬스터 기절 및 데미지, 플레이어 이속 증가
    private IEnumerator Stun_DuringSkill()
    {
        GameObject temp;
        NormalMonster monster;
        List<bool> isFind = new List<bool>();

        while (Stun_duration < 2)
        {
            yield return new WaitForSeconds(1f);
            Stun_duration++;

            // RayCast에 닿는 All Monsters 배열(hits)로 가져오기
            hits = Physics.RaycastAll(transform.position + offset, transform.forward, MaxDistance);

            if (hits.Length != 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    // hits 원소들 모두 mosterList로 넣어, 중복 확인
                    temp = hits[i].collider.gameObject;
                    if (temp.tag == "Monster" && monsterList.Find(x => x == temp) == null)
                    {
                        monsterList.Add(temp);
                        // 데미지 입히기
                        monster = temp.GetComponent<NormalMonster>();
                        monster.Damaged(playerManager.playerData.STR * 0.5f);

                        // 현재 isFound 상태 저장
                        isFind.Add(monster.monsterData.isFound);
                        // 잠시 기절시키기
                        // Stunning
                        monster.monsterData.isFound = false;
                        print("Mons Damaged by STUN skill!");
                    }
                }
            }

            MonsterListDebug(monsterList);

            if (Stun_duration >= 2)
            {
                // RayCast에 닿은 몬스터 리스트에
                // 만약 몬스터가 한 마리 이상 있다면
                if (monsterList.Count != 0)
                {
                    foreach (GameObject monsters in monsterList)
                    {
                        // 모든 몬스터 저장해둔 isFound 상태로 복구
                        monsters.GetComponent<NormalMonster>().monsterData.isFound = isFind[monsterList.IndexOf(monsters)];
                    }
                }

                // 플레이어 이동속도 복구
                playerManager.playerData.moveSpeed = playerTempSpeed;

                // 쿨타임 초기화
                Stun_coolDelay = 0;

                // 몬스터 리스트 초기화
                monsterList.Clear();

                // 쿨타임 시작
                StartCoroutine(Stun_CountDelay());
                yield break;
            }
        }
    }


    // Stun (Skill #3) 쿨타임 초 세기
    private IEnumerator Stun_CountDelay()
    {
        while(Stun_coolDelay < Stun_maxCoolDelay)
        {
            yield return new WaitForSeconds(1f);
            Stun_coolDelay++;

            if (Stun_coolDelay >= Stun_maxCoolDelay)
            {
                Stun_coolDelay = Stun_maxCoolDelay;
                yield break;
            }
        }
    }

    
    // monsterList Debugging
    private void MonsterListDebug(List<GameObject> monsterList)
    {
        if (monsterList.Count == 0)
        {
            print("Cannot Detect any Monsters");
            return;
        }

        else{

            for (int i = 0; i < monsterList.Count; i++)
            {
                print("monsterList -> " + monsterList[i].gameObject.name);
            }
        }
    }

    void Start() {
        monsterList = new List<GameObject>();
    }
}