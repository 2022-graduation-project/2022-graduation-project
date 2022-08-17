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
    }

    public void SetMonsterData()
    {
        // monster.json 에서 이름 기준으로 파싱해서 등록
    }

    public virtual void Die() { }
    public virtual void Attack() { }
    public virtual void Skill() { }
    public virtual void Chase() { }
    public virtual void Damaged() { }
}