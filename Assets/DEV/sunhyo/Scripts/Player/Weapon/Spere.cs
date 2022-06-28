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
        // �Ϲ� ����
        if (!isSkill)
        {
            collider.enabled = true;
        }
        // ��ų ����
        else
        {
            // ��ų ���� ��ư ������ ��, Warrior���� RayCast�� ���� ���� ����Ʈ�� Spere�� �Ѱ��ش�.
            // ���� ���Ͱ� �� ���� �̻� �ִٸ�
            if (monsterList.Count != 0)
            {
                // ��� ���� ���ÿ� Damage �ֱ�
                foreach (GameObject monsters in monsterList)
                {
                    monsters.GetComponent<MonsterController>().Damage(damage);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // �Ϲ� ����
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
