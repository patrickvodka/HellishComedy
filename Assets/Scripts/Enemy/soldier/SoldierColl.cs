using System;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using UnityEngine;

public class SoldierColl : MonoBehaviour
{
    private HitBullet _hitBullet;
    private Movement movement;
    private int randomNum;
    private bool randomBool;
    private void Awake()
    {
        _hitBullet = GameObject.FindWithTag("Player").GetComponent<HitBullet>();
        movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

   

    private void OnCollisionEnter2D (Collision2D coll2D)
    {
        if (coll2D.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = coll2D.contacts[0];
            //  Vector2 bulletContact = contact.rigidbody.position;
            // Vector2 playerTrans = col.rigidbody.ClosestPoint(transform.position);
            var bulletNormal = contact.normal.normalized;
            if (bulletNormal.x == 0f) //si x est 0
            {
                if (randomBool)
                {
                    randomBool = false;
                    randomNum = UnityEngine.Random.Range(0, 2);
                }

                if (randomNum == 0)
                {
                    bulletNormal.x = 0.1f;
                }
                else
                {
                    bulletNormal.x = -0.1f;
                }
            }

            if (bulletNormal.x > 0) //si la balle a touché a droite(balle)
            {
                if (bulletNormal.y > 0) //si la balle a touché a droite en haut (balle)
                {
                    HitBullet.Instance.HitBulletSoldierLeft();
                    // Debug.Log("DroiteHaut");
                    // Debug.Log(bulletNormal);
                }
                else //si la balle a touché a droite en bas et a 0(balle)
                {
                    HitBullet.Instance.HitBulletSoldierLeft();
                    //  Debug.Log(bulletNormal);
                    // Debug.Log("DroiteBas");
                }
            }

            if (bulletNormal.x < 0) //si la balle a touché a gauche (balle)
            {
                if (bulletNormal.y < 0) //si la balle a touché gauche haut  (balle)
                {
                    HitBullet.Instance.HitBulletSoldierRight();
                    //  Debug.Log("gaucheHaut");
                    //  Debug.Log(bulletNormal);
                }
                else //si la balle a touché a gauche bas et 0 (balle)
                {
                    HitBullet.Instance.HitBulletSoldierRight();
                    // Debug.Log("gauchebas");
                    // Debug.Log(bulletNormal);
                }
            }
        }

    } 

    private void OnColliderStay2D(Collider2D collStay2D)
    {
        if (collStay2D.CompareTag("Player"))
        {
            if (randomBool) 
            {
                    randomBool = false;
                    randomNum = UnityEngine.Random.Range(0,2);
            } 
            if (randomNum == 0)
            {
                    HitBullet.Instance.HitBulletSoldierLeft();
                    randomBool = true;
            }
            else
            {
                    HitBullet.Instance.HitBulletSoldierRight();
                    randomBool = true;
            }
         
            
        }
    }
}
