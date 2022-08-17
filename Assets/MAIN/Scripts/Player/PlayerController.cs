using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/**************************************************************************
            플레이어의 이동 및 상호작용을 담당하는 스크립트입니다.
    플레이어의 데이터와 관련된 코드들은 PlayerManager 스크립트를 참고해주세요.
***************************************************************************/

public class PlayerController : MonoBehaviour
{
    /* Manager */
    // protected PlayerManager playerManager;

    /* local data */
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected Animator animator;
    protected Transform tr;
    protected RaycastHit hit;

    public bool canDamage = false;
    protected bool canUseSkill = true;
    private bool jumpable = true;
    private bool isDead = false;

    /* runtime data */
    public Transform rightWeapon;
    public Transform leftWeapon;

    public GameObject weaponObj;
    public Weapon weapon;

    void Start()
    {
        // playerManager = PlayerManager.instance;

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        weapon = weaponObj.transform.GetChild(0).GetComponent<Weapon>();
    }

    void Update()
    {
        /* 플레이어가 적절한 타이밍에 점프할 수 있도록 땅 체크 */
        //Debug.DrawRay(tr.position + (Vector3.up * 0.1f), Vector3.down * 0.3f, Color.yellow);


        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");
        
        if (PlayerManager.instance.keyMoveable) Move(h, v, r);
        

        // Jump
        if (isJumpable()) Jump();
        else animator.SetBool("Jumping", false);
            

        // 일반 공격
        if (isAttackable())
            Attack();


        Debug.DrawRay(transform.position + new Vector3(0f, 0.7f, 0.5f), transform.forward * 10f, Color.blue);
        // Skill Attack
        // 스킬 공격 (단축키는 추후 변경)
        if (isSkillAccepted() && Input.GetKeyDown(KeyCode.G))
        {
            UseSkill();
        }


        // Skill Attack2
        // 스킬2 (단축키는 추후 변경)
        if (isSkillAccepted() && Input.GetKeyDown(KeyCode.H))
        {
            UseSkill2();
        }

        // Skill Attack3
        // 스킬3 (단축키는 추후 변경)
        if (isSkillAccepted() && Input.GetKeyDown(KeyCode.T))
        {
            UseSkill3();
        }
    }

    private bool isJumpable()
    {
        if(PlayerManager.instance.keyMoveable && Input.GetButton("Jump") && jumpable
            && Physics.Raycast(tr.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.1f))
            return true;
        else
            return false;
    }

    private void Jump()
    {
        animator.SetBool("Jumping", true);
        rigidBody.velocity = tr.up * PlayerManager.instance.playerData.jumpForce;
        jumpable = false;
        PlayerManager.instance.playerData.moveSpeed /= 2f;
        Invoke("AfterJump", 1.2f);
    }

    private bool isAttackable()
    {
        if (PlayerManager.instance.mouseMoveable && PlayerManager.instance.keyMoveable && Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            return true;
        else
            return false;
    }
    
    private bool isSkillAccepted()
    {
        if(PlayerManager.instance.mouseMoveable && Input.GetKeyDown(KeyCode.G) && canUseSkill == true) return true;
        else return false;
    }

    private void AfterJump()
    {
        jumpable = true;
        PlayerManager.instance.playerData.moveSpeed *= 2f;
    }

    private void Move(float h, float v, float r)
    {
        if (h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * PlayerManager.instance.playerData.moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * PlayerManager.instance.playerData.moveSpeed * Time.deltaTime);
        if (PlayerManager.instance.mouseMoveable) tr.Rotate(Vector3.up * 3.0f * r);
    }




    /************************************************/
    /*            외부 접근 가능 메소드               */
    /************************************************/

    public void Die()
    {
        print("죽음");
        PlayerManager.instance.keyMoveable = PlayerManager.instance.mouseMoveable = false;
        gameObject.transform.position = Vector3.zero;
        animator.SetTrigger("Dead");
        isDead = true;

        // 리스폰 장소로 소환
    }

    /*****************************************
     * 근접 공격
     * 
     * @ param - 피해량 (0 이상의 값)
     * @ return - X
     * @ exception - X
    ******************************************/
    public void Attack()
    {
        animator.SetTrigger("Attack");
        weapon.Attack(PlayerManager.instance.playerData.STR, this);
    }





    /*****************************************
     * damage(양수) 만큼 현재 체력에서 차감
     * 
     * @ param - 피해량 (0 이상의 값)
     * @ return - X
     * @ exception - X
    ******************************************/
    public void Damaged(float damage)
    {
        if (isDead)
            return;


        if(canDamage==true)
        {
            // 쉴드 포션 쓴 상태에서 대미지 입었을 때 나타나는 이펙트 자리
            print("Could not Damaged by Monster because of shield potion effect.");
            return;
        }

        PlayerManager.instance.playerData.curHp -= damage;
        print("curHP: "+ PlayerManager.instance.playerData.curHp);
        //UIManager.instance.playerUI.UpdateHpBar(playerManager.playerData.maxHp, playerManager.playerData.curHp);
        if (PlayerManager.instance.playerData.curHp <= 0)
            Die();
    }

    

    /*****************************************
     * consume(양수) 만큼 현재 MP에서 차감
     * 
     * @ param - 소모량 (0 이상의 값)
     * @ return - X
     * @ exception - X
    ******************************************/
    public void ConsumeMP(float scale)
    {
        PlayerManager.instance.playerData.curMp -= scale;
        print("curMP: "+ PlayerManager.instance.playerData.curMp);
        //UIManager.instance.playerUI.UpdateMpBar(playerManager.playerData.maxMp, playerManager.playerData.curMp);
        if (PlayerManager.instance.playerData.curMp <= 0)
            canUseSkill = false;
    }





    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("ItemBag") || other.tag == "Chest")
            //UIManager.instance.SetItemUI(other.gameObject);

        if (other.tag == "Chest")
        {
            other.GetComponent<OpenChest>().isOpening = true;
            other.GetComponent<OpenChest>().isClosing = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("ItemBag") || other.tag == "Chest")
            //UIManager.instance.ResetItemUI(other.gameObject);

        if (other.tag == "Chest")
        {
            other.GetComponent<OpenChest>().isOpening = false;
            other.GetComponent<OpenChest>().isClosing = true;
        }
    }


    /*****************************************
     * 들고 있는 무기 변경
     * 
     * @ param - 무기 이름 (무기 prefab 이름과 동일해야 함)
     * @ return - X
     * @ exception - X
    *****************************************/
    public void ChangeWeapon(string weaponName)
    {
        //weaponName = "Axe"; // null 방지 임시 변수
        //Destroy(rightWeapon.GetChild(0).gameObject);
        //GameObject weaponObj = Instantiate(Resources.Load<GameObject>("Weapon/" + weaponName), rightWeapon);
        //weapon = weaponObj.GetComponent<Weapon>();
    }




    /************************************************/
    /*                  오버라이딩                   */
    /************************************************/

    public virtual void UseSkill()
    {

    }
    public virtual void UseSkill2()
    {

    }
    public virtual void UseSkill3()
    {

    }

    
    
    
}