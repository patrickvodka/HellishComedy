using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoldierColl : MonoBehaviour
{
    private Movement movement;
    private SoldierRespawn SoldierResp;
    private void Awake()
    {
        SoldierResp = transform.parent.GetComponent<SoldierRespawn>();
        movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D coll2D)
    { 
        Debug.Log("Hit");
        if (coll2D.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log('e');
            SoldierResp.soldierDied = true;
            movement.HasADash = true;
            transform.gameObject.SetActive(false);
        }

        if (coll2D.gameObject.CompareTag("Player"))
        {
           Debug.Log("player"); 
        }
    }
}
