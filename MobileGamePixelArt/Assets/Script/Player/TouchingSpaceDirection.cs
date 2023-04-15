using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingSpaceDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float grounddistance = 0.05f;
    public float WallDistance = 0.2f;
    public float CeilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;

    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHit = new RaycastHit2D[5];
    RaycastHit2D[] CeilingHit = new RaycastHit2D[5];

    [SerializeField]
    private bool _IsGrouned = true;

    public bool IsGround
    {
        get
        {
            return _IsGrouned;
        }
        private set
        {
            _IsGrouned = value;
            animator.SetBool(AnimationString.IsGrounded, value);
        }
    }


    [SerializeField]
    private bool _IsOnCeiling = true;
    private Vector2 WallcheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _IsOnCeiling;
        }
        private set
        {
            _IsOnCeiling = value;
            animator.SetBool(AnimationString.IsOnCeiling, value);
        }
    }

    [SerializeField]
    private bool _IsOnWall = true;

    public bool IsOnWall
    {
        get
        {
            return _IsOnWall;
        }
        private set
        {
            _IsOnWall = value;
            animator.SetBool(AnimationString.IsOnWall, value);
        }
    }


    private void Awake()
    {
        animator= GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        IsGround = touchingCol.Cast(Vector2.down, contactFilter, groundHits, grounddistance) > 0;
        IsOnWall = touchingCol.Cast(WallcheckDirection, contactFilter, wallHit, WallDistance) > 0 ;
        IsOnCeiling = touchingCol.Cast(Vector2.up, contactFilter, CeilingHit, CeilingDistance) > 0;
    }
}
