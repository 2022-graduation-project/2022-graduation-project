using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpPotion : Item
{
    public override void Use()
    {
        //// player 상태이상 함수 갖고 있는 스크립트 가져오기
        //player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //// 회복 값 설정
        //manaAmount = 10;
        //// 상태이상 함수 Healing 호출
        //StartCoroutine(player.RefillMana(manaAmount));
        //print("Refilled the player's mana +10");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
