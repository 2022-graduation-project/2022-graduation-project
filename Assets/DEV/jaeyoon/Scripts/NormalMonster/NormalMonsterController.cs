using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalMonsterController : MonoBehaviour
{
    /* Manager */
    protected MonsterManager monsterManager;

    /* local data ? runtime data ? */
    public MonsterData monsterData;
    // for using manager's func
    public MonsterManager manager;  // ㅁㅏㅈㄴㅑ?
    protected Rigidbody rigidBody;
    protected Animator animator;

    //public GameObject hpBarPrefab;
    public GameObject MonsterHpBar;
    private GameObject hpBar;
    private Slider slider;


    //moving direction //Move(), Chase()
    private Vector3 direction;
    // ???????? ????????
    // Script which the player has
    private PlayerController player;
    // ?? ?????? hpBar
    private Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas canvas;
    //private Image hpBarImage;

    
    public Text HpText;
    float maxHealth = 100;
    float minHealth = 0;
    //private Transform transform;    // HP Bar ㅇㅜㅣㅊㅣ
    public float hp;
    public float damage;
    


    virtual public void Start()
    {
        SetHpBar();
        hpBar.SetActive(false);

        // Monster Rigidbody
        rigidBody = GetComponent<Rigidbody>();
        // Monster Animator
        animator = GetComponent<Animator>();

        // get MonsterManager script from GamaManager
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();


        UpdateHpBar(monsterData.curHp);

        // start with default state
        StartCoroutine(Idle());
    }



    /*---------------------------------------------
     *              HP BAR
     * -------------------------------------------*/

    public void SetHpBar()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //transform = transform.Find("Root").Find("HpBarPos");

        hpBar = transform.Find("Root").Find("Canvas").GetComponent<GameObject>();
        /*
        MonsterHpBar = GameObject.Find("MonsterHpBar").GetComponent<GameObject>();
        GameObject hpBar = Instantiate<GameObject>(MonsterHpBar);
        */

        /*
        GameObject hpBar = Instantiate<GameObject>(MonsterHpBar, transform.position, Quaternion.identity, canvas.transform);
        slider = GetComponent<Slider>();
        HpText = GetComponent<Text>();
        */

        // hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        /*
        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.targetTr = transform;
        _hpbar.offset = hpBarOffset;
        */
    }

    public void UpdateHpBar(float hp)
    {
        if (hp <= 10)
        {
            HpText.color = Color.red;
        }
        HpText.text = hp.ToString();
        slider.value = (hp / maxHealth);

        if (slider.value <= minHealth)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);



        /*
        hpBarImage.fillAmount = hp / monsterData.maxHp;
        if (hp <= 0f)
        {
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
        }
        */
    }

   
    private void DeleteHpBar()
    {
        Destroy(hpBar);
    }



    // Attack aniamating
    private void Animating()
    {
        animator.SetBool("Dead", false);
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");
    }

    // ???????? ????
    // damaging player
    private void damaging()
    {
        player.Damaged(monsterData.attackForce);
    }


    // ???????? ???? ???? ???????? ????
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            UpdateHpBar(monsterData.curHp);
        }
    }

    // ?????????? ???????? ???? ???????? ????
    // player is in range of Monster's Sight
    private void OnTriggerEnter(Collider other)
    {
        // ?????????? ???????? ???? ???????? ????
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {

            // ?????? ???? ?? ??????
            hpBar.SetActive(true);

            // ???????? ????
            // Monster found some player
            monsterData.isFound = true;

            // ?????? ???????? ?????? ????
            // save destination as player's location
            monsterData.destPosition = other.transform;

            // ???? ?? ???????? ????
            // save the player
            player = other.GetComponent<PlayerController>();
        }
    }


    // ?????????? ???????? ?????? ???????? ????
    // player is out of range of Monster's Sight
    private void OnTriggerExit(Collider other)
    {
        // ?????????? ???????? ?????? ???????? ????
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {

            // ?????? ???? ?? ??????
            hpBar.SetActive(false);

            // ?????????? ????
            // player is leaving the range
            monsterData.isFound = false;

            // ?????? ?????????? ????
            // save destination as default
            monsterData.destPosition = null;

            // ???????? ??????
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
        // ???? ????+1?? ?????? ??????
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // ?????? ??????????
        manager.DropItem(itemLocation);
        // ?????? ????
        manager.DeleteMonster(gameObject);
    }

    /*---------------------------------------------
     *              ATTACK
     * -------------------------------------------*/

    public IEnumerator Attack()
    {
        //???? ???? ????
        float attackTime = 0f;

        //???? ???? ???? ???? ????
        while (monsterData.state == MonsterData.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //?????????? ????????, ???????? ???? ???? ???? ?????? ????
            if (!monsterData.isFound || Vector3.Distance(transform.position, monsterData.destPosition.position) > monsterData.distance)
            {
                //?????????? ??????????, ???? ???? ???? ???? ??
                //???? ????
                //?????? ?????? Attack ?????? ????
                monsterData.state = MonsterData.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //???? ???? 2.5?? ???????? ????
            if (attackTime >= 2.5f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //???? ?????????? ???? ?? (chase ?? || ???? collider ?????? ???? ??)
                if (monsterData.destPosition != null)
                {
                    // ???????? HP ????
                    // damaging player
                    Invoke("damaging", 0.83f);

                    Animating();
                }
            }
            else animator.SetBool("isIdle", true);
            //Wait until next frame
            yield return null;
        }

        // ???? ?????????? ????????.
        yield return null;
    }

    /*---------------------------------------------
     *              DAMAGE
     * -------------------------------------------*/


    // player?? monster ???? ???? ?? ????
    // If player damages monster this will be called
    public void Damage(float scale)
    {
        // ??????????
        animator.SetTrigger("Damaged");

        // ???? ?????? ???? ???? ??
        if (monsterData.curHp > 0)
        {
            //scale(-)???? ?????? ?????? ????????.
            monsterData.curHp -= scale;
            UpdateHpBar(monsterData.curHp);
        }
        // ???? ?????? ???? ??
        else
        {
            // ???? ??????????
            animator.SetBool("Dead", true);
            // ?????? ?????? ????
            DeleteHpBar();
            Invoke("Die", 1f);
        }

        print(gameObject.name + "'s cur hp: " + monsterData.curHp);
    }

    /*---------------------------------------------
     *              MOVE
     * -------------------------------------------*/

    public void Move()
    {
        //?????? ???? ????
        rigidBody.AddForce(direction * Time.deltaTime * monsterData.moveSpeed, ForceMode.VelocityChange);

        // ???? ???????? ??????
        transform.LookAt(Vector3.Lerp(transform.position, monsterData.destPosition.position, monsterData.turnSpeed * Time.deltaTime));
    }

    /*---------------------------------------------
     *              CHASE
     * -------------------------------------------*/

    public IEnumerator Chase()
    {
        //?????????? ?? ???? ??
        if (animator.GetBool("isWalk") == false)
        {
            //???? ??????????
            animator.SetBool("isWalk", true);
            animator.SetBool("isIdle", false);
        }

        //???? ????
        while (monsterData.state == MonsterData.States.Chase)
        {
            // ?????? ?? ?? ???? ??
            if (monsterData.isFound)
            {

                // ???????? ???????? ?????? ????
                monsterData.destPosition = player.GetComponent<Transform>();


                // ?????????? ???????????? ????
                Vector3 direction = (monsterData.destPosition.position - transform.position);

                //?????? ???? ????
                Move();

                //???????? ???? ???? ????(2f)?? ?????????? ????
                if (Vector3.Distance(transform.position, monsterData.destPosition.position) <= monsterData.distance)
                {
                    //?????? ?????? Attack ?????? ????
                    monsterData.state = MonsterData.States.Attack;

                    //???? ???? ????
                    StartCoroutine(Attack());
                    yield break;
                }

                //1?? ?? ???? ??????
                yield return new WaitForSeconds(1.0f);
            }

            //?????? ?? ?? ???? ??
            else if (!monsterData.isFound)
            {
                //?????? ?????? Idle ?????? ????
                monsterData.state = MonsterData.States.Idle;

                //Idle ???? ????
                StartCoroutine(Idle());
                yield break;
            }
        }



        //???? ?????????? ????????.
        yield return null;
    }

    /*---------------------------------------------
     *              Idle
     * -------------------------------------------*/

    public IEnumerator Idle()
    {
        // Idle
        while (monsterData.state == MonsterData.States.Idle)
        {
            // ??
            if (animator.GetBool("Idle") == false)
            {
                // ?????? ?????????? ????
                animator.SetBool("Idle", true);
                animator.SetBool("Walk", false);
            }

            // ?????????? ?????? ??
            if (monsterData.isFound)
            {
                // ?????? ?????? ???? ?????? ????
                monsterData.state = MonsterData.States.Chase;

                // ???
                StartCoroutine(Chase());
                yield break;
            }

            // Patrol ???????

            yield return new WaitForSeconds(1.0f);
        }
        // ???? ?????????? ????????.
        yield return null;
    }

}