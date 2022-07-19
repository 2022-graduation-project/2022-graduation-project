using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    protected MeshCollider meshCollider;
    protected float damage;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    /* 몬스터(Melee) 무기에 플레이어 닿음 */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged");
        }
    }
}