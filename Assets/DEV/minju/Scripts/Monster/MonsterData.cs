using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData
{
    // 몬스터 상태
    //public enum States
    //{
    //    Idle, Patrol, Chase, Attack
    //};
    //public States state;


    public string name;
    public float maxHp;
    public float curHp;
    public float moveSpeed;
    public float turnSpeed;
    public float attackForce;

    //// 플레이어 찾은 여부
    //public bool isFound;
    //// 발견한 플레이어의 위치 (Chase: player location)
    //public Transform destPosition;
    //// 플레이어와 몬스터 사이의 거리
    //public float distance;
}
