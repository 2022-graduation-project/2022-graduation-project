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

    private bool jumpable = true;

    /* runtime data */
    public Transform rightWeapon;
    public Transform leftWeapon;

    public Weapon weapon;

    void Start()
    {
        playerManager = PlayerManager.instance;

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        rightWeapon = transform.Find("Root").Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03")
                             .Find("Clavicle_R").Find("Shoulder_R").Find("Elbow_R").Find("Hand_R").Find("IndexFinger_01").Find("Weapon");
        leftWeapon = transform.Find("Root").Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03")
                             .Find("Clavicle_L").Find("Shoulder_L").Find("Elbow_L").Find("Hand_L").Find("IndexFinger_01").Find("Weapon");
        weapon = rightWeapon.GetChild(0).GetComponent<Weapon>();
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
            rigidBody.velocity = tr.up * playerManager.playerData.jumpForce;
            jumpable = false;
            playerManager.playerData.moveSpeed /= 2f;
            Invoke("AfterJump", 1.2f);
        }
        else if(Physics.Raycast(tr.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.1f))
            animator.SetBool("Jumping", false);
            

        // Attack
        if (playerManager.keyMoveable && Input.GetMouseButtonDown(0) 
            && Time.timeScale != 0)
            Attack();


        // Skill Attack
        if (Input.GetKey(KeyCode.G))
        {
            UseSkill();
        }
    }

    private void AfterJump()
    {
        jumpable = true;
        playerManager.playerData.moveSpeed *= 2f;
    }

    private void Move(float h, float v, float r)
    {
        if (h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * playerManager.playerData.moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * playerManager.playerData.moveSpeed * Time.deltaTime);
        if (playerManager.mouseMoveable) tr.Rotate(Vector3.up * 3.0f * r);
    }




    /************************************************/
    /*            외부 접근 가능 메소드               */
    /************************************************/

    public void Die()
    {
        playerManager.keyMoveable = playerManager.mouseMoveable = false;

        // 리스폰 장소로 소환
    }

    public void Attack()
    {
        // 일반 공격
        animator.SetTrigger("Attack");

        weapon.Attack(playerManager.playerData.STR);
    }

    public void Damaged(float damage)
    {
        playerManager.playerData.curHp -= damage;
        UIManager.instance.playerUI.UpdateHpBar(playerManager.playerData.maxHp, playerManager.playerData.curHp);
        if (playerManager.playerData.curHp <= 0)
            Die();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("ItemBag") || other.tag == "Chest")
            UIManager.instance.SetItemUI(other.gameObject);

        if (other.tag == "Chest")
        {
            other.GetComponent<OpenChest>().isOpening = true;
            other.GetComponent<OpenChest>().isClosing = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("ItemBag") || other.tag == "Chest")
            UIManager.instance.ResetItemUI(other.gameObject);

        if (other.tag == "Chest")
        {
            other.GetComponent<OpenChest>().isOpening = false;
            other.GetComponent<OpenChest>().isClosing = true;
        }
    }

    public void ChangeWeapon(string weaponName)
    {
        weaponName = "Axe";

        Destroy(rightWeapon.GetChild(0).gameObject);

        GameObject weaponObj = Instantiate(Resources.Load<GameObject>("Weapon/" + weaponName), rightWeapon);

        // weapon 변수 설정
        weapon = weaponObj.GetComponent<Weapon>();
    }




    /************************************************/
    /*                  오버라이딩                   */
    /************************************************/
    
    public virtual void UseSkill()
    {

    }
}