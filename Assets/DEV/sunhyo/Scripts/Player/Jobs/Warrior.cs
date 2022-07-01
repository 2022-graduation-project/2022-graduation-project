using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : PlayerController
{
    public List<GameObject> monsterList;
    RaycastHit[] hits;
    float MaxDistance = 10f;
    int coolDelay = 60;
    int maxCoolDelay = 60;

    int maxMpConsume = 20;

    float duration = 0;

    Vector3 offset;


    override public void UseSkill()
    {
        // 쿨타임이 차지 않았을 때 또는
        // 현재 MP가 소모량보다 적을 때
        if (coolDelay < maxCoolDelay || playerManager.playerData.curMp < maxMpConsume)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 플레이어 MP 소모
        print("Skill 1's maxMpConsume: " + maxMpConsume);
        ConsumeMP(maxMpConsume);

        // 지속시간 동안 Monster 리스트에 담고, 공격
        StartCoroutine(Duration1());

    }

    // 지속시간 동안 Monster 리스트에 담기
    private IEnumerator Duration1()
    {
        // 지속 시간 내에서 반복
        while (duration < 2)
        {
            // 지속 시간 재기
            print("Duration1: "+duration);
            yield return new WaitForSeconds(1f);
            duration++;

            // RayCast에 닿는 All Monsters 배열(hits)로 가져오기
            hits = Physics.RaycastAll(transform.position + offset, transform.forward, MaxDistance);

            if (hits.Length != 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    // hits 원소들 모두 mosterList로 넣기
                    GameObject temp = hits[i].collider.gameObject;
                    if (temp.tag == "Monster" && monsterList.Find(x => x == temp) == null)
                    {
                        monsterList.Add(temp);
                    }
                }
            }

            // monsterList Debugging
            for (int i = 0; i < monsterList.Count; i++)
            {
                print("monsterList -> " + monsterList[i].gameObject.name);
            }

            if (duration >= 2)
            {

                // 스킬 공격 버튼 눌렀을 때, Warrior에서 RayCast에 닿은 몬스터 리스트에
                // 만약 몬스터가 한 마리 이상 있다면
                if (monsterList.Count != 0)
                {
                    // 모든 몬스터 동시에 Damage 주기
                    foreach (GameObject monsters in monsterList)
                    {
                        monsters.GetComponent<MonsterController>().Damage(playerManager.playerData.STR);
                    }
                }

                // 쿨타임 초기화 및 초 세기
                coolDelay = 0;
                StartCoroutine("CountDelay1", maxCoolDelay);

                // 다음 차례를 위해 몬스터 리스트 비워주기
                monsterList.Clear();

                yield break;
            }
        }
    }

    // 스킬 1 쿨타임 초 세기
    private IEnumerator CountDelay1(int tempCoolDelay)
    {
        while (coolDelay < tempCoolDelay)
        {
            print("CountDelay1: " + coolDelay);
            yield return new WaitForSeconds(1f);
            coolDelay++;

            if (coolDelay >= tempCoolDelay)
            {
                coolDelay = 60;
                yield break;
            }
        }
    }


    int coolDelay2 = 60 * 1;
    float duration2 = 0f;
    override public void UseSkill2()
    {
        // 쿨타임이 차지 않았을 때 또는
        // 현재 MP가 소모량보다 적을 때
        if (coolDelay2 < 60 * 1 || playerManager.playerData.curMp < 10)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration2 = 0;

        // 플레이어 MP 소모
        print("Skill 2's maxMpConsume: 10");
        ConsumeMP(10);

        // 스킬 1의 쿨타임을 60->40초로 줄여줌
        maxCoolDelay = 40;

        // 스킬 1의 MP 소모량을 20->10로 줄여줌
        maxMpConsume = 10;

        // 지속 시간 후에 스킬 1의 속성 초기화
        StartCoroutine(Duration2());

    }


    // 지속 시간 후에 스킬 1의 속성 초기화
    private IEnumerator Duration2()
    {
        while (duration2 < 6)
        {
            yield return new WaitForSeconds(1f);
            duration2++;
            print("[[Duration2: " + duration2);

            if (duration2 >= 6)
            {
                // 스킬 1의 쿨타임 초기화
                maxCoolDelay = 60;
                print("Skill 2 makes MaxCoolDelay as 60: " + maxCoolDelay);

                // 스킬 1의 MP 소모량 초기화
                maxMpConsume = 20;
                print("Skill 2 makes maxMpConsume as 20: " + maxMpConsume);


                // 쿨타임 초기화 및 초 세기
                coolDelay2 = 0;
                StartCoroutine(CountDelay2());

                yield break;
            }
        }
    }

    // 스킬 2 쿨타임 초 세기
    private IEnumerator CountDelay2()
    {
        while (coolDelay2 < 60*1)
        {
            print("[[CountDelay2: " + coolDelay2);
            yield return new WaitForSeconds(1f);
            coolDelay2++;

            if (coolDelay2 >= 60 * 1)
            {
                coolDelay2 = 60 * 1;
                yield break;
            }
        }
    }

}