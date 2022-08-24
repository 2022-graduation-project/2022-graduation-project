using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    /* local data */
    public Rigidbody rigidBody;
    public CapsuleCollider capsuleCollider;
    public Animator animator;
    public Transform tr;
    public RaycastHit hit;

    private bool jumpable = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        /* 플레이어가 적절한 타이밍에 점프할 수 있도록 땅 체크 */
        Debug.DrawRay(tr.position + (Vector3.up * 0.1f), Vector3.down * 0.3f, Color.yellow);

        // Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");
        
        /* 수정 : gameManager에서 게임 가능 여부 판단 후 */
        Move(h, v, r);
        

        // Jump
        if (isJumpable()) Jump();
        else animator.SetBool("Jumping", false);
           
        Debug.DrawRay(transform.position + new Vector3(0f, 0.7f, 0.5f), transform.forward * 10f, Color.blue);
    }

    public void SetAnimation(string _type)
    {
        switch (_type)
        {
            case "Dead":
                break;
        }
    }

    private bool isJumpable()
    {
        if(Input.GetButton("Jump") && jumpable
            && Physics.Raycast(tr.position + (Vector3.up * 0.1f), Vector3.down, out hit, 0.1f))
            return true;
        else
            return false;
    }

    private void Jump()
    {
        animator.SetBool("Jumping", true);
        rigidBody.velocity = tr.up * DataManager.instance.playerData.jumpForce;
        jumpable = false;
        DataManager.instance.playerData.moveSpeed /= 2f;
        Invoke("AfterJump", 1.2f);
    }

    private void AfterJump()
    {
        jumpable = true;
        DataManager.instance.playerData.moveSpeed *= 2f;
    }

    private void Move(float h, float v, float r)
    {
        if (h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * DataManager.instance.playerData.moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * DataManager.instance.playerData.moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * 3.0f * r);
    }

    public void OnTriggerEnter(Collider other)
    {
        //if (other.name.Contains("ItemBag") || other.tag == "Chest")
        //    //UIManager.instance.SetItemUI(other.gameObject);

        //if (other.tag == "Chest")
        //{
        //    other.GetComponent<OpenChest>().isOpening = true;
        //    other.GetComponent<OpenChest>().isClosing = false;
        //}
    }

    public void OnTriggerExit(Collider other)
    {
        //if (other.name.Contains("ItemBag") || other.tag == "Chest")
        //    //UIManager.instance.ResetItemUI(other.gameObject);

        //if (other.tag == "Chest")
        //{
        //    other.GetComponent<OpenChest>().isOpening = false;
        //    other.GetComponent<OpenChest>().isClosing = true;
        //}
    }
}