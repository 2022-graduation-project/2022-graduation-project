using System.Collections;
using UnityEngine;

public class Wand : Weapon
{
    public GameObject projectile;
    public override void Attack(float _damage)
    {
        collider.enabled = true;
        damage = _damage;

        // 투사체 발사?
        GameObject pj = Instantiate(Resources.Load<GameObject>("Weapon/Projectile"), transform);
        pj.transform.parent = projectile.transform;
    }
}