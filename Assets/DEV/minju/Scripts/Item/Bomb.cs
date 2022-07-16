using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public override void Use()
    {
        // Instantiate(폭탄오브젝트)
        // 플레이어의 손 위치에서
        // 플레이어가 바라보는 방향으로
        // 폭탄 던지기
        print("It will damage Monster");
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
