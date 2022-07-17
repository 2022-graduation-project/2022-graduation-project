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



    int coolDelay1 = 60;
    int maxCoolDelay1 = 60;
    int maxMpConsume1 = 20;
    float duration = 0;

    /*****************************************
     * Blast (Skill #1) 공격
     * Blast() -> BlastDuration() -> BlastCountDelay() 
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
        if (coolDelay1 < maxCoolDelay1 || playerManager.playerData.curMp < maxMpConsume1)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 플레이어 MP 소모
        ConsumeMP(maxMpConsume1);

        // 지속시간 동안 Monster 리스트에 담고, 공격
        StartCoroutine(BlastDuration());
    }
    
    // Blast (Skill #1) 지속시간 동안 Monster 리스트에 담기
    private IEnumerator BlastDuration()
    {
        GameObject temp;
        // Blast 지속 시간: 2초
        while (duration < 2)
        {
            // 지속 시간 재기
            yield return new WaitForSeconds(1f);
            duration++;

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

            if (monsterList.Count == 0)
            {
                print("Cannot Detect any Monsters");
            }

            else{

                for (int i = 0; i < monsterList.Count; i++)
                {
                    print("monsterList -> " + monsterList[i].gameObject.name);
                }
            }

            if (duration >= 2)
            {

                // RayCast에 닿은 몬스터 리스트에
                // 만약 몬스터가 한 마리 이상 있다면
                if (monsterList.Count != 0)
                {
                    // 모든 몬스터 동시에 Damage 주기
                    foreach (GameObject monsters in monsterList)
                    {
                        monsters.GetComponent<MonsterController>().Damaged(playerManager.playerData.STR);
                    }
                }

                // 쿨타임 초기화 및 초 세기
                coolDelay1 = 0;
                StartCoroutine(BlastCountDelay(maxCoolDelay1));

                // 다음 차례를 위해 몬스터 리스트 비워주기
                monsterList.Clear();

                yield break;
            }
        }
    }

    // Blast (Skill #1) 쿨타임 초 세기
    private IEnumerator BlastCountDelay(int tempCoolDelay1)
    {
        while (coolDelay1 < tempCoolDelay1)
        {
            yield return new WaitForSeconds(1f);
            coolDelay1++;

            if (coolDelay1 >= tempCoolDelay1)
            {
                // set as default
                coolDelay1 = 60;
                yield break;
            }
        }
    }


    int coolDelay2 = 60;
    int maxCoolDelay2 = 60 * 1;
    int maxMpConsume2 = 20;
    float duration2 = 0f;

    /*****************************************
     * Effect (Skill #2) 스킬
     * Effect() -> EffectDuration() -> EffectCountDelay() 
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
        if (coolDelay2 < maxCoolDelay2 || playerManager.playerData.curMp < maxMpConsume2)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration2 = 0;

        // 플레이어 MP 소모
        ConsumeMP(10);

        // 스킬 1의 쿨타임을 60->40초로 줄여줌
        maxCoolDelay1 = 40;

        // 스킬 1의 MP 소모량을 20->10로 줄여줌
        maxMpConsume1 = 10;

        // 지속 시간 후에 스킬 1의 속성 초기화
        StartCoroutine(EffectDuration());
    }

    // Effect (Skill #2) 지속시간 후 Skill #1의 속성 초기화
    private IEnumerator EffectDuration()
    {
        while (duration2 < 6)
        {
            yield return new WaitForSeconds(1f);
            duration2++;

            if (duration2 >= 6)
            {
                // 스킬 1의 쿨타임 초기화
                maxCoolDelay1 = 60;

                // 스킬 1의 MP 소모량 초기화
                maxMpConsume1 = 20;

                // 쿨타임 초기화 및 초 세기
                coolDelay2 = 0;
                StartCoroutine(EffectCountDelay());

                yield break;
            }
        }
    }

    // Effect (Skill #2) 쿨타임 초 세기
    private IEnumerator EffectCountDelay()
    {
        while (coolDelay2 < maxCoolDelay2)
        {
            yield return new WaitForSeconds(1f);
            coolDelay2++;

            if (coolDelay2 >= maxCoolDelay2)
            {
                coolDelay2 = maxCoolDelay2;
                yield break;
            }
        }
    }

    int coolDelay3 = 60 * 5;
    int maxCoolDelay3 = 60 * 5;
    int maxMpConsume3 = 30;
    int duration3 = 0;


    float playerTempSpeed = 0;
    /*****************************************
     * Stun (Skill #3) 스킬
     * Stun() -> StunDuration() -> StunCountDelay() 
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
        if (coolDelay3 < maxCoolDelay3 || playerManager.playerData.curMp < maxMpConsume3)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration3 = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 플레이어 MP 소모
        ConsumeMP(maxMpConsume3);

        // 플레이어 이동속도 증가
        playerTempSpeed = playerManager.playerData.moveSpeed;
        playerManager.playerData.moveSpeed = playerTempSpeed * 1.3f;

        // 지속시간 동안 Monster 리스트에 담고, 공격
        StartCoroutine(StunDuration());
    }


    // Stun (Skill #3) 지속시간 동안 몬스터 기절 및 데미지, 플레이어 이속 증가
    private IEnumerator StunDuration()
    {
        GameObject temp;
        MonsterController monster;
        List<bool> isFind = new List<bool>();

        while (duration3 < 2)
        {
            yield return new WaitForSeconds(1f);
            duration3++;


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
                        monster = temp.GetComponent<MonsterController>();
                        monster.Damaged(playerManager.playerData.STR * 0.5f);

                        // 현재 isFound 상태 저장
                        isFind.Add(monster.monsterData.isFound);
                        // 잠시 기절시키기
                        // Stunning
                        monster.monsterData.isFound = false;
                    }
                }
            }

            if (monsterList.Count == 0)
            {
                print("Cannot Detect any Monsters");
            }

            else{

                for (int i = 0; i < monsterList.Count; i++)
                {
                    print("monsterList -> " + monsterList[i].gameObject.name);
                }
            }

            if (duration3 >= 2)
            {
                // RayCast에 닿은 몬스터 리스트에
                // 만약 몬스터가 한 마리 이상 있다면
                if (monsterList.Count != 0)
                {
                    foreach (GameObject monsters in monsterList)
                    {
                        // 모든 몬스터 저장해둔 isFound 상태로 복구
                        monsters.GetComponent<MonsterController>().monsterData.isFound = isFind[monsterList.IndexOf(monsters)];
                    }
                }

                // 플레이어 이동속도 복구
                playerManager.playerData.moveSpeed = playerTempSpeed;

                // 쿨타임 초기화
                coolDelay3 = 0;

                // 몬스터 리스트 초기화
                monsterList.Clear();

                // 쿨타임 시작
                StartCoroutine(StunCountDelay());
                yield break;
            }
        }
    }


    // Stun (Skill #3) 쿨타임 초 세기
    private IEnumerator StunCountDelay()
    {
        while(coolDelay3 < maxCoolDelay3)
        {
            yield return new WaitForSeconds(1f);
            coolDelay3++;

            if (coolDelay3 >= maxCoolDelay3)
            {
                coolDelay3 = maxCoolDelay3;
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