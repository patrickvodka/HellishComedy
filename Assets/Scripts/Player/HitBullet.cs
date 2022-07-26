using System;
using System.Collections;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class HitBullet : MonoBehaviour
{
    //><
    private Rigidbody2D rb;
    private SpriteRenderer Sr;
    public static HitBullet instance;
    
    private GameObject BulletRight;
    private GameObject BulletLeft;
    private GameObject BulletUp;
    private GameObject BulletDown;

    private bool randomBool;
    private bool HitRight;
    private bool HitLeft;
    private bool HitUpDown;

    private int RandomNumber;
    private int StartTime = 0;
    private int EndTime = 120;
    private Movement movement;

    private void Awake()
    {
        Sr=GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        if (HitRight)
        {
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(5);
        }
        if (HitLeft)
        {
            movement.canMove = false;
            movement.canJump = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(-5);
        }
        if (HitUpDown)
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
        }

        
    }
    public void HitBulletSoldierRight()
    {
        HitRight = true;

    }
    public void HitBulletSoldierLeft()
    {
        HitLeft = true;

    }
    public void HitBulletSoldierUpDown()
    {
        HitUpDown = true;

    }

      void FallPlayer(float x)
    {
        if (StartTime < EndTime)
        {
            Sr.enabled=false;
            rb.velocity = new Vector2(x*1.5f, rb.velocity.y);
            rb.gravityScale = 5;
            var moduloTwo = StartTime % 2;
            if (moduloTwo!=0)
            {
                Sr.enabled=true;  
            }
            StartTime++;
        }
        else
        {
            movement.canJump = true;
            HitUpDown = false;
            HitLeft = false;
            HitRight = false;
            movement.canMove = true;
            randomBool = true;
            rb.gravityScale = 3;
            StartTime = 0;
        }
    }
}
 