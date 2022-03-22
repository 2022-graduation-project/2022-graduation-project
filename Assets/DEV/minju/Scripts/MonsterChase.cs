using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    //���� �Ŵ��� ��ũ��Ʈ
    MonsterManager manager;
    //���� ���� ��ũ��Ʈ
    MonsterIdle idle;
    //���� �ȱ� ��ũ��Ʈ
    MonsterPatrol patrol;
    //���� ���� ��ũ��Ʈ
    MonsterAttack attack;
    //���� �ִϸ�����
    Animator anim;
    //���� �����ۿ�
    Rigidbody rig;
    public IEnumerator Chase()
    {
        //�ȱ� �ִϸ��̼�
        anim.SetBool("isWalk", true);
        while (manager.test.state == MonsterData.Monster.States.Chase)
        {
            if (manager.test.isFound)
            {
                //�������� �÷��̾� ��ġ�� ����
                //manager.test.destPosition = PlayerController.tr;

                //��������� ������������ ����
                Vector3 direction = manager.test.destPosition.position - transform.position;

                //������ ���� �̵� (Z�� ���� ����)
                rig.AddForce(direction, ForceMode.Impulse);
            }
        }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //���� �Ŵ��� ��ũ��Ʈ
        manager = GetComponent<MonsterManager>();
        //���� �ѱ� ��ũ��Ʈ
        idle = GetComponent<MonsterIdle>();
        //���� �ȱ� ��ũ��Ʈ
        patrol = GetComponent<MonsterPatrol>();
        //���� ���� ��ũ��Ʈ
        attack = GetComponent<MonsterAttack>();
        //���� �ִϸ�����
        anim = GetComponent<Animator>();
        //���� �����ۿ�
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
