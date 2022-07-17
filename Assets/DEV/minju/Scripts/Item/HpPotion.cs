using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpPotion : Item
{
    public override void Use()
    {
        // player 상태이상 함수 갖고 있는 스크립트 가져오기
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        // 회복 값 설정
        healAmount = 10;
        // 상태이상 함수 Healing 호출
        StartCoroutine(player.Healing(healAmount));
        print("Healed the player +10");
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
