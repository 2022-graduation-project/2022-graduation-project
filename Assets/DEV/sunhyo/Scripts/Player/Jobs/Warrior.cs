using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : PlayerController
{
    public List<GameObject> monsterList;
    RaycastHit[] hits;
    float MaxDistance = 10f;
    int coolDelay = 60;
    float duration = 0;

    Vector3 offset;


    override public void UseSkill()
    {
        // 쿨타임이 차지 않았을 때
        if (coolDelay < 60)
        {
            return;
        }

        // Initial setting
        // 지속시간 초기화
        duration = 0;
        offset = new Vector3(0f, 0.7f, 0.5f);

        // 지속 시간 내에서 반복
        while (duration < 2)
        {
            // 지속 시간 재기
            duration += Time.deltaTime;

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

        }

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
        StartCoroutine("countDelay1");

        // 플레이어 MP 소모
        playerManager.playerData.curMp -= 20;

        // 다음 차례를 위해 몬스터 리스트 비워주기
        monsterList.Clear();

    }

    // 스킬 1 쿨타임 초 세기
    private IEnumerator countDelay1()
    {
        while (coolDelay < 60)
        {
            print("cool Delay: " + coolDelay);
            yield return new WaitForSeconds(1f);
            coolDelay++;
            if (coolDelay >= 60)
                yield break;
        }
    }


}