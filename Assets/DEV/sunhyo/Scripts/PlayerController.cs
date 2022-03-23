using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public PlayerUI playerUI;
    public string playerName { get; set; }
    public int level { get; set; }
    public string cls { get; set; }
    public float hp { get; set; }
    public float mp { get; set; }
    public float maxHP { get; set; }
    public float maxMP { get; set; }
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;
    private RaycastHit hit;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        // 플레이어 정보 세팅 필요 (이름, 레벨, 클래스, hp, mp)
        playerName = "Test player";
        level = 10;
        cls = "Soldier";
        maxHP = 100.0f;
        maxMP = 50.0f;
        hp = maxHP - 10;
        mp = maxMP - 20;
    }

    void Start()
    {
        playerUI.nameTxt.text = playerName;
        playerUI.levelTxt.text = "Lv. " + level.ToString();
        playerUI.classTxt.text = cls;
        playerUI.hpBar.fillAmount = hp / maxHP;
        playerUI.mpBar.fillAmount = mp / maxMP;
    }

    void Update()
    {
        Debug.DrawRay(tr.position, Vector3.down * 0.1f, Color.yellow);

        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");

        if(h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * r);

        // Jump
        if(Input.GetButton("Jump") && Physics.Raycast(tr.position, Vector3.down, out hit, 0.1f))
            rigidBody.velocity = tr.up * jumpForce;
        
        // UseItem
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Item : HP Potion");
            hp = maxHP;
            playerUI.hpBar.fillAmount = hp / maxHP;
        }
            
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Item : MP Potion");
            mp = maxMP;
            playerUI.mpBar.fillAmount = mp / maxMP;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Item : Poision");
            hp -= 10;
            mp -= 5;
            playerUI.hpBar.fillAmount = hp / maxHP;
            playerUI.mpBar.fillAmount = mp / maxMP;
        }
            

        // UseSkill
        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log("Skill : Q");
        else if(Input.GetKeyDown(KeyCode.E))
            Debug.Log("Skill : E");
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            Debug.Log("Skill : LeftShift");
    }

    // void SetUI(float delta)
    // {
    //     hp += delta;
    // }
}