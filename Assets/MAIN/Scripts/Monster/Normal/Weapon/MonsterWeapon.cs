using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    protected Collider Collider;
    protected float damage;

    public virtual void Start()
    {
        Collider = GetComponent<Collider>();
    }
}
