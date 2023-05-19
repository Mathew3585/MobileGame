using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    private int _maxHeal = 100;

    [SerializeField]
    private int Damage;

    public int MaxHealth
    {
        get 
        { 
            return _maxHeal; 
        }
        set 
        { 
            _maxHeal = value; 
        }
    }

    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    private bool IsInvicible = false;
    private float timeSinvceHit = 0;
    private float invicubiltyTimer = 0.25f;


    public bool IsAlive
    {
            get 
            { 
                return _isAlive;
            }
            set 
            {
                _isAlive = value;
                animator.SetBool(AnimationString.IsAlive, value);
            }
    }



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsInvicible)
        {
            if(timeSinvceHit > invicubiltyTimer)
            {
                IsInvicible = false;
                timeSinvceHit = 0;
            }
            timeSinvceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 Knockback)
    {
        if(IsAlive && !IsInvicible)
        {
            Health -= damage;
            IsInvicible = true;
            return true;
        }
        return false;
    }   
}
