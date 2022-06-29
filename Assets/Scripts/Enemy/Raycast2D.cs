using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast2D : MonoBehaviour
{
    [Space]
   private  int RotaAngle=45;
   private Vector2 MoveRaycast;
   
   [Space]
   [Header("Vision du Monstre")]
   public bool Right;


   private void Start()
   {
       if (!Right)
       {
            RotaAngle = -45;
       }
   }

   void Update()
    {
        if (Right)
        {
            RaycastRight();
        }
        else
        {
            
            RaycastLeft();
        }
        //si vision 90degres en face de lui((RotaAngleStart=45,RotaAngleEnd=135)*-1 si sens inverse)
        //si vision 120degres en face de lui((RotaAngleStart=30,RotaAngleEnd=150)*-1 si sens inverse)
        
        
    }

    void RaycastRight()
    {
        
        var RotationXRaycast = Mathf.Sin(Mathf.Deg2Rad * RotaAngle) * 10;
        var RotationYRaycast = Mathf.Cos(Mathf.Deg2Rad * RotaAngle) * 10;
        MoveRaycast = new Vector2(RotationXRaycast, RotationYRaycast);
        Debug.DrawRay(transform.position, (MoveRaycast), Color.red);
        
        if (RotaAngle == 135)
        {
            RotaAngle = 44;
        }
        RotaAngle ++; 
    }
    void RaycastLeft()
    {
      
        var RotationXRaycast = Mathf.Sin(Mathf.Deg2Rad * RotaAngle) * 10;
        var RotationYRaycast = Mathf.Cos(Mathf.Deg2Rad * RotaAngle) * 10;
        MoveRaycast = new Vector2(RotationXRaycast, RotationYRaycast);
        Debug.DrawRay(transform.position, (MoveRaycast), Color.red);
        
        if (RotaAngle == -135)
        {
            RotaAngle = -46;
        }
            RotaAngle--; 
    }
}
