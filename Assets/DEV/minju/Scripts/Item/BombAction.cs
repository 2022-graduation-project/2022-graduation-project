using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public void FireBomb(float firePower)
    {
        // 플레이어가 바라보는 방향으로
        // 폭탄 던지기
        Rigidbody rigOfBomb = GetComponent<Rigidbody>();
        rigOfBomb.AddForce((GameObject.Find("Main Camera").transform.forward + new Vector3(0,0.2f,0))
                            * firePower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) 
    {
        // Add Bomb Effect on this

        // Damage any monsters
        if(other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<NormalMonster>().Damaged(-20);
            print("Damaged by bomb");
        }

        Destroy(gameObject, 0.1f);
    }
}
