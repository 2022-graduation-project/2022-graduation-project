using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mpPotion : Item
{
    public override void Use()
    {
        // player 상태이상 함수 갖고 있는 스크립트 가져오기
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        // 회복 값 설정
        manaAmount = 10;
        // 상태이상 함수 Healing 호출
        StartCoroutine(player.RefillMana(manaAmount));
        print("Refilled the player's mana +10");
    }
}
