using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private Transform tr;

    [SerializeField]private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private float jumpForce = 5.0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>(); 
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");

        if(h != 0 || v != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * r);

        if(Input.GetButton("Jump"))
            rigidBody.AddForce(Vector3.up * jumpForce);
    }
}
