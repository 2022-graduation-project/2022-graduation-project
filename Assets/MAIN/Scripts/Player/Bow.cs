using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)


    /* Protected Variable */
    private void Awake()
    {
        damage = 100f;
    }

    public override void Attack(float _damage, PlayerController _pc = null)
    {
        /*
        // attack effects
        Instantiate(slashEffect, transform.position + new Vector3(0, 0, 0.3f), Quaternion.identity);
        slashSound.Play();

        attackable = true;
        damage = _damage;
        pc = _pc;
        */
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            monster = other.transform.GetComponent<NormalMonster>();
            // 임시
            monster.dDamaged(damage);
        }
    }
}
