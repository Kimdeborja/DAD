using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D myRigidbody;

    private bool attack;

    [SerializeField]
    private Transform[] GroundPoints;

    [SerializeField]
    private float GroundRadius;

    [SerializeField]
    private LayerMask WhatIsGround;

    private bool IsGrounded;

    private bool jump;

    private bool jumpAttack;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

	private Vector2 startPos;

	public override void Start ()
    {

		base.Start();
		startPos = transform.position;
		 
        myRigidbody = GetComponent<Rigidbody2D>();
 

	}

    void Update()
    {
        HandleInput();
    }

   
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

        IsGrounded = Isgrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks();

        HandleLayers();

        ResetValues();

    
	}

    public void HandleMovement(float horizontal)
    {
        if (myRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("Land", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
			myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if (IsGrounded && jump)
        {
            IsGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            MyAnimator.SetTrigger("Jump");
        }

        MyAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
    }
   
    private void HandleAttacks()
    {
        if (attack && IsGrounded &&!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            MyAnimator.SetTrigger("Attack");
            myRigidbody.velocity = Vector2.zero;
        }
        if (jumpAttack && !IsGrounded && !this.MyAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            MyAnimator.SetBool("jumpAttack", true);
        }
        if (!jumpAttack && !this.MyAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            MyAnimator.SetBool("jumpAttack" , false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            jumpAttack = true;
        }
    }   

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
			ChangeDirection ();
        }
        
    }
    private void ResetValues()
    {
        attack = false;
        jump = false;
        jumpAttack = false;
    }

    private bool Isgrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in GroundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, GroundRadius, WhatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        MyAnimator.ResetTrigger("Jump");
                        MyAnimator.SetBool("Land", false);
                        return true;
                    }
                }

            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!IsGrounded)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }
}

