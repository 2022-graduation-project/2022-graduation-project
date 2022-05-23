using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{
    /* Manager */
    private PlayerManager playerManager;


    /* local data */
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;
    private RaycastHit hit;



    /************************************************/
    /*           가변적인 플레이어 데이터             */
    /*          얘네도 json으로 보내야 함             */
    /************************************************/
    private float curHp;
    private float curMp;

    private float moveSpeed = 5.0f;
    private float turnSpeed = 3.0f;
    private float jumpForce = 4.0f;
    private bool jumpable = true;



    void Start()
    {
        playerManager = PlayerManager.instance;

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(tr.position + (Vector3.up * 0.1f), Vector3.down * 0.3f, Color.yellow);


        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");

        if (playerManager.keyMoveable)
            Move(h, v, r);


        // Jump
        if (playerManager.keyMoveable && Input.GetButton("Jump") && jumpable
            && Physics.Raycast(tr.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.1f))
        {
            animator.SetBool("Jumping", true);
            rigidBody.velocity = tr.up * jumpForce;
            jumpable = false;
            moveSpeed /= 2f;
            Invoke("AfterJump", 1.2f);
        }
        else if(Physics.Raycast(tr.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.1f))
            animator.SetBool("Jumping", false);
            

        // Attack
        if (playerManager.keyMoveable && Input.GetMouseButtonDown(0))
            Attack();
    }

    void AfterJump()
    {
        jumpable = true;
        moveSpeed *= 2f;
    }

    void Move(float h, float v, float r)
    {
        if (h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        if (playerManager.mouseMoveable) tr.Rotate(Vector3.up * turnSpeed * r);
    }




    /************************************************/
    /*            외부 접근 가능 메소드               */
    /************************************************/

    public void Die()
    {
        playerManager.keyMoveable = playerManager.mouseMoveable = false;
        // if(Inventory.MasterPotion) 부활
        // else 주사위 던지기
    }

    public void Attack()
    {
    }

    public void Damaged(float damage)
    {
        curHp += damage;
        UIManager.instance.playerUI.UpdateHpBar(playerManager.playerData.maxHp, curHp);
        if (curHp <= 0)
            Die();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("ItemBag"))
            playerManager.GetItemBag(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("ItemBag"))
            playerManager.LeaveItemBag();
    }
}