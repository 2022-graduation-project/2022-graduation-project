using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    //���� �ִϸ�����
    Animator anim;
    //�Ŵ������� ������ ����
    MonsterManager.Monster thisMon;


    public IEnumerator Idle(MonsterManager.Monster tempMon)
    {
        thisMon = tempMon;
        //Idle ������ �� ���� �ݺ�
        while (tempMon.state == MonsterManager.Monster.States.Idle)
        {
            //�ִϸ��̼� ù ���� ��
            if(anim.GetBool("isIdle") == false)
            {
                //���� �ִϸ��̼� ����
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
                anim.SetBool("isAttack", false);
            }

            //�÷��̾ ã���� ��
            if (tempMon.isFound)
            {
                //���� ���¸� ���� ���·� ��ȯ
                tempMon.state = MonsterManager.Monster.States.Chase;

                //�÷��̾ �� �� ������, �����Ѵ�.
                StartCoroutine(Chase());
                yield break;
            }

            //Patrol ���?

            yield return new WaitForSeconds(1.0f);
        }
        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }


    //�÷��̾� ��ũ��Ʈ
    PlayerController player;

    // ���� �ֱ� �ð� (public)
    [HideInInspector]
    public float attackTime;

    public IEnumerator Attack()
    {
        //���� �ֱ� �ð�
        attackTime = 0f;

        //���� ���� ���� ���� �ݺ�
        while (thisMon.state == MonsterManager.Monster.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //�÷��̾ ���̴���, �÷��̾� ���� ���� ���� �ִ��� �˻�
            if (!thisMon.isFound || Vector3.Distance(transform.position, thisMon.destPosition.position) > 5.0f)
            {
                //�÷��̾ �Ⱥ��̰ų�, ���� ���� ���� ���� ��
                //�ٽ� �߰�
                //���� ���¸� Attack ���·� ��ȯ
                thisMon.state = MonsterManager.Monster.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //���� �ֱ� 2.767�� �ʰ����� �˻�
            if (attackTime >= 2.767f)
            {


                //timer reset
                attackTime = 0f;

                print(thisMon.destPosition.position);

                //Start Attack
                //��ǥ �÷��̾ ���� �� (chase �� || �˻� collider �ݰ濡 �ɸ� ��)
                if (thisMon.destPosition != null)
                {
                    //�÷��̾� HP ���

                    Invoke("damaging", 1.0f);

                    //�ִϸ��̼� ù ���� ��
                    if (anim.GetBool("isAttack") == false)
                    {
                        Animating();
                    }
                }
            }
            //Wait until next frame
            yield return null;
        }

        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    //���� �����ۿ�
    Rigidbody rig;

    //�����̴� ����
    //moving direction
    public Vector3 direction;

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
        while (thisMon.state == MonsterManager.Monster.States.Chase)
        {
            // Ÿ���� �� �� ���� ��
            if (thisMon.isFound)
            {


                //�������� �÷��̾� ��ġ�� ����
                thisMon.destPosition = player.GetComponent<Transform>();


                //��������� ������������ ����
                Vector3 direction = thisMon.destPosition.position - transform.position;


                //������ ���� �̵�
                rig.AddForce(direction, ForceMode.VelocityChange);

                // Ÿ�� �������� ȸ����
                transform.LookAt(Vector3.Lerp(transform.position, thisMon.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //�÷��̾� ��ó ���� �Ÿ�(2f)�� �����ߴٸ� ����
                if (Vector3.Distance(transform.position, thisMon.destPosition.position) <= 5f)
                {
                    //���� ���¸� Attack ���·� ��ȯ
                    thisMon.state = MonsterManager.Monster.States.Attack;

                    //���� �Լ� ȣ��
                    StartCoroutine(Attack());
                    yield break;
                }

                //1�� �� ���� ������
                yield return new WaitForSeconds(1.0f);
            }

            //Ÿ���� �� �� ���� ��
            else if (!thisMon.isFound)
            {
                //���� ���¸� Idle ���·� ��ȯ
                thisMon.state = MonsterManager.Monster.States.Idle;

                //Idle �Լ� ȣ��
                StartCoroutine(Idle(thisMon));
                yield break;
            }
        }



        //���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    void Animating()
    {
        //���� �ִϸ��̼�
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
    }

    void damaging()
    {
        player.Damaged(-10);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ� ������ ���
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {
            // ã�Ҵٰ� ����
            // Monster found some player
            thisMon.isFound = true;
            
            // ������ �÷��̾� ��ġ�� ����
            // save destination as player's location
            thisMon.destPosition = other.transform;

            // �ݰ� �� �÷��̾� ����
            // save the player
            player = other.GetComponent<PlayerController>();
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ��� ����� ���
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {
            // ��ã�Ҵٰ� ����
            // player is leaving the range
            thisMon.isFound = false;

            // ������ �⺻������ ����
            // save destination as default
            thisMon.destPosition = null;

            // �÷��̾� �����
            // remove the player
            player = null;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
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
