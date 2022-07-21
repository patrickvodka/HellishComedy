using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class SoldierColl : MonoBehaviour
{
    private HitBullet _hitBullet;
    private Movement movement;
    private void Awake()
    {
        _hitBullet = GameObject.FindWithTag("Player").GetComponent<HitBullet>();
        movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

   

    private void OnCollisionEnter2D (Collision2D coll2D)
    { 
        Debug.Log("hit");
        if (coll2D.gameObject.CompareTag("Player"))
        {
            HitBullet.instance.HitBulletSoldierUpDown();
        }

    }

    private void OnCollisionStay2D(Collision2D collStay2D)
    {
        Debug.Log("hitlong");
        if (collStay2D.gameObject.CompareTag("Player"))
        {
            HitBullet.instance.HitBulletSoldierUpDown();
        }
    }
}
