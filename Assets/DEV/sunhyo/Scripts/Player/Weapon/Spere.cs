using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spere : Weapon
{
    public bool isSkill = false;
    public List<GameObject> monsterList;
    public override void Attack(float _damage)
    {
        damage = _damage;
        // 일반 공격
        if (!isSkill)
        {
            collider.enabled = true;
        }
        // 스킬 공격
        else
        {
            // 스킬 공격 버튼 눌렀을 때, Warrior에서 RayCast에 닿은 몬스터 리스트를 Spere에 넘겨준다.
            // 만약 몬스터가 한 마리 이상 있다면
            if (monsterList.Count != 0)
            {
                // 모든 몬스터 동시에 Damage 주기
                foreach (GameObject monsters in monsterList)
                {
                    monsters.GetComponent<MonsterController>().Damage(damage);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // 일반 공격
        if (other.tag == "Monster")
        {
            if (!isSkill)
            {
                print(other.name);
                other.GetComponent<MonsterController>().Damage(damage);
                collider.enabled = false;
            }
        }
    }
}
