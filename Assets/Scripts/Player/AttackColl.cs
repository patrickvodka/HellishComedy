using UnityEngine;

public class AttackColl : MonoBehaviour
{
    private  SoldierRespawn soldierResp;
    private Movement movement;
    private GameObject clone;
    [Header("La force du saut apres kill")]
    public float jumpForce;
    
    private void Awake()
    {
        movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }


    private void OnTriggerEnter2D (Collider2D coll2D)
    {
        if (coll2D.gameObject.CompareTag("Soldier"))
        {
            movement.rb.velocity = new Vector2(movement.rb.velocity.x, jumpForce);
            clone = coll2D.gameObject.transform.parent.GetComponent<soldier>().gameObject;
            soldierResp = clone.transform.parent.GetComponent<SoldierRespawn>();
            clone.SetActive(false);
            Instantiate(clone, clone.transform.position, Quaternion.identity,soldierResp.transform);
            soldierResp.soldierDied = true;
            movement.isDashing = false;
            movement.HasADash = true;
            Destroy(clone);
        }

    }
}
