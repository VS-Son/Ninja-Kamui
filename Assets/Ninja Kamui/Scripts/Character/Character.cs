using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float hp;
    protected Rigidbody2D Rb2D;
    private bool IsDead => hp <= 0;
    private string _currentAnim;
    private Animator _animator;
    public HealthBar healthBar;
    [SerializeField] protected TextCombat textCombat;
    
    protected virtual void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        OnInit();
    }

    protected virtual void OnInit()
    {
        hp = 100f;
        healthBar.OnInit(100);
    }

    protected virtual void OnDespawn()
    {
        
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("Dead");
        Invoke(nameof(OnDespawn), 2f);
    }

    public void OnHit(float damage)
    {
        Debug.Log("On Hit");
        if (!IsDead)
        {
            hp -= damage;
            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
            textCombat.gameObject.SetActive(true);
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (_currentAnim == animName) return;
        _currentAnim = animName;
        _animator.SetTrigger(_currentAnim); 
    }

    protected virtual void EndAttack()
    {
        ChangeAnim("Idle");
    }
    
    protected virtual void EndDead()
    {
        ChangeAnim("Idle");
        OnInit();
    }
}
