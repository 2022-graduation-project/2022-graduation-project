using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public MonsterData monsterData;
    // MonsterMananger ��ũ��Ʈ
    // for using manager's func
    public MonsterManager manager;
    //���� �����ۿ�
    public Rigidbody rig;
    // ���� �ִϸ�����
    // Monster Animator
    public Animator anim;

    public GameObject hpBarPrefab;
    public GameObject hpBar;




    //�����̴� ����
    //moving direction //Move(), Chase()
    private Vector3 direction;
    // �÷��̾� ��ũ��Ʈ
    // Script which the player has
    private PlayerController player;
    // �� ���� hpBar
    private Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    virtual public void Start()
    {
        SetHpBar();
        hpBar.SetActive(false);

        // ���� �ִϸ�����
        // Monster Animator
        anim = GetComponent<Animator>();
        // ���� �����ۿ�
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // ���� �Ŵ��� ��ũ��Ʈ ã��
        // get MonsterManager script from GamaManager
        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();


        UpdateHpBar(monsterData.curHp);

        // start with default state
        StartCoroutine(Idle());
    }

    private void DeleteHpBar()
    {
        Destroy(hpBar);
    }



    // ���� �ִϸ��̼� �Լ�
    // Attack aniamating
    private void Animating()
    {
        //���� �ִϸ��̼�
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetTrigger("Attack");
    }

    // �÷��̾� ����
    // damaging player
    private void damaging()
    {

        player.Damaged(monsterData.attackForce);

    }


    // ���Ϳ� � ���� ������ ���
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            UpdateHpBar(monsterData.curHp);
        }
    }

    // �÷��̾ �����ݰ� �ȿ� ������ ���
    // player is in range of Monster's Sight
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ� ������ ���
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {

            // ���� ü�� �� ����
            hpBar.SetActive(true);

            // ã�Ҵٰ� ����
            // Monster found some player
            monsterData.isFound = true;

            // ������ �÷��̾� ��ġ�� ����
            // save destination as player's location
            monsterData.destPosition = other.transform;

            // �ݰ� �� �÷��̾� ����
            // save the player
            player = other.GetComponent<PlayerController>();
        }
    }


    // �÷��̾ �����ݰ� �ȿ��� ����� ���
    // player is out of range of Monster's Sight
    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ��� ����� ���
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {

            // ���� ü�� �� �����
            hpBar.SetActive(false);

            // ��ã�Ҵٰ� ����
            // player is leaving the range
            monsterData.isFound = false;

            // ������ �⺻������ ����
            // save destination as default
            monsterData.destPosition = null;

            // �÷��̾� �����
            // remove the player
            //player = null;
        }
    }

    /*---------------------------------------------
     *              DIE
     * -------------------------------------------*/

    public void Die()
    {
        Transform itemLocation;
        // ���� ��ġ+1�� ������ ������
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // ������ ����Ʈ����
        manager.DropItem(itemLocation);
        // ���� ����
        manager.DeleteMonster(gameObject);
    }

    /*---------------------------------------------
     *              ATTACK
     * -------------------------------------------*/

    public IEnumerator Attack()
    {
        //���� �ֱ� �ð�
        float attackTime = 0f;

        //���� ���� ���� ���� �ݺ�
        while (monsterData.state == MonsterData.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //�÷��̾ ���̴���, �÷��̾� ���� ���� ���� �ִ��� �˻�
            if (!monsterData.isFound || Vector3.Distance(transform.position, monsterData.destPosition.position) > monsterData.distance)
            {
                //�÷��̾ �Ⱥ��̰ų�, ���� ���� ���� ���� ��
                //�ٽ� �߰�
                //���� ���¸� Attack ���·� ��ȯ
                monsterData.state = MonsterData.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //���� �ֱ� 2.5�� �ʰ����� �˻�
            if (attackTime >= 2.5f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //��ǥ �÷��̾ ���� �� (chase �� || �˻� collider �ݰ濡 �ɸ� ��)
                if (monsterData.destPosition != null)
                {
                    // �÷��̾� HP ���
                    // damaging player
                    Invoke("damaging", 0.83f);

                    Animating();
                }
            }
            else anim.SetBool("isIdle", true);
            //Wait until next frame
            yield return null;
        }

        // ���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    /*---------------------------------------------
     *              DAMAGE
     * -------------------------------------------*/


    // player�� monster ���� ���� �� ȣ��
    // If player damages monster this will be called
    public void Damage(float scale)
    {
        // �ִϸ��̼�
        anim.SetTrigger("Damaged");

        // ���� ü���� ���� ���� ��
        if (monsterData.curHp > 0)
        {
            //scale(-)��ŭ ���� ü���� �پ���.
            monsterData.curHp -= scale;
            UpdateHpBar(monsterData.curHp);
        }
        // ���� ü���� ���� ��
        else
        {
            // �״� �ִϸ��̼�
            anim.SetBool("Dead", true);
            // ���� ü�¹� ����
            DeleteHpBar();
            Invoke("Die", 1f);
        }

        print(gameObject.name+"'s cur hp: " + monsterData.curHp);
    }

    /*---------------------------------------------
     *              MOVE
     * -------------------------------------------*/

    public void Move()
    {
        //������ ���� �̵�
        rig.AddForce(direction * Time.deltaTime * monsterData.moveSpeed, ForceMode.VelocityChange);

        // Ÿ�� �������� ȸ����
        transform.LookAt(Vector3.Lerp(transform.position, monsterData.destPosition.position, monsterData.turnSpeed * Time.deltaTime));
    }

    /*---------------------------------------------
     *              CHASE
     * -------------------------------------------*/

    public IEnumerator Chase()
    {
        //�ִϸ��̼� ù ���� ��
        if (anim.GetBool("isWalk") == false)
        {
            //�ȱ� �ִϸ��̼�
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
        }

        //��� �߰�
        while (monsterData.state == MonsterData.States.Chase)
        {
            // Ÿ���� �� �� ���� ��
            if (monsterData.isFound)
            {

                // �������� �÷��̾� ��ġ�� ����
                monsterData.destPosition = player.GetComponent<Transform>();


                // ��������� ������������ ����
                Vector3 direction = (monsterData.destPosition.position - transform.position);

                //������ ���� �̵�
                Move();

                //�÷��̾� ��ó ���� �Ÿ�(2f)�� �����ߴٸ� ����
                if (Vector3.Distance(transform.position, monsterData.destPosition.position) <= monsterData.distance)
                {
                    //���� ���¸� Attack ���·� ��ȯ
                    monsterData.state = MonsterData.States.Attack;

                    //���� �Լ� ȣ��
                    StartCoroutine(Attack());
                    yield break;
                }

                //1�� �� ���� ������
                yield return new WaitForSeconds(1.0f);
            }

            //Ÿ���� �� �� ���� ��
            else if (!monsterData.isFound)
            {
                //���� ���¸� Idle ���·� ��ȯ
                monsterData.state = MonsterData.States.Idle;

                //Idle �Լ� ȣ��
                StartCoroutine(Idle());
                yield break;
            }
        }



        //���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    /*---------------------------------------------
     *              Idle
     * -------------------------------------------*/

    public IEnumerator Idle()
    {
        //Idle ������ �� ���� �ݺ�
        while (monsterData.state == MonsterData.States.Idle)
        {
            //�ִϸ��̼� ù ���� ��
            if(anim.GetBool("isIdle") == false)
            {
                //���� �ִϸ��̼� ����
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
            }

            //�÷��̾ ã���� ��
            if (monsterData.isFound)
            {
                //���� ���¸� ���� ���·� ��ȯ
                monsterData.state = MonsterData.States.Chase;

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

    /*---------------------------------------------
     *              SETHPBAR
     * -------------------------------------------*/

    public void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        //hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.targetTr = transform;
        _hpbar.offset = hpBarOffset;
    }

    /*---------------------------------------------
     *              UPDATEHPBAR
     * -------------------------------------------*/

    public void UpdateHpBar(float hp)
    {
        hpBarImage.fillAmount = hp / monsterData.maxHp;
        if (hp <= 0f)
        {
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
        }
    }

}
