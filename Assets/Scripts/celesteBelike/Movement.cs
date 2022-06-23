using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
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
    public float speed = 7;
    public float jumpForce = 12;
    public float slideSpeed = 1;
    public float wallJumpLerp = 5;
    public float dashSpeed = 40;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;


    [Space] 
    private bool onDashClick;
    private bool isJumping;
    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    // [Space]
    // [Header("Polish")]
    // public ParticleSystem dashParticle;
    // public ParticleSystem jumpParticle;
    // public ParticleSystem wallJumpParticle;
    // public ParticleSystem slideParticle;
    
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
        

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
                //anim.Flip(side*-1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if(Dir.x > .2f || Dir.x < -.2f)
            rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = Dir.y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, Dir.y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (Dir.x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (isJumping)
        {
            //anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if ( onDashClick  && !hasDashed)
        {
            if(DirRaw.x != 0 || DirRaw.y != 0)
                Dash(DirRaw.x, DirRaw.y);
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

        WallParticle(Dir.y);

        if (wallGrab || wallSlide || !canMove)
            return;

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

       // side = anim.sr.flipX ? -1 : 1;

        //jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
       // Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        //anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        //FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        //DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        //dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        //dashParticle.Stop();
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

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            //anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
         //anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
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
        //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
       // ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        //particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
       // var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
           // slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
           // main.startColor = Color.white;
        }
        else
        {
           // main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
}
