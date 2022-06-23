using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class soldier : MonoBehaviour
{
    private Transform transGun;
    public GameObject Bullet;
    private bool canShoot=true;
   
    void Awake()
    {
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
