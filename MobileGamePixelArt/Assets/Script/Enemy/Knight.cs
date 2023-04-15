using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingSpaceDirection))]
public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f;
    public DetectionZone detectionZone;
    Animator animator;

    Rigidbody2D rb;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    private TouchingSpaceDirection _touchingSpaceDirection;

    public WalkableDirection walkableDirection
    {
        get { return _walkDirection; }
        set 
        { 
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left) 
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; 
        }
    }

    public bool _HasTraget = false; 

    public bool HasTraget { get { return _HasTraget; } private set 
        {
            _HasTraget = value;
            animator.SetBool(AnimationString.hasTragtet, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _touchingSpaceDirection = GetComponent<TouchingSpaceDirection>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTraget = detectionZone.detectColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(_touchingSpaceDirection.IsGround && _touchingSpaceDirection.IsOnWall)
        {
            FlipDirection();
        }

        if(CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * Vector2.right.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,0, walkStopRate) , rb.velocity.y);
        }

    }

    private void FlipDirection()
    {
        if(_walkDirection == WalkableDirection.Right)
        {
            _walkDirection= WalkableDirection.Left;
        }
        else if(_walkDirection == WalkableDirection.Left)
        {
            _walkDirection= WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set legal values of right or left");
        }
    }

}
