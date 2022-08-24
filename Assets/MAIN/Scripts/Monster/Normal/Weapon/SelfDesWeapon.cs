using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesWeapon : MonsterWeapon
{
    public bool player_in = false;

    public override void Start()
    {
        base.Start();
        damage = 5f;
    }


    /* 플레이어가 Damage Range (공격 범위) 내에 들어와 있을 때 */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_in = true;
        }
    }

    /* 플레이어가 Damage Range (공격 범위) 내에 들어와 있지 않을 때 */
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player_in = false;
        }
    }
}
