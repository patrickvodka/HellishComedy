using System;
using System.Collections;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    //><
    private Rigidbody2D rb;
    public static HitBullet instance;
    
    private GameObject BulletRight;
    private GameObject BulletLeft;
    private GameObject BulletUp;
    private GameObject BulletDown;
    
    private bool HitRight;
    private bool HitLeft;
    private bool HitUpDown;
    
    private int StartTime = 0;
    private int EndTime = 120;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        if (HitRight)
        {
            movement.canMove = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(5);
        }
        if (HitLeft)
        {
            movement.canMove = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer(-5);
        }
        if (HitUpDown)
        {
            movement.canMove = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            var random = UnityEngine.Random.Range(0, 1);
            if (random == 0)
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
            rb.velocity = new Vector2(x*1.5f, rb.velocity.y);
            rb.gravityScale = 5;
            StartTime++;
        }
        else
        {
            HitUpDown = false;
            HitLeft = false;
            HitRight = false;
            movement.canMove = true;
            rb.gravityScale = 3;
            StartTime = 0;
        }
    }
}
 