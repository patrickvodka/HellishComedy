using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class soldier : MonoBehaviour
{
    [Header("Munition utilisé")]
    public Bullet _bullet;
    private Transform transGun;
    private GameObject Bullet;
    private bool canShoot=true;
    
   
    void Awake()
    {
        Bullet = _bullet.EnemyBullet[0];
        transGun= gameObject.transform.GetChild(0);
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
        Instantiate(Bullet, transGun.position,quaternion.identity);
    }
    private IEnumerator ShootRoutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(2);
        Shoot();
        canShoot = true;

    }
}
