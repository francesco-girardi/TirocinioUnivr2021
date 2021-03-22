using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Animation.Player {
public class PlayerAnimator : CharacterAnimator
{
    Animator anim;
    SimpleCharacterController controller;
    private float baseMoveSpeed;
    private float sprintSpeed;
    private float crouchSpeed;
    private bool isWalking;
    private bool isRunning;
    private bool isCrouched;
    private bool isDashing;
    private bool jumped;
    private bool isGrounded;

    // Start is called before the first frame update
    protected override void Start()
    {   
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<SimpleCharacterController>();
        
        // animator = GetComponentInChildren<Animator>();
        // base.Start();                                        //momentaneo finchè non mettiamo le animazioni di attacco o finchè non capisco meglio
    }

    // Update is called once per frame
    protected void Update()
    {   
        baseMoveSpeed = controller.BaseMoveSpeed;
        sprintSpeed = controller.RunSpeed;
        crouchSpeed = controller.CrouchSpeed;

        anim.SetFloat("walkSpeed", baseMoveSpeed/1.8f);
        anim.SetFloat("runSpeed", sprintSpeed/18f);
        anim.SetFloat("crouchSpeed", crouchSpeed/2.5f);    // DA IMPOSTARE IL NUMERO POI

        isWalking = controller.IsWalking;
        isRunning = controller.IsRunning;
        isCrouched = controller.IsCrouched;
        isDashing = controller.IsDashing;
        jumped = controller.Jumped;
        isGrounded = controller.IsGrounded;

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
            isWalking = false;
            isRunning = false;
        }

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isCrouched", isCrouched);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("jumped", jumped);
        anim.SetBool("isGrounded", isGrounded);
    }
}
}
