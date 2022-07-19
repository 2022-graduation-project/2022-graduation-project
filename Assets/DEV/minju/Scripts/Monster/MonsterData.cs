using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    /*
    // 몬스터 상태
    public enum States
    {
        Idle, Patrol, Chase, Attack
    };
    public States state;
    */


    // 몬스터 이름
    public string name;
    // 최대 체력
    public float maxHp;
    // 현재 체력
    public float curHp;
    // 이동 속도
    public float moveSpeed;


    // 공격력
    public float attackForce;
    // 회전 속도
    public float turnSpeed;
    // 플레이어 찾은 여부
    public bool isFound;
    // 발견한 플레이어의 위치 (Chase: player location)
    public Transform destPosition;
    // 플레이어와 몬스터 사이의 거리
    public float distance;
}
