using System.Collections;
using UnityEngine;

public class SoldierRespawn : MonoBehaviour
{
    public float spawnTimer;
    [HideInInspector]
    public bool soldierDied;
    [HideInInspector]
    public GameObject _soldierObj;

    [HideInInspector]
    public bool canSpawn=false,canBeActive=false;
    

    void Update()
    {
       /* if (canBeActive&&canSpawn)
        {
            canBeActive = false;
            canSpawn = false;
            _soldierObj = transform.GetChild(0).gameObject;
            _soldierObj.SetActive(true);
        }*/
        if (soldierDied)
        {
            soldierDied = false;
            StartCoroutine(RespawnTimer());
        }
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        _soldierObj = transform.GetChild(0).gameObject;
        _soldierObj.SetActive(true);
        //canSpawn = true;
    }
}
