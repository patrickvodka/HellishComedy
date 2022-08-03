using System.Collections;
using UnityEngine;

public class SoldierBulletDown : MonoBehaviour
{
    [Space]
    public float Speed;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = new Vector2(rb.velocity.x,Speed);
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        Vector2 numContact = contact.point;
        Debug.Log(contact);
        Debug.Log(numContact);
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if(col.gameObject.CompareTag("Player"))
        {
            
            HitBullet.instance.HitBulletSoldierUpDown();
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
        
    }
}
