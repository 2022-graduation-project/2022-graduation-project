using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    //���� �Ŵ��� ��ũ��Ʈ
    MonsterManager manager;
    //���� �ѱ� ��ũ��Ʈ
    MonsterChase chase;
    //���� �ȱ� ��ũ��Ʈ
    MonsterPatrol patrol;
    //���� �ִϸ�����
    Animator anim;

    public IEnumerator Idle()
    {
        //���� �ִϸ��̼� ����
        anim.SetBool("isIdle", true);

        //Idle ������ �� ���� �ݺ�
        while(manager.test.state == MonsterData.Monster.States.Idle)
        {
            //�÷��̾ ã���� ��
            if (manager.test.isFound)
            {
                //���� ���¸� ���� ���·� ��ȯ
                manager.test.state = MonsterData.Monster.States.Chase;

                //�÷��̾ �� �� ������, �����Ѵ�.
                StartCoroutine(chase.Chase());
                yield break;
            }
            //Patrol ���?
        }
        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //���� �Ŵ��� ��ũ��Ʈ
        manager = GetComponent<MonsterManager>();
        //���� �ѱ� ��ũ��Ʈ
        chase = GetComponent<MonsterChase>();
        //���� �ȱ� ��ũ��Ʈ
        patrol = GetComponent<MonsterPatrol>();
        //���� �ִϸ�����
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
