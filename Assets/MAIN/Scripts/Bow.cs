using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private MonsterDummy monster = null;    // 데미지 받는 몬스터 (일단 하나)
    private float damage = 5f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            monster = other.transform.GetComponent<MonsterDummy>();
            monster.Damaged(damage);
        }
    }
}
