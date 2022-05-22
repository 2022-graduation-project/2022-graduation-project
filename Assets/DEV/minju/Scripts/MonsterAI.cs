using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public float health;

    // ���� ������ Ŭ����
    // Monster Data class
    public class Monster
    {
        //���� ����
        public enum States
        {
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
        public float distance;
    }

    // ���� �ִϸ�����
    // Monster Animator
    Animator anim;

    // �� ����
    // This Monster's data var
    Monster thisMon;

    // �� ������ ������ȣ
    // Index of this monster from List of MonsterManager
    public int monsterIdx;

    public IEnumerator Idle()
    {
        //Idle ������ �� ���� �ݺ�
        while (thisMon.state == Monster.States.Idle)
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
            if (thisMon.isFound)
            {
                //���� ���¸� ���� ���·� ��ȯ
                thisMon.state = Monster.States.Chase;

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


    // �÷��̾� ��ũ��Ʈ
    // Script which the player has
    PlayerController player;

    // ���� �ֱ� �ð� 
    float attackTime;

    // ���� �ִϸ��̼�
    // Attack aniamating
    void Animating()
    {
        //���� �ִϸ��̼�
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
    }

    // �÷��̾� ����
    // damaging player
    void damaging()
    {
        player.Damaged(-1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hpBarImage.fillAmount = thisMon.hp / 100;
            if (thisMon.hp <= 0f)
            {
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }
    }

    public IEnumerator Attack()
    {
        //���� �ֱ� �ð�
        attackTime = 0f;

        //���� ���� ���� ���� �ݺ�
        while (thisMon.state == Monster.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //�÷��̾ ���̴���, �÷��̾� ���� ���� ���� �ִ��� �˻�
            if (!thisMon.isFound || Vector3.Distance(transform.position, thisMon.destPosition.position) > 5.0f)
            {
                //�÷��̾ �Ⱥ��̰ų�, ���� ���� ���� ���� ��
                //�ٽ� �߰�
                //���� ���¸� Attack ���·� ��ȯ
                thisMon.state = Monster.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //���� �ֱ� 2.767�� �ʰ����� �˻�
            if (attackTime >= 2.767f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //��ǥ �÷��̾ ���� �� (chase �� || �˻� collider �ݰ濡 �ɸ� ��)
                if (thisMon.destPosition != null)
                {
                    // �÷��̾� HP ���
                    // damaging player
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
        while (thisMon.state == Monster.States.Chase)
        {
            // Ÿ���� �� �� ���� ��
            if (thisMon.isFound)
            {

                //�������� �÷��̾� ��ġ�� ����
                thisMon.destPosition = player.GetComponent<Transform>();


                //��������� ������������ ����
                Vector3 direction = thisMon.destPosition.position - transform.position;


                //������ ���� �̵�
                rig.AddForce(direction * Time.deltaTime * thisMon.moveSpeed, ForceMode.VelocityChange);
                //transform.Translate(direction * thisMon.moveSpeed * Time.deltaTime);

                // Ÿ�� �������� ȸ����
                transform.LookAt(Vector3.Lerp(transform.position, thisMon.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //�÷��̾� ��ó ���� �Ÿ�(2f)�� �����ߴٸ� ����
                if (Vector3.Distance(transform.position, thisMon.destPosition.position) <= thisMon.distance)
                {
                    //���� ���¸� Attack ���·� ��ȯ
                    thisMon.state = Monster.States.Attack;

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
                thisMon.state = Monster.States.Idle;

                //Idle �Լ� ȣ��
                StartCoroutine(Idle());
                yield break;
            }
        }



        //���� �����ӱ��� ��ٸ���.
        yield return null;
    }

    // ������������
    // is it Boss Monster?
    public bool isBossMonster;

    // ���� ü�� ������ �� UI
    // guage bar of boss HP
    GameObject monsterUI;
    GameObject gaugeBar;

    // �÷��̾ �����ݰ� �ȿ� ������ ���
    // player is in range of Monster's Sight
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ� ������ ���
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {
            // �� ���Ͱ� �������Ͷ��
            // if this is boss monster
            if (isBossMonster)
            {
                // ���� ü�� ������ �� ����
                // able to see the guage bar
                gaugeBar.SetActive(true);
                gaugeBar.GetComponent<Image>().fillAmount = thisMon.hp / 50f;
            }

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


    // �÷��̾ �����ݰ� �ȿ��� ����� ���
    // player is out of range of Monster's Sight
    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ �����ݰ� �ȿ��� ����� ���
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {
            // �� ���Ͱ� �������Ͷ��
            // if this is boss monster
            if (isBossMonster)
            {
                // ���� ü�� ������ �� �������
                // unable to see the guage bar
                gaugeBar.SetActive(false);
            }

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

    // MonsterMananger ��ũ��Ʈ
    // for using manager's func
    MonsterManager manager;


    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;
    GameObject hpBar;

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("HealthUI").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<MonsterUI>();
        _hpbar.enemyTr = transform;
        _hpbar.offset = hpBarOffset;
    }

    void DeleteHpBar()
    {
        Destroy(hpBar);
    }


    // player�� monster ���� ���� �� ȣ��
    // If player damages monster this will be called
    public void Damage(int scale)
    {
        // ���� ü���� ���� ���� ��
        if (thisMon.hp > 0)
        {
            //scale(-)��ŭ ���� ü���� �پ���.
            thisMon.hp += scale;
            hpBarImage.fillAmount = thisMon.hp / 100;
            print("MonsterHP: " + thisMon.hp);
        }
        // ���� ü���� ���� ��
        else
        {
            Invoke("Kill", 1f);
        }
    }

    public GameObject item;
    Transform itemLocation;

    public void Kill()
    {
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // ������ ����Ʈ����
        manager.DropItem(itemLocation);

        // ���� ����
        manager.DeleteMonster(monsterIdx);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("MAI ThisMonster#: " + monsterIdx);

        SetHpBar();

        // ���� �ִϸ�����
        // Monster Animator
        anim = GetComponent<Animator>();
        // ���� �����ۿ�
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // ���� �Ŵ��� ��ũ��Ʈ ã��
        // get MonsterManager script from GamaManager
        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        // �������� UI ������Ʈ ã��
        // find Monster's HP guage bar
        //monsterUI = GameObject.Find("MonsterUI");
        //gaugeBar = monsterUI.transform.Find("MonsterHP").gameObject;
        //gaugeBar.SetActive(false);
        


        // ���� ������Ʈ�� ���� �⺻�� ó�� �����ϱ� (���� ����)
        // setting default values of little monster
        if (!isBossMonster)
        {
            // create new data from Monster class
            thisMon = new Monster();
            // setting default state
            thisMon.state = Monster.States.Idle;
            // setting default hp
            thisMon.hp = 50f;
            // setting default speed
            thisMon.moveSpeed = 10f;
            thisMon.turnSpeed = 2f;
            // setting default power
            thisMon.attackForce = 10f;
            // setting default seeking state
            thisMon.isFound = false;
            // setting default destination
            thisMon.destPosition = null;
            // attack available distance
            thisMon.distance = 2f;
            hpBarImage.fillAmount = thisMon.hp / 100;
        }

        // ���� ������Ʈ�� ���� �⺻�� ó�� �����ϱ� (���� ����)
        // setting default values of boss monster
        else
        {
            // create new data from Monster class
            thisMon = new Monster();
            // setting default state
            thisMon.state = Monster.States.Idle;
            // setting default hp
            thisMon.hp = 100f;
            // setting default speed
            thisMon.moveSpeed = 30f;
            thisMon.turnSpeed = 10f;
            // setting default power
            thisMon.attackForce = 20f;
            // setting default seeking state
            thisMon.isFound = false;
            // setting default destination
            thisMon.destPosition = null;
            // attack available distance
            thisMon.distance = 5f;
        }

        // start with default state
        StartCoroutine(Idle());
    }


    // Update is called once per frame
    void Update()
    {
        // ���������� �� �÷��̾ ã�� ���¶��
        // if boss monster found any player
        if (isBossMonster && thisMon.isFound)
        {
            // ���� ü�� ������ �� Update
            // Update the guage bar
            //gaugeBar.SetActive(true);
            gaugeBar.GetComponent<Image>().fillAmount = thisMon.hp / 50f;
        }
    }
}
