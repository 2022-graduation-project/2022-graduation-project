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

    public Vector3 direction;

    //�÷��̾� ��ũ��Ʈ
    public PlayerController player;

    public IEnumerator Chase()
    {
        //�ִϸ��̼� ù ���� ��
        if (anim.GetBool("isWalk") == false)
        {
            //�ȱ� �ִϸ��̼�
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", false);
        }

        //��� �߰�
        while (manager.test.state == MonsterManager.Monster.States.Chase)
        {
            // Ÿ���� �� �� ���� ��
            if (manager.test.isFound)
            {
                

                //�������� �÷��̾� ��ġ�� ����
                manager.test.destPosition = player.GetComponent<Transform>();

                
                //��������� ������������ ����
                Vector3 direction = manager.test.destPosition.position - transform.position;

                
                //������ ���� �̵�
                rig.AddForce(direction, ForceMode.VelocityChange);

                // Ÿ�� �������� ȸ����
                transform.LookAt(Vector3.Lerp(transform.position, manager.test.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //�÷��̾� ��ó ���� �Ÿ�(2f)�� �����ߴٸ� ����
                if (Vector3.Distance(transform.position, manager.test.destPosition.position) <= 5f)
                {
                    //���� ���¸� Attack ���·� ��ȯ
                    manager.test.state = MonsterManager.Monster.States.Attack;

                    //���� �Լ� ȣ��
                    StartCoroutine(attack.Attack());
                    yield break;
                }

                //1�� �� ���� ������
                yield return new WaitForSeconds(1.0f);
            }

            //Ÿ���� �� �� ���� ��
            else if (!manager.test.isFound)
            {
                //���� ���¸� Idle ���·� ��ȯ
                manager.test.state = MonsterManager.Monster.States.Idle;

                //Idle �Լ� ȣ��
                StartCoroutine(idle.Idle());
                yield break;
            }
        }

        

        //���� �����ӱ��� ��ٸ���.
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
