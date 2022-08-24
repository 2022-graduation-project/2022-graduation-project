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

    public virtual void Attack(float _damage, PlayerController _pc = null)
    {
        attackable = true;
        damage = _damage;
        pc = _pc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackable && other.tag == "Monster")
        {
            other.GetComponent<Monster>().Damaged(damage, pc);
        }

        attackable = false;
    }
}