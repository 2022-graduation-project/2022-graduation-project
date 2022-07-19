using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldPotion : Item
{
    public override void Use()
    {
        // player 상태이상 함수 갖고 있는 스크립트 가져오기
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        // 방어 지속시간 설정
        shieldDuration = 10f;
        // 상태이상 함수 Healing 호출
        StartCoroutine(player.Shielding(shieldDuration));
    }
}
