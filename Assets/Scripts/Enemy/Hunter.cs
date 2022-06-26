using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Hunter : MonoBehaviour
{
    private int RayCastDistance;
    private Vector3 RayCastMove;
    private float RayCastMoveY;
    // <>
    void Awake()
    {
        RayCastMoveY = Mathf.Clamp(0, -90, 90);
        RayCastMove = new Vector3(0, RayCastMoveY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        var posEnd = transform.position + ((Vector3.right + RayCastMove) * RayCastDistance);
        Debug.DrawLine(pos,posEnd,Color.red);
        RaycastHit2D hit = Physics2D.Linecast(pos, posEnd, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
               Debug.Log('Y'); 
            }
            else
            {
                Debug.Log('N');
            }

        }


    }
}
