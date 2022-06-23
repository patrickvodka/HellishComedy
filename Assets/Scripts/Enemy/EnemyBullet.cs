using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Space]
    public float Speed;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        rb.velocity = new Vector2(Speed,rb.velocity.y);
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null)
        {
            
        }
    }

    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
        
    }
}
