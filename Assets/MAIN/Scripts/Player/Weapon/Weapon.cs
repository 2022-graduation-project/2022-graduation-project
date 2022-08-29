using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerController pc;
    private bool attackable = false;
    protected MeshCollider meshCollider;
    protected float damage;
    public Object slashEffect;
    protected AudioSource slashSound;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        slashSound = GetComponent<AudioSource>();
    }

    public virtual void Attack(float _damage, PlayerController _pc = null)
    {
        // attack effects
        //Instantiate(slashEffect, transform.position+new Vector3(0,0,0.3f), Quaternion.identity);
        slashSound.Play();
        
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