using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoldierObjSO: ScriptableObject
{
    public List<GameObject> soldiers = new List<GameObject>();
    public GameObject soldierClone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        Debug.Log(soldiers[0]);
        soldierClone = soldiers[0];
    }

    public void AddSoldierClone( GameObject soldier)
    {
        soldiers.Add(soldier);
    }

    /*public void TakeSoldierClone(GameObject soldier)
    {
        soldierClone = soldiers[0];
    }*/
}
