﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Collision))]
[RequireComponent (typeof(BetterJumping))]
public class Movement : MonoBehaviour
{
    private Collision coll;
    private Vector2 Dir;
    private Vector2 DirRaw;
    [HideInInspector]
    public Rigidbody2D rb;
    

    [Space]
    [Header("Stats")]
    public float speed;
    public float jumpForce;
    public float wallJumpLerp;
    public float dashSpeed;

    [Space]
    [Header("Booleans")]
    public bool canMove=true;
    public bool wallJumped;
    public bool isDashing;


    [Space] 
    private bool onDashClick;
    private bool isJumping;
    private bool groundTouch;
    private bool hasDashed;

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
       DirRaw = ctx.ReadValue<Vector2>();
       Dir = DirRaw.normalized;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isJumping = true;
           // isJumpingCd();
        }

        if (ctx.canceled)
        {
            isJumping = false;
        }
    }
    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onDashClick = true;
        }

        if (ctx.canceled)
        {
            onDashClick = false;
        }
    }
    void Update()
    {
        Vector2 dir = new Vector2(Dir.x,Dir.y);

        Walk(dir);

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if (isJumping)
        {
            if (coll.onGround)
                Jump(Vector2.up, false);
            
                
        }

        if ( onDashClick  && !hasDashed)
        {
            Debug.Log(onDashClick);
            if(DirRaw.x != 0 || DirRaw.y != 0) 
                Dash(DirRaw.x, DirRaw.y);
            Debug.Log(onDashClick);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
        
        if(Dir.x > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
            
        }
        if (Dir.x < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
            
        }


    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

    }

    private void Dash(float x, float y)
    {
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;
        
        yield return new WaitForSeconds(.3f);
        
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }
    IEnumerator isJumpingCd()
    {
        isJumping = false;
        yield return new WaitForSeconds(.15f);
    }

    
    

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    

    
}
