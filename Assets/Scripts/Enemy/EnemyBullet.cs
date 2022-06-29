using System.Collections;
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
    void Start()
    {
        rb.velocity = new Vector2(Speed,rb.velocity.y);
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if(col.gameObject.CompareTag("Player"))
        {
            HitBullet.instance.HitBulletSoldier();
            Debug.Log("yes");
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
        
    }
}
