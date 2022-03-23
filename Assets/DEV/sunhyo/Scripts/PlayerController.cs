using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData
{
    // Info
    public string playerName { get; set; }
    public int level { get; set; }
    public string cls { get; set; }
    public float hp { get; set; }
    public float mp { get; set; }
    public float maxHP { get; set; }
    public float maxMP { get; set; }

    public float moveSpeed = 5.0f;
    public float turnSpeed = 3.0f;
    public float jumpForce = 5.0f;

    // Inventory
    public class Inventory
    {
        public int HpPotion { get; set; }
        public int MpPotion { get; set; }
        public int MasterPotion { get; set; }
    }
}

public class PlayerController : MonoBehaviour
{
    private PlayerData player;
    private PlayerData.Inventory inventory;
    [SerializeField] private PlayerUI playerUI;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;
    private RaycastHit hit;

    void Start()
    {
        player = new PlayerData();
        inventory= new PlayerData.Inventory();

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        // 플레이어 정보 세팅 필요 (이름, 레벨, 클래스, hp, mp)
        player.playerName = "Test player";
        player.level = 10;
        player.cls = "Soldier";
        player.maxHP = 100.0f;
        player.maxMP = 50.0f;
        player.hp = player.maxHP - 10;
        player.mp = player.maxMP - 20;

        inventory.HpPotion = 1;
        inventory.MpPotion = 1;
        inventory.MasterPotion = 1;

        // Set UI
        playerUI.nameTxt.text = player.playerName;
        playerUI.levelTxt.text = "Lv. " + player.level.ToString();
        playerUI.classTxt.text = player.cls;
        playerUI.hpBar.fillAmount = player.hp / player.maxHP;
        playerUI.mpBar.fillAmount = player.mp / player.maxMP;
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

        tr.Translate(Vector3.right * h * player.moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * player.moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * player.turnSpeed * r);

        // Jump
        if(Input.GetButton("Jump") && Physics.Raycast(tr.position, Vector3.down, out hit, 0.1f))
            rigidBody.velocity = tr.up * player.jumpForce;
        
        // UseItem
        if(Input.GetKeyDown(KeyCode.Alpha1) && inventory.HpPotion > 0)
        {
            Debug.Log("Item : HP Potion");
            inventory.HpPotion--;

            player.hp = player.maxHP;
            playerUI.hpBar.fillAmount = player.hp / player.maxHP;
        }
            
        else if(Input.GetKeyDown(KeyCode.Alpha2) && inventory.MpPotion > 0)
        {
            Debug.Log("Item : MP Potion");
            inventory.MpPotion--;

            player.mp = player.maxMP;
            playerUI.mpBar.fillAmount = player.mp / player.maxMP;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3) && inventory.MasterPotion > 0)
        {
            Debug.Log("Item : Poision");
            inventory.MasterPotion--;

            player.hp -= 10;
            player.mp -= 5;
            playerUI.hpBar.fillAmount = player.hp / player.maxHP;
            playerUI.mpBar.fillAmount = player.mp / player.maxMP;

            if(player.)
        }
            
        // UseSkill
        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log("Skill : Q");
        else if(Input.GetKeyDown(KeyCode.E))
            Debug.Log("Skill : E");
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            Debug.Log("Skill : LeftShift");
    }

    void Die()
    {

    }

    void Attack()
    {

    }

    void Damaged(float damage)
    {
        player.hp -= damage;
        if(player.hp <= 0)
            Die();
    }
}