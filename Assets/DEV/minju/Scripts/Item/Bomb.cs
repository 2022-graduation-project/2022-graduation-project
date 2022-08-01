using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : Item
{
    public override void Use()
    {
        firePower = 30f;

        Transform handLocation = GameObject.FindWithTag("Player").GetComponent<PlayerController>().weapon.transform;
        // Instantiate(폭탄오브젝트)
        // 플레이어의 손 위치에서 생성
        GameObject bomb = Instantiate(Resources.Load("Bomb"), handLocation.position, Quaternion.identity) as GameObject;
        // 발사
        bomb.GetComponent<BombAction>().FireBomb(firePower);
        print("Throwing bomb");
    }
}
