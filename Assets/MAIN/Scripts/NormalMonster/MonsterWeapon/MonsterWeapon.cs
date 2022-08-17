using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    protected MeshCollider meshCollider;
    protected float damage;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }
}
