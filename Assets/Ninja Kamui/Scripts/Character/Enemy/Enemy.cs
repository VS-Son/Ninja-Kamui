using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Character
{
   [SerializeField] private float attackRange;
   [SerializeField] private float moveSpeed;
   [SerializeField] private RectTransform canvasHealth;
    private IState currentState;
    private Character target;
    public Character Target => target;
    private bool isRight;
   
    
    private void Update()
    {
       if (currentState !=null)
       {
          currentState.OnExecute(this);
       }

    }
    protected override void Start()
   {
      base.Start();
   }

   protected override void OnInit()
   {
      base.OnInit();
      ChangeState(new IdleState() );
   }
   protected override void OnDespawn()
   {
      base.OnDespawn();
      Destroy(gameObject);
   }
   protected override void OnDeath()
   {
       ChangeState(null);
        base.OnDeath();
   }

   public void ChangeState(IState state)
   {
      if (currentState != null)
      {
         currentState.OnExit(this);
      }

      currentState = state;
      if (currentState!=null)
      {
         currentState.OnEnter(this);
      }
   }
   public void Moving()
   {
     ChangeAnim("Run");
     Rb2D.velocity = transform.right * moveSpeed;
   }

   public void StopMoving()
   {
       Rb2D.velocity = Vector2.zero;
            ChangeAnim("Idle");
   }

   public void Attack()
   {
      ChangeAnim("Attack");
   }

   internal void SetTarget(Character character)
   {
      this.target = character;
      if (IsTargetInRange())
      {
         ChangeState(new AttackState());
      }
      else if (Target !=null)
      {
         ChangeState(new PatrolState());
      }
      else
      {
         ChangeState( new IdleState());
      }
   }

   public bool IsTargetInRange()
   {
      if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
      {
         return true;
      }
      else
      {
         return false;
      }
          
      
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("EnemyWall"))
      {
         ChangeDirection(!isRight);
      }
   }

   public void ChangeDirection(bool isRight)
   {
      this.isRight = isRight;
      transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
      canvasHealth.transform.localScale = isRight ? new Vector3(0.003f, 0.003f, 0.003f) : new Vector3(-0.003f, 0.003f, 0.003f);

   }
   
}
