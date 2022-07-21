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
    
    private bool canShoot=true;
    private Transform transformGun;
    
   
   private void Awake()
   {
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

    void Shoot()
    {
        audioS.Play(0);
        Instantiate(BulletChoose, transformGun.position,quaternion.Euler(0,0,0));
    }
    private IEnumerator ShootRoutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(2);
        Shoot();
        canShoot = true;

    }
}
