using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    public NormalMonster monster;

    /* 추적 범위 내에 플레이어 진입 -> Target 설정 */
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            monster.target = other.gameObject.transform;
            Debug.Log("Monster : Target found");
        }
    }

    /* 추적 범위 내에서 플레이어 사라짐 */
    protected void OnTriggerExit(Collider other)
    {
        monster.target = null;
        Debug.Log("Monster : Target lost");
    }

}
