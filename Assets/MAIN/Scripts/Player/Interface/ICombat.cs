using System.Collections;
using UnityEngine;


public interface ICombat
{
    public void Attack(float _damage);
    public void Damaged(float _damage, string _type = null, float _amount = 0, float _time = 0);
    public void Die();
}