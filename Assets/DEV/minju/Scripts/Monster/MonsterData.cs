using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    //몬스터 상태
    public enum States
    {
        Idle, Patrol, Chase, Attack
    };
    public States state;


    //몬스터 이름
    public string name;
    //몬스터 최대 체력
    public float maxHp;
    //몬스터 현재 체력
    public float curHp;
    //몬스터 움직임 속도
    public float moveSpeed;


    //몬스터 공격력
    public float attackForce;
    //몬스터 회전 속도
    public float turnSpeed;
    //플레이어 찾았는지 여부
    public bool isFound;
    //플레이어 위치 기억 (Chase: player location)
    public Transform destPosition;
    //몬스터와 플레이어 사이 거리
    public float distance;
}
