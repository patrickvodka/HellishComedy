using System;
using System.Collections;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    //><
    public Bullet _bullet;
    private GameObject BulletObj;
    private Rigidbody2D rb;
    private EnemyBullet _enemyBullet;
    public static HitBullet instance;
    private bool Hit;
    private int StartTime = 0;
    private int EndTime = 120;
    private bool VelocityOn=false;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        BulletObj = _bullet.EnemyBullet[0];
        _enemyBullet = BulletObj.GetComponent<EnemyBullet>();
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        if (Hit)
        {
            movement.canMove = false;
            rb.velocity = new Vector2(0,rb.velocity.y );
            FallPlayer();
        }
        
    }

    public void HitBulletSoldier()
    {
        Hit = true;

    }

      void FallPlayer()
    {
        if (StartTime < EndTime)
        {
            rb.velocity = new Vector2(_enemyBullet.Speed*1.5f, rb.velocity.y);
            rb.gravityScale = 5;
            StartTime++;
        }
        else
        {
            Hit = false;
            movement.canMove = true;
            rb.gravityScale = 3;
            StartTime = 0;
        }
    }
}
 