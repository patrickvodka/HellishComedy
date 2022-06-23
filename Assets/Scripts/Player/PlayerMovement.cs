using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private Controls controls;
    private InputAction move;
    public float speed;
    private Rigidbody2D Rb2d;
    private Vector2 Direction;
    public float jumpForce;
    private float moveInput;
    private bool isGrounded;
    public Transform FeetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimerCounter;
    public float jumpTime;
    private bool isJumping;
    private bool InputJump;
    private Animator anim;
    
    private void Awake()
    {
        controls = new Controls();
        //anim = GetComponent<Animator>();
       Rb2d= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();

       // controls.Player.Jump.performed += DoJump;
        //controls.Player.Jump.Enable();
    }
    private void FixedUpdate()
    {
        Rb2d.velocity = new Vector2(Direction.x * speed, Rb2d.velocity.y);
        
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(FeetPos.position, checkRadius, whatIsGround);
        if (Direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(Direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        /*if (Direction.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }*/
    } 
    
    public void OnMove(InputAction.CallbackContext obj)
    {
        Direction = obj.ReadValue<Vector2>();
    }
    public void DoJump(InputAction.CallbackContext obj)
    {
        
        if (isGrounded && obj.performed)
        {
            Debug.Log(obj);
            isJumping = true;
            jumpTimerCounter = jumpTime;
            Rb2d.velocity = Vector2.up * jumpForce;
        }
        /*if (isGrounded == true)
        {
        anim.SetBool("isJumping", false);
        }
        else
        {
        anim.SetBool("isJumping", true);
        }*/
        if(obj.performed && isJumping)
        {
            Debug.Log(obj);
            if (jumpTimerCounter > 0)
            {
                Rb2d.velocity = Vector2.up * jumpForce;
                jumpTimerCounter -= Time.deltaTime;
                
            }
            else 
            {
                isJumping = false;
            }
        }
        if (obj.canceled)
        {
            Debug.Log(obj);
            isJumping = false;
        }
    }
    
    private void OnDisable()
    {
        move.Disable();
       // controls.Player.Jump.Disable();
    }
}
