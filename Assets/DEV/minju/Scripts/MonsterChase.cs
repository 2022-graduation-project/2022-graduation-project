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

    //�÷��̾� ��ġ
    public Transform player;

    public IEnumerator Chase()
    {
        //�ȱ� �ִϸ��̼�
        anim.SetBool("isWalk", true);

        //��� �߰�
        while (manager.test.state == MonsterManager.Monster.States.Chase)
        {
            // Ÿ���� �� �� ���� ��
            if (manager.test.isFound)
            {
                //�������� �÷��̾� ��ġ�� ����
                manager.test.destPosition = player;

                
                //��������� ������������ ����
                Vector3 direction = manager.test.destPosition.position - transform.position;

                
                //������ ���� �̵�
                rig.AddForce(direction, ForceMode.Impulse);

                // Ÿ�� �������� ȸ����
                transform.LookAt(Vector3.Lerp(transform.position, manager.test.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //1�� �� ���� ������
                yield return new WaitForSeconds(1.0f);
            }

            //�÷��̾� ��ó ���� �Ÿ�(2f)�� �����ߴٸ� ����
            if (Vector3.Distance(transform.position, manager.test.destPosition.position) <= 2f)
            {
                //���� ���¸� Attack ���·� ��ȯ
                manager.test.state = MonsterManager.Monster.States.Attack;

                //���� �Լ� ȣ��
                StartCoroutine(attack.Attack());
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
