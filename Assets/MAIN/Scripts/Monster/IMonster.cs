using System.Collections;
using UnityEngine;


public interface IMonster
{
    public MonsterData monsterData { get; set; }
    public Animator animator { get; set; }
    public Transform target { get; set; }

    void SetMonsterData();
    void Die();
    void Attack();
    void Skill();
    void Chase();
    void Damaged();
}