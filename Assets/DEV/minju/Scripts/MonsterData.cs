using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    public class Monster
    {
        //���� ����
        public enum States{
            Idle, Patrol, Chase, Attack
        };
        public States state;
        //���� �̸�
        public string name;
        //���� ü��
        public float hp;
        //���� �ӵ�
        public float moveSpeed, turnSpeed;
        //���� ���ݷ�
        public float attackForce;
        //�÷��̾� ã�Ҵ��� ����
        public bool isFound;
        //������ ��ġ (Patrol: random location, Chase: player location)
        public Transform destPosition;
    }
}
