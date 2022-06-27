using System.Collections;
using UnityEngine;


public class Axe : Weapon
{
    public override void Attack(float _damage)
    {
        collider.enabled = true;
        damage = _damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            print(other.name);
            other.GetComponent<MonsterController>().Damage(damage);
            collider.enabled = false;
        }
    }
}