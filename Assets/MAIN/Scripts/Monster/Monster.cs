using System.Collections;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    public MonsterData monsterData;
    public Animator animator;
    public Transform tr;

    void Start()
    {
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        Set();
    }

    public virtual void SetMonsterData()
    {
        // monster.json 에서 이름 기준으로 파싱해서 등록
    }


    public virtual void Set() { }
    public virtual void Chase(float _speed) { }
    public virtual void Attack() { }
    public virtual void Skill() { }
    public virtual void Damaged(float _damage, PlayerController _player = null) { }
    public virtual void Die() { }
}