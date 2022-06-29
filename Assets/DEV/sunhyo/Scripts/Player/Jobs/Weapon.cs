using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private MeshCollider meshCollider;
    private float damage;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();        
    }

    public void Attack(float _damage)
    {
        meshCollider.enabled = true;
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            other.GetComponent<MonsterDummy>().Damage(damage);
            meshCollider.enabled = false;
        }
    }
}