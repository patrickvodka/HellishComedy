using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SoldierBulletDown : MonoBehaviour
{
    [Space]
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 bulletContact;
    private Color debugCollisionColor = Color.red;
    public GameObject Goject;//touche gauche
    public GameObject Goject1;
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
             Vector2 bulletContact = contact.rigidbody.position;
            Vector2 playerTrans = col.rigidbody.ClosestPoint(transform.position);
            if (bulletContact.x<playerTrans.x)
            {
                Instantiate(Goject, playerTrans, quaternion.identity);
                Debug.Log("dsqd");
            }
            else 
                Instantiate(Goject1, bulletContact, quaternion.identity);
                 
            HitBullet.instance.HitBulletSoldierUpDown();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.blue;
        Gizmos.DrawWireSphere(transform.position,1);
    }

    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
        
    }
}
