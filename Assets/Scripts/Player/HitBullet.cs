using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = Unity.Mathematics.Random;

public class HitBullet : MonoBehaviour
{
    //><
    private Rigidbody2D rb;
    private SpriteRenderer Sr;
    public static HitBullet Instance;
    
    private GameObject BulletRight;
    private GameObject BulletLeft;
    private GameObject BulletUp;
    private GameObject BulletDown;

    //private bool randomBool;
    private bool HitRight;
    private bool HitLeft;
    //private bool HitUpDown;
    private bool canBeHit=true;

    [Header("Temps du stun Default=120, Nombre Pair Only")]
    public int EndTime;
    [Header("Vélocité de la chute Default=5 ")]
    public float fallVelocity;

    [Header("gravité pendant la chute  Default=5 ")]
    public float gravity;
    private int RandomNumber;
    private int StartTime = 0;
    private Movement movement;

    private void Awake()
    {
        Sr=GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    private void Update()
    {
        if (HitRight)
        {
            canBeHit = false;
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(fallVelocity);
        }
        if (HitLeft)
        {
            canBeHit = false;
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(-fallVelocity);
        }
        /*if (HitUpDown)
        {
            
            if (randomBool)
            {
                randomBool = false;
                RandomNumber = UnityEngine.Random.Range(0,2);
            }
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            if (RandomNumber == 0)
            {
                FallPlayer(5);
            }
            else
            {
                FallPlayer(-5);
            }
        }*/

        
    }
    public void HitBulletSoldierRight()
    {
        HitRight = true;

    }
    public void HitBulletSoldierLeft()
    {
        HitLeft = true;

    }
    /*public void HitBulletSoldierUpDown()
    {
        HitUpDown = true;

    }*/

      void FallPlayer(float x)
    {
        if (StartTime < EndTime)
        {
            Sr.enabled=false;
            rb.velocity = new Vector2(x/*1.5f*/, rb.velocity.y);
            rb.gravityScale = gravity;
            var moduloTwo = StartTime % 2;
            if (moduloTwo!=0)
            {
                Sr.enabled=true;  
            }
            StartTime++;
            Debug.Log(StartTime);
        }
        else
        {
            Debug.Log("start");
            Sr.enabled=true; 
            canBeHit = true;
            movement.canJump = true;
            //HitUpDown = false;
            HitLeft = false;
            HitRight = false;
            movement.canMove = true;
           // randomBool = true;
            rb.gravityScale = 3;
            StartTime = 0;
        }
    }
}
 