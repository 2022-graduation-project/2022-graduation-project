using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* local data */
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected Animator animator;
    protected Transform tr;
    protected RaycastHit hit;

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

        Move(h, v, r);


        // Jump
        if (isJumpable())
        {
            rigidBody.velocity = tr.up * playerData.jumpForce;
            playerData.moveSpeed /= 2f;
            Invoke("AfterJump", 1.2f);
        }
        else
            animator.SetBool("Jumping", false);
    }
}