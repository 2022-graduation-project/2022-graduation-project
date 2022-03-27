using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    public class Monster
    {
        //몬스터 상태
        public enum States{
            Idle, Patrol, Chase, Attack
        };
        public States state;
        //몬스터 이름
        public string name;
        //몬스터 체력
        public float hp;
        //몬스터 속도
        public float moveSpeed, turnSpeed;
        //몬스터 공격력
        public float attackForce;
        //플레이어 찾았는지 여부
        public bool isFound;
        //목적지 위치 (Patrol: random location, Chase: player location)
        public Transform destPosition;
    }
}
