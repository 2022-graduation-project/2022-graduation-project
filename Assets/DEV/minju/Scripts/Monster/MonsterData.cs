using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    //���� ����
    public enum States
    {
        Idle, Patrol, Chase, Attack
    };
    public States state;


    //���� �̸�
    public string name;
    //���� �ִ� ü��
    public float maxHp;
    //���� ���� ü��
    public float curHp;
    //���� ������ �ӵ�
    public float moveSpeed;


    //���� ���ݷ�
    public float attackForce;
    //���� ȸ�� �ӵ�
    public float turnSpeed;
    //�÷��̾� ã�Ҵ��� ����
    public bool isFound;
    //�÷��̾� ��ġ ��� (Chase: player location)
    public Transform destPosition;
    //���Ϳ� �÷��̾� ���� �Ÿ�
    public float distance;
}
