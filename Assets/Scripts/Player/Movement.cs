using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent (typeof(Collision))]
[RequireComponent (typeof(BetterJumping))]
public class Movement : MonoBehaviour
{
    
    private Collision coll;
    private Vector2 Dir;
    private Vector2 DirRaw;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public bool GhostTrail=false,maxJumpBool=false,isJumping;
    //<>


    [Space]
    [Header("Stats")]
    public float speed;
    public float jumpForce;
    public float dashSpeed;
    public float TimeDash;
    public float coyoteTime;
    private float coyoteTimeCounter;
    private float TimeDashLine;

    [Space]
    [Header("Booleans")]
    public bool canMove=true;
    public bool canJump;
    public bool isDashing;
    public bool HasADash;



    [Space] 
    private Animator anim;
    private SpriteRenderer sR;
    private bool onDashClick;
    private bool groundTouch;
    private bool hasDashed;
    [Space] 
    public Sprite FallenWhite;
    public Sprite FallenPurple;
    
    

    void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();

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
            coyoteTimeCounter = 0f;
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
        var check = !HasADash ? sR.sprite = FallenPurple : sR.sprite = FallenWhite;
        

        if (coll.onGround && !isDashing)
        {
            GetComponent<BetterJumping>().enabled = true;
        }

        if (coll.onGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (isJumping&&canJump&& coyoteTimeCounter>0)
        {
            GhostTrail=true;
            Jump(Vector2.up);
        }

        if ( onDashClick  && !hasDashed && HasADash &&!groundTouch)
        {
            if (DirRaw.x != 0 || DirRaw.y != 0)
            {
                GhostTrail=true;
                Dash(DirRaw.x, DirRaw.y);
                HasADash = false;
            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
        
        if(Dir.x > 0)
        {
            sR.flipX=false;
            
        }
        if (Dir.x < 0)
        {
            sR.flipX=true;
            
        }


    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        HasADash = true;
        groundTouch = true;
        GhostTrail = false;

    }

    private void Dash(float x, float y)
    {
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        hasDashed = true;
        canMove = false;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y).normalized;
        var lineJump = new Vector2(0, 1);
        if (dir == lineJump)
        {
            rb.velocity = dir * (dashSpeed);
        }
        else
        {
            rb.velocity = dir * dashSpeed;
        }
        StartCoroutine(DashWait(TimeDash));
    }

    IEnumerator DashWait(float time)
    {
        StartCoroutine(GroundDash());
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        isDashing = true;
        yield return new WaitForSeconds(time);
        rb.velocity = rb.velocity/=2;
        canMove = true;
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        isDashing = false;
        hasDashed = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }
    

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        else
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        
    }

    private void Jump(Vector2 dir)
    {
        maxJumpBool = true;
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
