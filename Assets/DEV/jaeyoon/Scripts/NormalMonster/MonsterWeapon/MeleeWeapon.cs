using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonsterWeapon
{
    /* 몬스터(Melee) 무기에 플레이어 닿음 */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged");
            other.GetComponent<PlayerController>().Damaged(5);
        }
    }
}