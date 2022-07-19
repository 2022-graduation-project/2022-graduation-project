using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected MeshCollider meshCollider;
    protected float damage;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();        
    }

    public virtual void Attack(float _damage)
    {
        meshCollider.enabled = true;
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            other.GetComponent<NormalMonster>().Damaged(damage);
            meshCollider.enabled = false;
        }
    }
}