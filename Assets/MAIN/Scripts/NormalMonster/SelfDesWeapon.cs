using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesWeapon : MonoBehaviour
{
    public bool player_in = false;


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
