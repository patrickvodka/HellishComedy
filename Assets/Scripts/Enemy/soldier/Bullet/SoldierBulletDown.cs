using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SoldierBulletDown : MonoBehaviour
{
    //><
    [Space]
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 bulletContact;
    private int randomNum;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = new Vector2(rb.velocity.x,Speed);
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       // ContactPoint2D contact = col.contacts[0];
       // Vector2 numContact = contact.point;
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if(col.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = col.contacts[0];
           //  Vector2 bulletContact = contact.rigidbody.position;
           // Vector2 playerTrans = col.rigidbody.ClosestPoint(transform.position);
            var bulletNormal = contact.normal.normalized;
            Debug.Log(bulletNormal);
            if (bulletNormal.x==0f)//si x est 0
            {
                var randomBool = true;
                if (randomBool)
                {
                    randomBool = false;
                    randomNum = UnityEngine.Random.Range(0,2);
                }
                if (randomNum == 0)
                {
                    bulletNormal.x=0.1f;
                }
                else
                {
                    bulletNormal.x=-0.1f;
                }
            }
            if (bulletNormal.x > 0)//si la balle a touché a droite(balle)
            {
                if (bulletNormal.y >0)//si la balle a touché a droite en haut (balle)
                {
                    HitBullet.Instance.HitBulletSoldierLeft();
                   // Debug.Log("DroiteHaut");
                  // Debug.Log(bulletNormal);
                }
                else//si la balle a touché a droite en bas et a 0(balle)
                {
                    HitBullet.Instance.HitBulletSoldierLeft();
                  //  Debug.Log(bulletNormal);
                   // Debug.Log("DroiteBas");
                }
            }
            if(bulletNormal.x<0)//si la balle a touché a gauche (balle)
            {
                if (bulletNormal.y <0)//si la balle a touché gauche haut  (balle)
                {
                    HitBullet.Instance.HitBulletSoldierRight();
                  //  Debug.Log("gaucheHaut");
                  //  Debug.Log(bulletNormal);
                }
                else//si la balle a touché a gauche bas et 0 (balle)
                {
                    HitBullet.Instance.HitBulletSoldierRight();
                   // Debug.Log("gauchebas");
                   // Debug.Log(bulletNormal);
                }
            }
            Destroy(gameObject);
        }
    }

    

    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
        
    }
}
