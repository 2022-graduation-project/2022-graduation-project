﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private Inventory inventory;
    private Skill skill;
    private PlayerUI playerUI;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;
    private RaycastHit hit;



    /************************************************/
    /*           가변적인 플레이어 데이터             */
    /************************************************/
    private float curHp;
    private float curMp;

    private float moveSpeed = 5.0f;
    private float turnSpeed = 3.0f;
    private float jumpForce = 5.0f;



    /************************************************/
    private Dictionary<string, PlayerData> playerDict;
    /************************************************/


    void Start()
    {
        playerDict = DataManager.instance
                    .LoadJsonFile<Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player");

        playerData = playerDict["000_player"];
        playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        playerUI.Set(playerData);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(tr.position, Vector3.down * 0.1f, Color.yellow);

        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");

        if (GameManager.instance.keyMoveable)
            Move(h, v, r);

        // Jump
        if (GameManager.instance.keyMoveable && Input.GetButton("Jump") && Physics.Raycast(tr.position, Vector3.down, out hit, 0.1f))
            rigidBody.velocity = tr.up * jumpForce;

        // Attack
        if (GameManager.instance.keyMoveable && Input.GetMouseButtonDown(0))
            Attack();
    }

    void Move(float h, float v, float r)
    {
        if (h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        if (GameManager.instance.mouseMoveable) tr.Rotate(Vector3.up * turnSpeed * r);
    }



    /************************************************/
    /*            외부 접근 가능 메소드               */
    /************************************************/

    public void Die()
    {
        GameManager.instance.keyMoveable = GameManager.instance.mouseMoveable = false;
        // if(Inventory.MasterPotion) 부활
        // else 주사위 던지기
    }

    public void Attack()
    {
    }

    public void Damaged(float damage)
    {
        curHp += damage;
        playerUI.UpdateHpBar(playerData.maxHp, curHp);
        if (curHp <= 0)
            Die();
    }

    public void UseItem(int item)
    {

    }

    public void BuyItem(int price)
    {
        playerData.money -= price;
        playerUI.UpdateMoney(playerData.money);
    }

    public void TakeItem(GameObject obj)
    {

    }
}