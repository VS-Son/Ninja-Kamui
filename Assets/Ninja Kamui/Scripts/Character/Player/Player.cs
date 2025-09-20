using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform feetPos;
    [SerializeField] private Vector2 checkSizeFeet;
    [SerializeField] private Kunai kunai;
    [SerializeField] private Transform pointThrow;
    
    private float moveX;
    private Vector2 posStarting;
    private int coin = 0;

    private bool isJumping;
    private bool isAttacking;
    private bool isThrowing;
    private bool isDead;    
    private bool _isRuning = true; 
    private bool _isGrounded;
    private bool _isRightDirection;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }

    protected override void Start()
    {
        
        base.Start();
        SavePoint();
        UiManager.instance.SetCoin(coin);
    }

    private bool IsGround()
    {
        return Physics2D.OverlapBox(feetPos.position, checkSizeFeet, 0, layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPos.position, checkSizeFeet);
    }

    private void Update()
    {
        // if (!isAttacking)
        // {
        //     moveX = Input.GetAxisRaw("Horizontal");
        // }
        // else
        // {
        //     moveX = 0;
        // }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            isJumping = true;
        }
        else if(Input.GetKeyDown(KeyCode.C) && !isAttacking)
        {
            isAttacking = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && !isThrowing)
        {
            isThrowing = true;
            Instantiate(kunai, pointThrow.position, pointThrow.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Coin":
                coin++;
                PlayerPrefs.SetInt("coin", coin);
                UiManager.instance.SetCoin(coin);
                Debug.Log(col.gameObject.name);
                Destroy(col.gameObject);
                break;
            case "DeathZone":
                isDead = true;
                ChangeAnim("Dead");
                Debug.Log(col.gameObject.name);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            ChangeAnim("Attack");
        }
        if (isThrowing)
        {
            ChangeAnim("Throw");
        }
        if(isAttacking|| isThrowing|| isDead ) return;
        _isGrounded = CheckGrounded();
        Rb2D.velocity = new Vector2(moveX * speed, Rb2D.velocity.y);
        if (isJumping && _isGrounded)
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
            isJumping = false;
        }

        if (!_isGrounded)
        {
            switch (Rb2D.velocity.y)
            {
                case > 0.1f:
                {
                    ChangeAnim("Jump");
                    Rb2D.gravityScale = 1f;
                    if (Mathf.Abs(moveX) > 0.1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, moveX > 0 ? 0 : 180, 0));
                    }

                    break;
                }
                case < -0.1f when !isAttacking:
                {
                    ChangeAnim("Fall");
                    Rb2D.gravityScale = 1.3f; 
                    if (Mathf.Abs(moveX) > 0.1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, moveX > 0 ? 0 : 180, 0));
                    }

                    break;
                }
            }
        }   
        else
        {
            switch (Mathf.Abs(moveX))
            {
                case > 0.1f:
                    _isRuning = true;
                    transform.rotation = Quaternion.Euler(new Vector3(0, moveX > 0 ? 0 : 180, 0));
                    ChangeAnim("Run");
                    break;
                default:
                    Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
                    ChangeAnim("Idle");
                    break;
            }
        }
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, layerMask);
        return hit.collider != null;
    }
    internal void SavePoint()
    {
        posStarting = transform.position;
    }
    protected override void EndAttack()
    {
        base.EndAttack();
        isAttacking = false;
        isThrowing = false;
    }
    
    protected override void EndDead()
    {
        base.EndDead();
        isDead = false;
        transform.position = posStarting;
    }

    public void SetMove(float horizontal)
    {
        this.moveX = horizontal;
    }

    public void Jump()
    {
        if(!_isGrounded) return;
        isJumping = true;

    }
    public void SetAttack()
    {
        isAttacking = true;
    }
    public void SetThrow()
    {
        if(isThrowing) return;
        isThrowing = true;
        Instantiate(kunai, pointThrow.position, pointThrow.rotation);

    }
}
    