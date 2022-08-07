using System;
using System.Collections;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class soldier : MonoBehaviour
{
    [Header("Munition utilisé")]
    public Bullet _bullet;
    [Space]
    [Header("Vers la où il tire,1 Direction")]
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    [Header("Laisser actif Pour les Soldats Bleu")]
    public bool BlueSoldier;
    public float shootTimeRed;
    public float shootTimeBlue;
    private float shootTime;
    
    private AudioSource audioS;
    private Transform SpawnBulletUp;
    private Transform SpawnBulletDown;
    private Transform SpawnBulletLeft;
    private Transform SpawnBulletRight;
    private Transform SoldierSr;
    
    private GameObject BulletRight;
    private GameObject BulletLeft;
    private GameObject BulletDown;
    private GameObject BulletUp;
    private GameObject BulletChoose;
    private SoldierRespawn soldierRespawn;
    
    private bool canShoot=true;
    private Transform transformGun;
    //private Collider2D collider2D;
    
   
   private void Awake()
   {
    //   collider2D = GetComponent<Collider2D>();
     //  soldierRespawn = transform.parent.GetComponent<SoldierRespawn>();
       SoldierSr = transform.Find("SoldierS.R");
        SpawnBulletUp = transform.Find("SpawnBulletUp");
        SpawnBulletDown = transform.Find("SpawnBulletDown");
        SpawnBulletLeft = transform.Find("SpawnBulletLeft");
        SpawnBulletRight = transform.Find("SpawnBulletRight");
        BulletRight = _bullet.EnemyBullet[0];
        BulletLeft = _bullet.EnemyBullet[1];
        BulletUp = _bullet.EnemyBullet[2];
        BulletDown = _bullet.EnemyBullet[3];
        audioS = transform.GetComponent<AudioSource>();
   }

    private void Start()
    {
       var time =BlueSoldier ? shootTime=shootTimeBlue : shootTime=shootTimeRed;
       
       if (Up)
        {
            SoldierSr.transform.eulerAngles = new Vector3(0, 0, 0);
            transformGun = SpawnBulletUp;
            BulletChoose = BulletUp;
        }
        if (Down)
        {
            SoldierSr.transform.eulerAngles = new Vector3(0, 0,180);
            transformGun = SpawnBulletDown;
            BulletChoose = BulletDown;
        }
        if (Right)
        {
            SoldierSr.transform.eulerAngles = new Vector3(0, 0,-90);
            transformGun = SpawnBulletRight;
            BulletChoose = BulletRight;
        }
        if(Left)
        {
            SoldierSr.transform.eulerAngles = new Vector3(0, 0,90);
            transformGun = SpawnBulletLeft;
            BulletChoose = BulletLeft;
        }

        if (Up && Down && Right && Left)
        {
            Debug.Log("Seule 1 peut etre actif! ");
        }
        transformGun.rotation=quaternion.Euler(0,0,0);
    }


    void FixedUpdate()
    {
        if (canShoot)
        {
            StartCoroutine(ShootRoutine());
        }
    }

   /* private void Update()
    {
        if (collider2D.IsTouchingLayers(128))
        {
            soldierRespawn.canBeActive = false; 
        }
        else
        {
            soldierRespawn.canBeActive = true;
        }
    }*/


    void Shoot()
    {
        audioS.Play(0);
        Instantiate(BulletChoose, transformGun.position, transform.GetChild(0).rotation);
    }
    private IEnumerator ShootRoutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootTime);
        Shoot();
        canShoot = true;

    }

   /* private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Debug.Log("stay");
            soldierRespawn.canBeActive = false;
        }
       /* else
        {
            Debug.Log("stayelse");
            soldierRespawn.canBeActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("exit");
            soldierRespawn.canBeActive = true;
        }
        /*else
        {
            Debug.Log("exitelse");
            soldierRespawn.canBeActive = true;
        }
    }*/
}
