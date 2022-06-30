using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Bullet : ScriptableObject
{
    [Header("toutes les munitions")]
    public GameObject[]  EnemyBullet;
    [Header("La Speed des balles ")]
    public float[] Speed;
    /*public float[] speed;
    private int Index;

    public bool BulletChoose()
    {
        foreach (var Bullet in EnemyBullet)
        {
           speed.SetValue(Bullet.GetComponent<SoldierBulletRight>().Speed,Index);
           Index++;

        }

        return false;
    }*/
}
