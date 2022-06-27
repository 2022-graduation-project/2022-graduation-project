using System.Collections;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public MeshCollider collider;
    public float damage;

    void Start()
    {
        collider = GetComponent<MeshCollider>();
        collider.enabled = false;
    }

    public virtual void Attack(float _damage) { }
}