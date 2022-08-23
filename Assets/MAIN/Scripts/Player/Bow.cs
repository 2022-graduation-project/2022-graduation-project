<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)
    private float damage = 30f;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)
    private float damage = 50f;
>>>>>>> 6ba036443e52ad52e4b8c32c79e3e199379f3fd4


    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
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
