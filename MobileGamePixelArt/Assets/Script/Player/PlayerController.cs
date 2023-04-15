using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingSpaceDirection))]

public class PlayerController : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float AirSpeed = 3f;
    public float jumpinpulse = 10f;

    Vector2 moveInput;
    TouchingSpaceDirection touchingSpace;

    public float CurrentMoveSpeed { 
        get 
        {
            if (CanMove)
            {
                if (IsMoving && !touchingSpace.IsOnWall)
                {
                    if (touchingSpace.IsGround)
                    {
                        if (IsRunning)
                        {
                            return RunSpeed;
                        }
                        else
                        {
                            return WalkSpeed;
                        }
                    }
                    else
                    {
                        return AirSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        } 
    }

    [SerializeField]
    private bool _isMoving= false;


    public bool IsMoving { get 
        { 
            return _isMoving;
        }
        private set
        { 
            _isMoving = value;
            animator.SetBool(AnimationString.IsMoving, value);
        } 
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.IsRunning, value);
        }
    }

    public bool _isFacingRigth;

    public bool IsFacingRight { 
        get 
        { 
            return _isFacingRigth;  
        } 
        private set 
        { 
            if(_isFacingRigth != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRigth = value;
        } 
    }

    public bool CanMove {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        } 
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.IsAlive);
        }

    }

    Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        touchingSpace = GetComponent<TouchingSpaceDirection>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationString.YVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();


        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirecetion(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirecetion(Vector2 moveInput)
    {
        if(moveInput.x < 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x > 0 && IsFacingRight)
        {
            IsFacingRight = false;

        }
    }



    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingSpace.IsGround && CanMove)
        {
            animator.SetTrigger(AnimationString.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpinpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attack1);
        }
    }
}
