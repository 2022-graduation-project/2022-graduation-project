using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerController pc;
    private bool attackable = false;
    protected MeshCollider meshCollider;
    protected float damage;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    public virtual void Attack(float _damage, PlayerController _pc)
    {
        attackable = true;
        damage = _damage;
        pc = _pc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackable && other.tag == "Monster")
        {
            print("공격????");
            other.GetComponent<NormalMonster>().Damaged(damage, pc);
        }

        attackable = false;
    }
}