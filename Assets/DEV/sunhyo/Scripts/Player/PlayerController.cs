using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{
    private PlayerData player;
    private Inventory inventory;
    private Skill skills;
    private PlayerUI playerUI;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;
    private RaycastHit hit;

    private bool moveable = true;

    void Start()
    {
        player = new PlayerData();
        inventory = new Inventory();
        skills = new Skill();
        playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        // 플레이어 정보 세팅 필요 (이름, 레벨, 클래스, hp, mp)
        // player.playerName = "Test player";
        // player.level = 10;
        // player.cls = "Soldier";
        // player.maxHP = 100.0f;
        // player.maxMP = 50.0f;
        // player.hp = player.maxHP - 10;
        // player.mp = player.maxMP - 20;
        player.Set();

        inventory.HpPotion = 1;
        inventory.MpPotion = 1;
        inventory.MasterPotion = 1;

        // Set UI
        playerUI.nameTxt.text = player.playerName;
        playerUI.levelTxt.text = "Lv. " + player.level.ToString();
        playerUI.classTxt.text = player.cls;
        playerUI.moneyTxt.text = player.money.ToString();
        playerUI.hpBar.fillAmount = player.hp / player.maxHP;
        playerUI.mpBar.fillAmount = player.mp / player.maxMP;

        playerUI.items[0].text = inventory.HpPotion.ToString();
        playerUI.items[1].text = inventory.MpPotion.ToString();
        playerUI.items[2].text = inventory.MasterPotion.ToString();
    }

    void Update()
    {
        Debug.DrawRay(tr.position, Vector3.down * 0.1f, Color.yellow);

        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");

        if(moveable)
            Move(h, v, r);

        // Jump
        if(moveable && Input.GetButton("Jump") && Physics.Raycast(tr.position, Vector3.down, out hit, 0.1f))
            rigidBody.velocity = tr.up * player.jumpForce;

        if(moveable && Input.GetMouseButtonDown(0))
            Attack();
        
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

            Damaged(-10);
        }
            
        // UseSkill
        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log("Skill : Q");
        else if(Input.GetKeyDown(KeyCode.E))
            Debug.Log("Skill : E");
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            Debug.Log("Skill : LeftShift");
    }

    void Move(float h, float v, float r)
    {
        if(h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * player.moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * player.moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * player.turnSpeed * r);
    }

    public void Die()
    {
        moveable = false;
        // if(Inventory.MasterPotion) 부활
        // else 주사위 던지기
    }

    public void Attack()
    {
        moveable = false;
        animator.SetBool("Shooting", true);
        Invoke("StopShooting", 1);
    }

    void StopShooting()
    {
        animator.SetBool("Shooting", false);
        moveable = true;
    }

    public void Damaged(float damage)
    {
        player.hp += damage;
        playerUI.hpBar.fillAmount = player.hp / player.maxHP;
        if(player.hp <= 0)
            Die();
    }

    public void UseItem(int item)
    {

    }

    public void UseSkill()
    {

    }

    public void ButyItem(int price)
    {
        player.money -= price;
        playerUI.moneyTxt.text = player.money.ToString();
    }

    public void GetQuest()
    {

    }
}