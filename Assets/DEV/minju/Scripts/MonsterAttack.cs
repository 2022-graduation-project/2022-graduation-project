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
    //�ִϸ�����
    Animator anim;

    // ���� �ֱ� �ð� (public)
    [HideInInspector]
    public float attackTime;

    public IEnumerator Attack()
    {
        //���� �ֱ� �ð�
        attackTime = 0f;

        //�ִϸ��̼� ù ���� ��
        if (anim.GetBool("isAttack") == false)
        {
            //���� �ִϸ��̼�
            anim.SetBool("isAttack", true);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
        }

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

            //���� �ֱ� 3�� �ʰ����� �˻�
            if (attackTime >= 3.0f)
            {
                //timer reset
                attackTime = 0f;

                print(manager.test.destPosition.position);

                //Start Attack
                //��ǥ �÷��̾ ���� �� (chase �� || �˻� collider �ݰ濡 �ɸ� ��)
                if (manager.test.destPosition != null)
                {
                    
                    
                    //�÷��̾� HP ���
                    player.Damaged(-10);
                }
            }
            //Wait until next frame
            yield return null;
        }
        
        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<MonsterManager>();
        chase = GetComponent<MonsterChase>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
