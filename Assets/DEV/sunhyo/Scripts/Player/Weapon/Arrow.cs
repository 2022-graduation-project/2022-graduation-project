using System.Collections;
using UnityEngine;


public class Arrow : Weapon
{
    public override void Attack(float _damage)
    {
        meshCollider.enabled = true;
        damage = _damage;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            print(other.name);
            other.GetComponent<MonsterController>().Damaged(damage);
            meshCollider.enabled = false;
        }
    }
}