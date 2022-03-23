using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    //���� �Ŵ��� ��ũ��Ʈ
    public MonsterManager manager;
    //�߰� ��ũ��Ʈ
    public MonsterChase chase;
    //�÷��̾� ��ũ��Ʈ
    public PlayerController player;

    public IEnumerator Attack()
    {
        //���� �ֱ� �ð�
        float attackTime = 0f;

        //���� ���� ���� ���� �ݺ�
        while (manager.test.state == MonsterManager.Monster.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //�÷��̾ ���̴���, �÷��̾� ���� ���� ���� �ִ��� �˻�
            if (!manager.test.isFound || Vector3.Distance(transform.position, manager.test.destPosition.position) > 2.0f)
            {
                //�÷��̾ �Ⱥ��̰ų�, ���� ���� ���� ���� ��
                //�ٽ� �߰�
                //���� ���¸� Attack ���·� ��ȯ
                manager.test.state = MonsterManager.Monster.States.Chase;
                StartCoroutine(chase.Chase());
                yield break;
            }

            //���� �ֱ� 1�� �ʰ����� �˻�
            if (attackTime <= 1.0f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //��ǥ �÷��̾ ���� �� (chase �� || �˻� collider �ݰ濡 �ɸ� ��)
                if (manager.test.destPosition != null)
                {
                    //���� �ִϸ��̼�
                    //anim.SetBool("isAttack", true);

                    //�÷��̾� HP ���
                    player.Damaged(-10);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
        
        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<MonsterManager>();
        chase = GetComponent<MonsterChase>();
        //player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
