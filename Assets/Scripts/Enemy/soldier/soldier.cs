using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class soldier : MonoBehaviour
{
    [Header("Munition utilisé")]
    public Bullet _bullet;
    [Space]
    [Header("Vers la où il tire")]
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    
    private Transform SpawnBulletUp;
    private Transform SpawnBulletDown;
    private Transform SpawnBulletLeft;
    private Transform SpawnBulletRight;
    
    private GameObject BulletRight;
    private GameObject BulletLeft;
    private GameObject BulletDown;
    private GameObject BulletUp;
    private GameObject BulletChoose;
    
    private bool canShoot=true;
    private Transform transformGun;
    
   
   private void Awake()
    {
        SpawnBulletUp = transform.Find("SpawnBulletUp");
        SpawnBulletDown = transform.Find("SpawnBulletDown");
        SpawnBulletLeft = transform.Find("SpawnBulletLeft");
        SpawnBulletRight = transform.Find("SpawnBulletRight");
        BulletRight = _bullet.EnemyBullet[0];
        BulletLeft = _bullet.EnemyBullet[1];
        BulletUp = _bullet.EnemyBullet[2];
        BulletDown = _bullet.EnemyBullet[3];
    }

    private void Start()
    {
        if (Up)
        {
            transformGun = SpawnBulletUp;
            BulletChoose = BulletUp;
        }
        if (Down)
        {
            transformGun = SpawnBulletDown;
            BulletChoose = BulletDown;
        }
        if (Right)
        {
            transformGun = SpawnBulletRight;
            BulletChoose = BulletRight;
        }
        if(Left)
        {
            transformGun = SpawnBulletLeft;
            BulletChoose = BulletLeft;
        }

        if (Up && Down && Right && Left)
        {
            Debug.Log("Seule 1 peut etre actif! ");
        }
    }


    void FixedUpdate()
    {
        if (canShoot)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    void Shoot()
    {
        Instantiate(BulletChoose, transformGun.position,quaternion.identity);
    }
    private IEnumerator ShootRoutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(2);
        Shoot();
        canShoot = true;

    }
}
